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
            return await apiConnection.Get<ModelSummary>($"models/{id}", null, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
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
        private class ModelResponseDto
        {
            [JsonProperty("items", Required = Required.Always)]
            public List<ModelSummary> items { get; set; }
        }

        private async Task<List<ModelSummary>> ListModelInternal(IDictionary<string, string> parameters,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var response = await apiConnection.Get<ModelResponseDto>("models", parameters, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
            return response?.items;
        }

        public Task<ModelPredictionResult> Predict(Guid modelId, List<Dictionary<string, string>> data)
        {
            return Predict(modelId, data, null);
        }

        public Task<ModelPredictionResult> Predict(Guid modelId, List<Dictionary<string, string>> data, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return Predict(modelId, data, httpMessageTransformer, CancellationToken.None);
        }

        public Task<ModelPredictionResult> Predict(Guid modelId, List<Dictionary<string, string>> data, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNull(data, nameof(data));

            var requestBody = new { Data = data };
            return apiConnection.Post<ModelPredictionResult>($"models/{modelId}/predict", null, requestBody, httpMessageTransformer, cancellationToken);
        }

        public Task Remove(Guid modelId)
        {
            return Remove(modelId, null);
        }

        public Task Remove(Guid modelId, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return Remove(modelId, httpMessageTransformer, CancellationToken.None);
        }

        public async Task Remove(Guid modelId, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            await apiConnection.Delete($"models/{modelId}", null, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }

        public Task Remove(string dataSourceName, DateTimeOffset createdAfterDate, DateTimeOffset createdBeforeDate)
        {
            return Remove(dataSourceName, createdAfterDate, createdBeforeDate, null);
        }

        public Task Remove(string dataSourceName, DateTimeOffset createdAfterDate, DateTimeOffset createdBeforeDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return Remove(dataSourceName, createdAfterDate, createdBeforeDate, httpMessageTransformer, CancellationToken.None);
        }

        public Task Remove(string dataSourceName, DateTimeOffset createdAfterDate, DateTimeOffset createdBeforeDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(createdAfterDate), createdAfterDate.ToString("O") },
                { nameof(createdBeforeDate), createdBeforeDate.ToString("O") },
            };

            if (!string.IsNullOrEmpty(dataSourceName))
            {
                parameters.Add(nameof(dataSourceName), dataSourceName);
            }

            return RemoveModelsInternal(parameters, httpMessageTransformer, cancellationToken);
        }

        private async Task RemoveModelsInternal(IDictionary<string, string> parameters, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            await apiConnection.Delete("models", parameters, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }
    }
}
