using System;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;
using System.Threading;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nexosis.Api.Client
{
    public class ModelClient : IModelClient
    {
        private readonly ApiConnection apiConnection;

        public ModelClient(ApiConnection apiConnection)
        {
            this.apiConnection = apiConnection;
        }

        public Task<ModelSummary> Get(Guid id)
        {
            return Get(id, null, CancellationToken.None);
        }

        public async Task<ModelSummary> Get(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            return await apiConnection.Get<ModelSummary>($"model/{id}", null, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }

        public Task<List<ModelSummary>> List()
        {
            return ListModelInternal(new Dictionary<string, string>(), null, CancellationToken.None);
        }

        public Task<List<ModelSummary>> List(int page, int pageSize)
        {
            return List(String.Empty, page, pageSize);
        }

        public Task<List<ModelSummary>> List(string dataSourceName)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(dataSourceName), dataSourceName },
            };

            return ListModelInternal(parameters, null, CancellationToken.None);
        }

        public Task<List<ModelSummary>> List(string dataSourceName, int page, int pageSize)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(dataSourceName), dataSourceName },
                { nameof(page), page.ToString() },
                { nameof(pageSize), pageSize.ToString() }
            };

            return ListModelInternal(parameters, null, CancellationToken.None);
        }

        public Task<List<ModelSummary>> List(string dataSourceName, DateTimeOffset createdAfterDate, DateTimeOffset createdBeforeDate)
        {
            return List(dataSourceName, createdAfterDate, createdBeforeDate, null, CancellationToken.None);
        }

        public Task<List<ModelSummary>> List(string dataSourceName, DateTimeOffset createdAfterDate, DateTimeOffset createdBeforeDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return List(dataSourceName, createdAfterDate, createdBeforeDate, httpMessageTransformer, CancellationToken.None);
        }

        public Task<List<ModelSummary>> List(string dataSourceName, DateTimeOffset createdAfterDate, DateTimeOffset createdBeforeDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(dataSourceName), dataSourceName },
                { nameof(createdAfterDate), createdAfterDate.ToString("O") },
                { nameof(createdBeforeDate), createdBeforeDate.ToString("O") }
            };

            return ListModelInternal(parameters, httpMessageTransformer, cancellationToken);
        }

        private async Task<List<ModelSummary>> ListModelInternal(IDictionary<string, string> parameters,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var response = await apiConnection.Get<ModelResponseDto>("models", parameters, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
            return response?.items;
        }

        private class ModelResponseDto
        {
            [JsonProperty("items", Required = Required.Always)]
            public List<ModelSummary> items { get; set; }
        }
    }
}
