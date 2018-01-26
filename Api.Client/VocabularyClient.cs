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
        public Task<VocabulariesSummary> List(VocabulariesQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = query.ToParameters();
            return apiConnection.Get<VocabulariesSummary>($"/vocabulary", parameters, HttpMessageTransformer, cancellationToken);
        }

        public Task<VocabularyResponse> Get(VocabularyWordsQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = query.ToParameters();
            return apiConnection.Get<VocabularyResponse>($"/vocabulary/{query.Id}", parameters, HttpMessageTransformer, cancellationToken);
        }

        public VocabularyClient(ApiConnection connection)
        {
            this.apiConnection = connection;
        }
    }
}