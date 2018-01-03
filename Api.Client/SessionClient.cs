using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{

    public class SessionClient : ISessionClient
    {
        private readonly ApiConnection apiConnection;
        private readonly ContestClient contestClient;


        public SessionClient(ApiConnection apiConnection)
        {
            this.apiConnection = apiConnection;
            this.contestClient = new ContestClient(apiConnection);
        }

        public IContestClient Contest => contestClient;

        private Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer;

        public Action<HttpRequestMessage, HttpResponseMessage> HttpMessageTransformer
        {
            get => httpMessageTransformer;
            set
            {
                httpMessageTransformer = value;
                contestClient.HttpMessageTransformer = value;
            }
        }

        public Task<SessionResponse> CreateForecast(ForecastSessionRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            Argument.IsNotNullOrEmpty(request?.DataSourceName, "dataSourceName");

            return apiConnection.Post<SessionResponse>("/sessions/forecast", null, request, HttpMessageTransformer, cancellationToken);
        }

        public Task<SessionResponse> AnalyzeImpact(ImpactSessionRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            Argument.IsNotNullOrEmpty(request?.DataSourceName, "dataSourceName");
            Argument.IsNotNullOrEmpty(request?.EventName, "eventName");

            return apiConnection.Post<SessionResponse>("/sessions/impact", null, request, HttpMessageTransformer, cancellationToken);
        }

        public Task<SessionResponse> TrainModel(ModelSessionRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            Argument.IsNotNullOrEmpty(request?.DataSourceName, "dataSourceName");

            return apiConnection.Post<SessionResponse>("/sessions/model", null, request, HttpMessageTransformer, cancellationToken);
        }

        public Task<SessionResponseList> List(SessionQuery query = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = query.ToParameters();

            return apiConnection.Get<SessionResponseList>("/sessions", parameters, HttpMessageTransformer, cancellationToken);
        }

        public Task Remove(SessionRemoveCriteria criteria, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = criteria.ToParameters();

            return apiConnection.Delete("/sessions", parameters, HttpMessageTransformer, cancellationToken);
        }

        public Task<SessionResponse> Get(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return apiConnection.Get<SessionResponse>($"/sessions/{id}", null, HttpMessageTransformer, cancellationToken);
        }

        public Task<SessionResultStatus> GetStatus(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            void LocalTransform(HttpRequestMessage request, HttpResponseMessage response)
            {
                if (response != null && response.IsSuccessStatusCode && response.Headers.Contains("Nexosis-Session-Status"))
                {
                    response.Content = new StringContent(JsonConvert.SerializeObject(new
                    {
                        SessionId = id,
                        Status = response.Headers.GetValues("Nexosis-Session-Status").FirstOrDefault()
                    }));
                }

                HttpMessageTransformer?.Invoke(request, response);
            }

            return apiConnection.Head<SessionResultStatus>($"sessions/{id}", null, LocalTransform, cancellationToken);
        }

        public Task Remove(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return apiConnection.Delete($"/sessions/{id}", null, HttpMessageTransformer, cancellationToken);
        }

        public Task<SessionResult> GetResults(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return apiConnection.Get<SessionResult>($"/sessions/{id}/results", null, HttpMessageTransformer, cancellationToken);
        }

        public Task<ConfusionMatrixResult> GetResultConfusionMatrix(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return apiConnection.Get<ConfusionMatrixResult>($"/sessions/{id}/results/confusionmatrix", null, HttpMessageTransformer,
                cancellationToken);
        }

        public Task<SessionResult> GetResultAnomalyScores(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return apiConnection.Get<SessionResult>($"/sessions/{id}/results/anomalyscores", null, HttpMessageTransformer,
                cancellationToken);
        }

        public Task<SessionResult> GetResultClassScores(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return apiConnection.Get<SessionResult>($"/sessions/{id}/results/classscores", null, HttpMessageTransformer,
                cancellationToken);
        }
    }
}
