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
        public async Task<VocabulariesSummary> List(VocabulariesQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = query.ToParameters();

            return await apiConnection.Get<VocabulariesSummary>($"/vocabulary", parameters, HttpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }

        public async Task<VocabularyResponse> Get(VocabularyWordsQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = query.ToParameters();

            return await apiConnection.Get<VocabularyResponse>($"/vocabulary/{query.Id}", parameters, HttpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }

        public async Task Remove(VocabularyRemoveCriteria criteria, CancellationToken cancellationToken = default(CancellationToken))
        {
            Argument.IsNotNull(criteria, nameof(criteria));

            if (criteria.VocabularyId.HasValue)
            {
                await apiConnection.Delete($"vocabulary/{criteria.VocabularyId}", null, HttpMessageTransformer, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                var parameters = criteria.ToParameters();

                await apiConnection.Delete("vocabulary", parameters, HttpMessageTransformer, cancellationToken).ConfigureAwait(false);
            }
        }

        public VocabularyClient(ApiConnection connection)
        {
            this.apiConnection = connection;
        }
    }
}