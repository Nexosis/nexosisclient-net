using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{
    public class ContestClient : IContestClient
    {
        private readonly ApiConnection apiConnection;

        public Action<HttpRequestMessage, HttpResponseMessage> HttpMessageTransformer { get; set; }

        public ContestClient(ApiConnection connection)
        {
            this.apiConnection = connection;
        }

        public Task<ContestResponse> GetContest(Guid sessionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return apiConnection.Get<ContestResponse>($"/sessions/{sessionId}/contest", null, HttpMessageTransformer, cancellationToken);
        }

        public Task<ContestantResponse> GetChampion(Guid sessionId, ChampionQueryOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = options.ToParameters();
            return apiConnection.Get<ContestantResponse>($"/sessions/{sessionId}/contest/champion", parameters, HttpMessageTransformer, cancellationToken);
        }

        public Task<ContestSelectionResponse> GetSelection(Guid sessionId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return apiConnection.Get<ContestSelectionResponse>($"/sessions/{sessionId}/contest/selection", null, HttpMessageTransformer, cancellationToken);
        }
    }
}