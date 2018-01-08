using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{
    public class VocabularyClient : IVocabularyClient
    {
        private readonly ApiConnection apiConnection;

        public Action<HttpRequestMessage, HttpResponseMessage> HttpMessageTransformer { get; set; }

        public VocabularyClient(ApiConnection connection)
        {
            this.apiConnection = connection;
        }

        public Task<VocabulariesSummary> List(Guid sessionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return apiConnection.Get<VocabulariesSummary>($"/sessions/{sessionId}/results/vocabularies", null,
                HttpMessageTransformer, cancellationToken);
        }

        public Task<VocabularyResponse> Get(VocabulariesQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = query.ToParameters();
            return apiConnection.Get<VocabularyResponse>($"/sessions/{query.SessionId}/results/vocabularies/{query.ColumnName}", parameters, HttpMessageTransformer, cancellationToken);
        }
    }
}