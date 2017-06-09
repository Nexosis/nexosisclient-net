using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nexosis.Api.Client.Model;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Api.Client.Tests")]

namespace Nexosis.Api.Client
{
    public class NexosisClient : INexosisClient
    {
        private readonly string key;
        private readonly string endpoint;
        private readonly IHttpClientFactory httpClientFactory;

        /// <summary>
        /// The name of the environment variable used for the API key from the api manager
        /// </summary>
        public const string NexosisApiKeyEnvironmentVariable = "NEXOSIS_API_KEY";

        /// <summary>
        /// The default URL of the api endpoint.
        /// </summary>
        public const string BaseUrl = "https://ml.nexosis.com/api/";

        /// <summary>
        /// The client id and version sent as the User-Agent header
        /// </summary>
        public const string ClientVersion = "Nexosis-DotNet-API-Client/1.0";

        /// <summary>
        /// The currently configured api key used by this instance of the client.
        /// </summary>
        public string ApiKey => key;

        /// <summary>
        /// Constructs a instance of the client with the api key read from an environement variable
        /// </summary>
        public NexosisClient() : this(Environment.GetEnvironmentVariable(NexosisApiKeyEnvironmentVariable))
        {

        }

        /// <summary>
        /// Constructs a instance of the client with the api key as a parameter.
        /// </summary>
        /// <param name="key">The api key from your account.</param>
        public NexosisClient(string key) : this(key, BaseUrl, new HttpClientFactory())
        {

        }

        /// <summary>
        /// Internal provided for testing use only 
        /// </summary>
        internal NexosisClient(string key, string endpoint, IHttpClientFactory httpClientFactory)
        {
            this.key = key;

            if (!endpoint.EndsWith("/"))
                endpoint = endpoint + "/";
            this.endpoint = endpoint;

            this.httpClientFactory = httpClientFactory;
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
                return new HttpClient(handler);
            }
        }

        private Task<T> Get<T>(string path, IDictionary<string, string> parameters, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var uri = new Uri(endpoint + path);
            uri = uri.AddParameters(parameters);
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                return MakeRequest<T>(requestMessage, httpMessageTransformer, cancellationToken);
            }
        }

        private async Task<T> MakeRequest<T>(HttpRequestMessage requestMessage, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var client = httpClientFactory.CreateClient();
            try
            {
                requestMessage.Headers.Add("api-key", key);
                requestMessage.Headers.Add("User-Agent", ClientVersion);

                httpMessageTransformer?.Invoke(requestMessage, null);
                var responseMessage = await client.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);
                httpMessageTransformer?.Invoke(requestMessage, responseMessage);

                if (responseMessage != null && responseMessage.IsSuccessStatusCode)
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
                    throw new NexosisClientException("API Error", responseMessage.StatusCode);
                }
            }
            catch (HttpRequestException hre)
            {
                throw new NexosisClientException(hre.Message, hre);
            }
            finally
            {
                client.Dispose();
            }
        }

        public Task<AccountBalance> GetAccountBalance()
        {
            return GetAccountBalance(null, CancellationToken.None);
        }

        public Task<AccountBalance> GetAccountBalance(Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return GetAccountBalance(httpMessageTransformer, CancellationToken.None);
        }

        public Task<AccountBalance> GetAccountBalance(Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            return Get<AccountBalance>("/data", null, httpMessageTransformer, cancellationToken);
        }

        public async Task<SessionResponse> CreateForecastSession(TextReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateForecastSession(TextReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateForecastSession(TextReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateForecastSession(TextReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateForecastSession(TextReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateForecastSession(TextReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateForecastSession(TextReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<SessionResponse>> ListSessions()
        {
            return ListSessions(new Dictionary<string, string>(), null, CancellationToken.None);
        }

        public Task<List<SessionResponse>> ListSessions(string dataSetName)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(dataSetName), dataSetName },
            };
            return ListSessions(parameters, null, CancellationToken.None);
        }

        public Task<List<SessionResponse>> ListSessions(string dataSetName, string eventName)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(dataSetName), dataSetName },
                { nameof(eventName), eventName },
            };
            return ListSessions(parameters, null, CancellationToken.None);
        }

        public Task<List<SessionResponse>> ListSessions(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return ListSessions(dataSetName, eventName, startDate, endDate, null, CancellationToken.None);
        }

        public Task<List<SessionResponse>> ListSessions(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return ListSessions(dataSetName, eventName, startDate, endDate, httpMessageTransformer, CancellationToken.None);
        }

        public Task<List<SessionResponse>> ListSessions(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string> {
                { nameof(dataSetName), dataSetName },
                { nameof(eventName), eventName },
                { nameof(startDate), startDate.ToString("O") },
                { nameof(endDate), endDate.ToString("O") }
            };

            return ListSessions(parameters, httpMessageTransformer, cancellationToken);
        }

        private class SessionResponseDto
        {
            [JsonProperty("results", Required = Required.Always )]
            public List<SessionResponse> Results { get; set; }
        }
        private async Task<List<SessionResponse>> ListSessions(IDictionary<string, string> parameters,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var response = await Get<SessionResponseDto>("sessions", parameters, httpMessageTransformer, cancellationToken);
            return response?.Results;
        }

        public async Task RemoveSessions()
        {
            throw new NotImplementedException();
        }

        public async Task RemoveSessions(string dataSetName)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveSessions(string dataSetName, string eventName)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveSessions(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveSessions(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveSessions(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> GetSession(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> GetSession(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> GetSession(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResultStatus> GetSessionStatus(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResultStatus> GetSessionStatus(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResultStatus> GetSessionStatus(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveSession(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveSession(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveSession(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResult> GetSessionResults(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResult> GetSessionResults(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResult> GetSessionResults(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task GetSessionResults(Guid id, TextWriter output)
        {
            throw new NotImplementedException();
        }

        public async Task GetSessionResults(Guid id, TextWriter output, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task GetSessionResults(Guid id, TextWriter output, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetSummary> CreateDataSet(string dataSetName, IEnumerable<DataSetRow> data)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetSummary> CreateDataSet(string dataSetName, IEnumerable<DataSetRow> data, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetSummary> CreateDataSet(string dataSetName, IEnumerable<DataSetRow> data, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DataSetSummary>> ListDataSets()
        {
            throw new NotImplementedException();
        }

        public async Task<List<DataSetSummary>> ListDataSets(string nameFilter)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DataSetSummary>> ListDataSets(string nameFilter, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DataSetSummary>> ListDataSets(string nameFilter, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSet(string dataSetName)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSet(string dataSetName, int pageNumber, int pageSize, IEnumerable<string> includeColumns)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSet(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate,
            IEnumerable<string> includeColumns)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSet(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate,
            IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSet(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate,
            IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSet(string dataSetName, TextWriter output)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSet(string dataSetName, TextWriter output, int pageNumber, int pageSize, IEnumerable<string> includeColumns)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSet(string dataSetName, TextWriter output, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSet(string dataSetName, TextWriter output, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSet(string dataSetName, TextWriter output, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveDataSet(string dataSetName, DataSetDeleteOptions options)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveDataSet(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, DataSetDeleteOptions options)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveDataSet(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, DataSetDeleteOptions options,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveDataSet(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, DataSetDeleteOptions options,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSetForecast(string dataSetName)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSetForecast(string dataSetName, int pageNumber, int pageSize, IEnumerable<string> includeColumns)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSetForecast(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, int pageNumber,
            int pageSize, IEnumerable<string> includeColumns)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSetForecast(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSetForecast(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSetForecast(string dataSetName, TextWriter output)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSetForecast(string dataSetName, TextWriter output, int pageNumber, int pageSize,
            IEnumerable<string> includeColumns)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSetForecast(string dataSetName, TextWriter output, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSetForecast(string dataSetName, TextWriter output, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSetForecast(string dataSetName, TextWriter output, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveDataSetForecast(string dataSetName)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveDataSetForecast(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveDataSetForecast(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ForecastModel>> ListDataSetForecastModels(string dataSetName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ForecastModel>> ListDataSetForecastModels(string dataSetName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ForecastModel>> ListDataSetForecastModels(string dataSetName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ForecastModel> GetDataSetForecastModel(string dataSetName, string targetColumn)
        {
            throw new NotImplementedException();
        }

        public async Task<ForecastModel> GetDataSetForecastModel(string dataSetName, string targetColumn, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<ForecastModel> GetDataSetForecastModel(string dataSetName, string targetColumn, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSetForecastModelForecast(string dataSetName, string targetColumn, string algorithmKey)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSetForecastModelForecast(string dataSetName, string targetColumn, string algorithmKey,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    static class UriExtensions
    {
        /// <summary>
        /// Merge a dictionary of values with an existing <see cref="Uri"/>
        /// </summary>
        /// <param name="uri">Original request Uri</param>
        /// <param name="parameters">Collection of key-value pairs</param>
        /// <returns>Updated request Uri</returns>
        public static Uri AddParameters(this Uri uri, IDictionary<string, string> parameters)
        {
            if (parameters == null || !parameters.Any()) return uri;

            var hasQueryString = uri.OriginalString.IndexOf("?", StringComparison.Ordinal);
            string baseUri;
            if (hasQueryString != -1)
            {
                baseUri = uri.OriginalString.Substring(0, hasQueryString);
                var queryString = uri.OriginalString.Substring(hasQueryString);
                var values = queryString.Replace("?", "").Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

                var existingParameters = values.ToDictionary(key => key.Substring(0, key.IndexOf('=')), value => value.Substring(value.IndexOf('=') + 1));

                foreach (var existing in existingParameters)
                {
                    if (!parameters.ContainsKey(existing.Key))
                    {
                        parameters.Add(existing);
                    }
                }

            }
            else
            {
                baseUri = uri.OriginalString;
            }

            string query = string.Join("&", parameters.Select(kvp => kvp.Key + "=" + Uri.EscapeDataString(kvp.Value)));
            return new Uri(baseUri + "?" + query);
        }
    }
}
