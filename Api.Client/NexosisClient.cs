using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;

[assembly: InternalsVisibleTo("Api.Client.Tests")]

namespace Nexosis.Api.Client
{
    public class NexosisClient : INexosisClient
    {
        private readonly string key;
        private readonly string configuredUrl;

        protected readonly ApiConnection apiConnection;

        /// <summary>
        /// The name of the environment variable used for the API key from the api manager
        /// </summary>
        public const string NexosisApiKeyEnvironmentVariable = "NEXOSIS_API_KEY";

        /// <summary>
        /// The default URL of the api endpoint.
        /// </summary>
        public const string BaseUrl = "https://ml.nexosis.com/v1/";

        /// <summary>
        /// The client id and version sent as the User-Agent header
        /// </summary>
        public const string ClientVersion = "Nexosis-DotNet-API-Client/1.0";

        /// <summary>
        /// The currently configured api key used by this instance of the client.
        /// </summary>
        public string ApiKey => key;

        /// <summary>
        /// The URL endpoint the client will connect to.
        /// </summary>
        public string ConfiguredUrl => configuredUrl ?? BaseUrl;

        public const int MaxPageSize = 100;

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
        public NexosisClient(string key) : this(key, BaseUrl, new ApiConnection.HttpClientFactory())
        {

        }

        /// <summary>
        /// Constructs a instance of the client with the api key and uri as parameters.
        /// </summary>
        /// <param name="key">The api key from your account.</param>
        /// <param name="uri">The uri endpoint to run against. Not particularly useful except for internal testsing.</param>
        public NexosisClient(string key, string uri) : this(key, uri, new ApiConnection.HttpClientFactory())
        {

        }

        /// <summary>
        /// Internal provided for testing use only 
        /// </summary>
        protected internal NexosisClient(string key, string uri, ApiConnection.IHttpClientFactory clientFactory)
        {
            this.key = key;

            if (!uri.EndsWith("/"))
                uri = uri + "/";

            configuredUrl = uri;

            apiConnection = new ApiConnection(uri, key, clientFactory);

            Sessions = new SessionClient(apiConnection);
            DataSets = new DataSetClient(apiConnection);
            Imports = new ImportClient(apiConnection);
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
            return apiConnection.Get<AccountBalance>("/data", null, httpMessageTransformer, cancellationToken);
        }

        public ISessionClient Sessions { get; }
        public IDataSetClient DataSets { get; }
        public IImportClient Imports { get; }
    }
}
