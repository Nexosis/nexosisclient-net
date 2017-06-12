using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{
    public class ApiConnection
    {
        private string endpoint;
        private string key;
        private IHttpClientFactory httpClientFactory;

        public ApiConnection(string endpoint, string key) : this(endpoint, key, new HttpClientFactory()) { }

        internal ApiConnection(string endpoint, string key, IHttpClientFactory httpClientFactory)
        {
            this.endpoint = endpoint;
            this.httpClientFactory = httpClientFactory;
            this.key = key;
        }

        /// <summary>
        /// <see cref="HttpClient"/> is extensible using a <see cref="HttpMessageHandler"/>, so if we provide a factory for creation of the <see cref="HttpClient"/> in the library, 
        /// we can then substitute another <see cref="HttpMessageHandler"/> in the creation of the client and that way fake/mock it out for use in testing.
        /// </summary>
        internal interface IHttpClientFactory
        {
            HttpClient CreateClient();
        }

        internal class HttpClientFactory : IHttpClientFactory
        {
            private readonly HttpMessageHandler handler;

            public HttpClientFactory()
            {

            }

            public HttpClientFactory(HttpMessageHandler handler)
            {
                this.handler = handler;
            }

            public HttpClient CreateClient()
            {
                if (handler != null)
                {
                    return new HttpClient(handler);
                }

                return new HttpClient();
            }
        }

        public async Task<T> Get<T>(string path, IDictionary<string, string> parameters, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var uri = PrepareUri(path, parameters);
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return await MakeRequest<T>(requestMessage, httpMessageTransformer, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        public async Task<T> Post<T>(string path, IDictionary<string, string> parameters, object body, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var uri = PrepareUri(path, parameters);
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // TODO: would it be better to do StreamContent with a MemoryStream?
                requestMessage.Content =
                    new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body)));
                requestMessage.Content.Headers.Add("Content-Type", "application/json");
                return await MakeRequest<T>(requestMessage, httpMessageTransformer, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        public async Task<T> Post<T>(string path, IDictionary<string, string> parameters, StreamReader body, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var uri = PrepareUri(path, parameters);
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                requestMessage.Content = new StreamContent(body.BaseStream);
                requestMessage.Content.Headers.Add("Content-Type", "text/csv");
                return await MakeRequest<T>(requestMessage, httpMessageTransformer, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        public Uri PrepareUri(string path, IDictionary<string, string> parameters)
        {
            // ctor made sure endpoint ends with / and we don't want doubles
            if (path.StartsWith("/"))
                path = path.Substring(1);
            var uri = new Uri(endpoint + path).AddParameters(parameters);
            return uri;
        }

        private async Task<T> MakeRequest<T>(HttpRequestMessage requestMessage,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var client = httpClientFactory.CreateClient();
            try
            {
                requestMessage.Headers.Add("api-key", key);
                requestMessage.Headers.Add("User-Agent", NexosisClient.ClientVersion);

                httpMessageTransformer?.Invoke(requestMessage, null);
                var responseMessage = await client.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);
                httpMessageTransformer?.Invoke(requestMessage, responseMessage);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultContent = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    try
                    {
                        var result = JsonConvert.DeserializeObject<T>(resultContent);
                        if (result is ReturnsCost)
                        {
                            (result as ReturnsCost).AssignCost(responseMessage.Headers);
                        }
                        return result;
                    }
                    catch (Exception e)
                    {
                        throw new NexosisClientException("Error deserializing response.", e);
                    }
                }
                else
                {
                    try
                    {
                        var errorResponseContent = responseMessage.Content.ReadAsStringAsync();
                        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(await errorResponseContent);
                        throw new NexosisClientException($"API Error: {responseMessage.StatusCode}", errorResponse);
                    }
                    catch
                    {
                        // don't care what as we are already in an error state.
                        throw new NexosisClientException($"API Error: {responseMessage.StatusCode} - {await responseMessage.Content.ReadAsStringAsync()}", responseMessage.StatusCode);
                    }
                }
            }
            // something is wrong with the HttpClient (network or other), not our code.
            catch (HttpRequestException e)
            {
                // wrapping so user can catch one exception type and handle it.
                throw new NexosisClientException(e.Message, e);
            }
            finally
            {
                client.Dispose();
            }
        }

    }

}
