using System;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;
using System.Threading;
using System.Net.Http;

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
    }
}
