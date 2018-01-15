using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{

    public class ModelClient : IModelClient
    {
        private readonly ApiConnection apiConnection;

        public ModelClient(ApiConnection apiConnection)
        {
            this.apiConnection = apiConnection;
        }

        public Action<HttpRequestMessage, HttpResponseMessage> HttpMessageTransformer { get; set; }

        public async Task<ModelSummary> Get(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await apiConnection
                .Get<ModelSummary>($"models/{id}", null, HttpMessageTransformer, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<ModelSummaryList> List(ModelSummaryQuery query = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = query.ToParameters();
            var response = await apiConnection
                .Get<ModelSummaryList>("models", parameters, HttpMessageTransformer, cancellationToken)
                .ConfigureAwait(false);

            return response;
        }

        public Task<ModelPredictionResult> Predict(ModelPredictionRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            Argument.IsNotNull(request.Data, nameof(ModelPredictionRequest.Data));

            var requestBody = new
            {
                request.Data,
                request.ExtraParameters
            };

            return apiConnection.Post<ModelPredictionResult>($"models/{request.ModelId}/predict", null, requestBody,
                HttpMessageTransformer, cancellationToken);
        }

        public async Task Remove(ModelRemoveCriteria criteria, CancellationToken cancellationToken = default(CancellationToken))
        {
            Argument.OneOfIsNotNullOrEmpty(
                Tuple.Create(criteria?.DataSourceName, nameof(ModelRemoveCriteria.DataSourceName)),
                Tuple.Create(criteria?.ModelId.ToString(), nameof(ModelRemoveCriteria.ModelId))
            );

            if (criteria.ModelId.HasValue)
            {
                await apiConnection.Delete($"models/{criteria.ModelId}", null, HttpMessageTransformer, cancellationToken)
                    .ConfigureAwait(false);
            }
            else
            {
                var parameters = criteria.ToParameters();

                await apiConnection.Delete("models", parameters, HttpMessageTransformer, cancellationToken)
                    .ConfigureAwait(false);
            }


        }
    }
}
