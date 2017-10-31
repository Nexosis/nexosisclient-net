using System;
using System.Collections.Generic;
using System.IO;
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

        public SessionClient(ApiConnection apiConnection)
        {
            this.apiConnection = apiConnection;
        }

        public Task<SessionResponse> CreateForecast(SessionDetail data, DateTimeOffset startDate, DateTimeOffset endDate, ResultInterval resultInterval)
        {
            return CreateForecast(data, startDate, endDate, resultInterval, null);
        }

        public Task<SessionResponse> CreateForecast(SessionDetail data, DateTimeOffset startDate, DateTimeOffset endDate, ResultInterval resultInterval,
            string statusCallbackUrl)
        {
            return CreateForecast(data, startDate, endDate, resultInterval, statusCallbackUrl, null);
        }

        public Task<SessionResponse> CreateForecast(SessionDetail data, DateTimeOffset startDate, DateTimeOffset endDate, ResultInterval resultInterval,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return CreateForecast(data, startDate, endDate, resultInterval, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> CreateForecast(SessionDetail data, DateTimeOffset startDate, DateTimeOffset endDate, ResultInterval resultInterval,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNull(data, nameof(data));
            Argument.IsNotNullOrEmpty(data.DataSetName, "data.DataSetName");

            return CreateSessionInternal("sessions/forecast", data, null /* eventName */, startDate, endDate, resultInterval, statusCallbackUrl, httpMessageTransformer, cancellationToken, isEstimate: false);
        }

        public Task<SessionResponse> CreateForecast(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, ResultInterval resultInterval)
        {
            return CreateForecast(dataSetName, targetColumn, startDate, endDate, resultInterval, null);
        }

        public Task<SessionResponse> CreateForecast(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, ResultInterval resultInterval,
            string statusCallbackUrl)
        {
            return CreateForecast(dataSetName, targetColumn, startDate, endDate, resultInterval, statusCallbackUrl, null);
        }

        public Task<SessionResponse> CreateForecast(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, ResultInterval resultInterval,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return CreateForecast(dataSetName, targetColumn, startDate, endDate, resultInterval, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> CreateForecast(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, ResultInterval resultInterval,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));
            Argument.IsNotNullOrEmpty(targetColumn, nameof(targetColumn));

            var data = new SessionDetail
            {
                DataSetName = dataSetName,
                Columns = new Dictionary<string, ColumnMetadata>()
                {
                    { targetColumn, new ColumnMetadata { Role = ColumnRole.Target } }
                }
            };

            return CreateForecast(data, startDate, endDate, resultInterval, statusCallbackUrl, httpMessageTransformer, cancellationToken);
        }

        public Task<SessionResponse> AnalyzeImpact(SessionDetail data, string eventName, DateTimeOffset startDate,
            DateTimeOffset endDate, ResultInterval resultInterval)
        {
            return AnalyzeImpact(data, eventName, startDate, endDate, resultInterval, null);
        }

        public Task<SessionResponse> AnalyzeImpact(SessionDetail data, string eventName, DateTimeOffset startDate,
            DateTimeOffset endDate, ResultInterval resultInterval, string statusCallbackUrl)
        {
            return AnalyzeImpact(data, eventName, startDate, endDate, resultInterval, statusCallbackUrl, null);
        }

        public Task<SessionResponse> AnalyzeImpact(SessionDetail data, string eventName, DateTimeOffset startDate,
            DateTimeOffset endDate, ResultInterval resultInterval, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return AnalyzeImpact(data, eventName, startDate, endDate, resultInterval, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> AnalyzeImpact(SessionDetail data, string eventName, DateTimeOffset startDate,
            DateTimeOffset endDate, ResultInterval resultInterval, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            Argument.IsNotNull(data, nameof(data));
            Argument.IsNotNullOrEmpty(data.DataSetName, "data.DataSetName");
            Argument.IsNotNullOrEmpty(eventName, nameof(eventName));

            return CreateSessionInternal("sessions/impact", data, eventName, startDate, endDate, resultInterval, statusCallbackUrl, httpMessageTransformer, cancellationToken, isEstimate: false);
        }

        public Task<SessionResponse> AnalyzeImpact(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, ResultInterval resultInterval)
        {
            return AnalyzeImpact(dataSetName, eventName, targetColumn, startDate, endDate, resultInterval, null);
        }

        public Task<SessionResponse> AnalyzeImpact(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, ResultInterval resultInterval, string statusCallbackUrl)
        {
            return AnalyzeImpact(dataSetName, eventName, targetColumn, startDate, endDate, resultInterval, statusCallbackUrl, null);
        }

        public Task<SessionResponse> AnalyzeImpact(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, ResultInterval resultInterval, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return AnalyzeImpact(dataSetName, eventName, targetColumn, startDate, endDate, resultInterval, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> AnalyzeImpact(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            ResultInterval resultInterval, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));
            Argument.IsNotNullOrEmpty(targetColumn, nameof(targetColumn));

            var data = new SessionDetail
            {
                DataSetName = dataSetName,
                Columns = new Dictionary<string, ColumnMetadata>()
                {
                    { targetColumn, new ColumnMetadata { Role = ColumnRole.Target } }
                }
            };

            return AnalyzeImpact(data, eventName, startDate, endDate, resultInterval, statusCallbackUrl, httpMessageTransformer, cancellationToken);
        }

        public Task<SessionResponse> TrainModel(ModelSessionDetail data)
        {
            return TrainModel(data, null);
        }

        public Task<SessionResponse> TrainModel(ModelSessionDetail data,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return TrainModel(data, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> TrainModel(ModelSessionDetail data,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNull(data, nameof(data));
            Argument.IsNotNullOrEmpty(data.DataSourceName, "data.DataSetName");

            return CreateSessionInternal("sessions/model", data, httpMessageTransformer, cancellationToken, false);
        }

        public Task<SessionResponse> TrainModel(string dataSourceName, string targetColumn, PredictionDomain predictionDomain)
        {
            return TrainModel(dataSourceName, targetColumn, predictionDomain, null);
        }

        public Task<SessionResponse> TrainModel(string dataSourceName, string targetColumn, PredictionDomain predictionDomain, string statusCallbackUrl)
        {
            return TrainModel(dataSourceName, targetColumn, predictionDomain, statusCallbackUrl, null);
        }

        public Task<SessionResponse> TrainModel(string dataSourceName, string targetColumn, PredictionDomain predictionDomain, string statusCallbackUrl,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return TrainModel(dataSourceName, targetColumn, predictionDomain, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> TrainModel(string dataSourceName, string targetColumn, PredictionDomain predictionDomain, string statusCallbackUrl,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNullOrEmpty(dataSourceName, nameof(dataSourceName));
            Argument.IsNotNullOrEmpty(targetColumn, nameof(targetColumn));

            var data = new ModelSessionDetail
            {
                DataSourceName = dataSourceName,
                Columns = new Dictionary<string, ColumnMetadata>()
                {
                    { targetColumn, new ColumnMetadata { Role = ColumnRole.Target } }
                },
                PredictionDomain = predictionDomain,
                CallbackUrl = statusCallbackUrl,
            };

            return TrainModel(data, httpMessageTransformer, cancellationToken);
        }

        public Task<SessionResponse> EstimateForecast(SessionDetail data, DateTimeOffset startDate, DateTimeOffset endDate, ResultInterval resultInterval)
        {
            return EstimateForecast(data, startDate, endDate, resultInterval, null);
        }

        public Task<SessionResponse> EstimateForecast(SessionDetail data, DateTimeOffset startDate, DateTimeOffset endDate, ResultInterval resultInterval,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return EstimateForecast(data, startDate, endDate, resultInterval, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> EstimateForecast(SessionDetail data, DateTimeOffset startDate, DateTimeOffset endDate, ResultInterval resultInterval,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNull(data, nameof(data));
            Argument.IsNotNullOrEmpty(data.DataSetName, "data.DataSetName");

            return CreateSessionInternal("sessions/forecast", data, null /* eventName */, startDate, endDate, resultInterval, null, httpMessageTransformer, cancellationToken, isEstimate: true);
        }

        public Task<SessionResponse> EstimateForecast(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, ResultInterval resultInterval)
        {
            return EstimateForecast(dataSetName, targetColumn, startDate, endDate, resultInterval, null);
        }

        public Task<SessionResponse> EstimateForecast(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, ResultInterval resultInterval,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return EstimateForecast(dataSetName, targetColumn, startDate, endDate, resultInterval, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> EstimateForecast(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            ResultInterval resultInterval, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));
            Argument.IsNotNullOrEmpty(targetColumn, nameof(targetColumn));

            var data = new SessionDetail
            {
                DataSetName = dataSetName,
                Columns = new Dictionary<string, ColumnMetadata>()
                {
                    { targetColumn, new ColumnMetadata { Role = ColumnRole.Target } }
                }
            };

            return EstimateForecast(data, startDate, endDate, resultInterval, httpMessageTransformer, cancellationToken);
        }

        public Task<SessionResponse> EstimateImpact(SessionDetail data, string eventName, DateTimeOffset startDate,
            DateTimeOffset endDate, ResultInterval resultInterval)
        {
            return EstimateImpact(data, eventName, startDate, endDate, resultInterval, null);
        }

        public Task<SessionResponse> EstimateImpact(SessionDetail data, string eventName, DateTimeOffset startDate,
            DateTimeOffset endDate, ResultInterval resultInterval, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return EstimateImpact(data, eventName, startDate, endDate, resultInterval, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> EstimateImpact(SessionDetail data, string eventName, DateTimeOffset startDate, DateTimeOffset endDate,
            ResultInterval resultInterval, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNull(data, nameof(data));
            Argument.IsNotNullOrEmpty(data.DataSetName, "data.DataSetName");
            Argument.IsNotNullOrEmpty(eventName, nameof(eventName));

            return CreateSessionInternal("sessions/impact", data, eventName, startDate, endDate, resultInterval, null, httpMessageTransformer, cancellationToken, isEstimate: true);
        }

        public Task<SessionResponse> EstimateImpact(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, ResultInterval resultInterval)
        {
            return EstimateImpact(dataSetName, eventName, targetColumn, startDate, endDate, resultInterval, null);
        }

        public Task<SessionResponse> EstimateImpact(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, ResultInterval resultInterval, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return EstimateImpact(dataSetName, eventName, targetColumn, startDate, endDate, resultInterval, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> EstimateImpact(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, ResultInterval resultInterval, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));
            Argument.IsNotNullOrEmpty(targetColumn, nameof(targetColumn));

            var data = new SessionDetail
            {
                DataSetName = dataSetName,
                Columns = new Dictionary<string, ColumnMetadata>()
                {
                    { targetColumn, new ColumnMetadata { Role = ColumnRole.Target } }
                }
            };

            return EstimateImpact(data, eventName, startDate, endDate, resultInterval, httpMessageTransformer, cancellationToken);
        }

        public Task EstimateTrainModel(ModelSessionDetail data)
        {
            return EstimateTrainModel(data, null);
        }

        public Task EstimateTrainModel(ModelSessionDetail data,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return EstimateTrainModel(data, httpMessageTransformer, CancellationToken.None);
        }

        public Task EstimateTrainModel(ModelSessionDetail data,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNull(data, nameof(data));
            Argument.IsNotNullOrEmpty(data.DataSourceName, "data.DataSetName");

            return CreateSessionInternal("sessions/model", data, httpMessageTransformer, cancellationToken, true);
        }

        public Task EstimateTrainModel(string dataSourceName, string targetColumn, PredictionDomain predictionDomain)
        {
            return EstimateTrainModel(dataSourceName, targetColumn, predictionDomain, null);
        }

        public Task EstimateTrainModel(string dataSourceName, string targetColumn, PredictionDomain predictionDomain, string statusCallbackUrl)
        {
            return EstimateTrainModel(dataSourceName, targetColumn, predictionDomain, statusCallbackUrl, null);
        }

        public Task EstimateTrainModel(string dataSourceName, string targetColumn, PredictionDomain predictionDomain, string statusCallbackUrl,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return EstimateTrainModel(dataSourceName, targetColumn, predictionDomain, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task EstimateTrainModel(string dataSourceName, string targetColumn, PredictionDomain predictionDomain, string statusCallbackUrl,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNullOrEmpty(dataSourceName, nameof(dataSourceName));
            Argument.IsNotNullOrEmpty(targetColumn, nameof(targetColumn));

            var data = new ModelSessionDetail
            {
                DataSourceName = dataSourceName,
                Columns = new Dictionary<string, ColumnMetadata>()
                {
                    { targetColumn, new ColumnMetadata { Role = ColumnRole.Target } }
                },
                PredictionDomain = predictionDomain,
                CallbackUrl = statusCallbackUrl,
            };

            return EstimateTrainModel(data, httpMessageTransformer, cancellationToken);
        }

        private Task<SessionResponse> CreateSessionInternal(string path, SessionDetail data, string eventName, DateTimeOffset startDate, DateTimeOffset endDate,
            ResultInterval resultInterval, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken, bool isEstimate)
        {
            var parameters = new Dictionary<string, string>
            {
                { "dataSetName", data.DataSetName },
                { nameof(startDate), startDate.ToString("O") },
                { nameof(endDate), endDate.ToString("O") },
                { nameof(isEstimate), isEstimate.ToString().ToLowerInvariant() },
                { nameof(resultInterval), resultInterval.ToString().ToLowerInvariant() }
            };
            if (!string.IsNullOrEmpty(eventName))
            {
                parameters.Add(nameof(eventName), eventName);
            }
            if (!string.IsNullOrEmpty(statusCallbackUrl))
            {
                parameters.Add("callbackUrl", statusCallbackUrl);
            }
            return apiConnection.Post<SessionResponse>(path, parameters, data, httpMessageTransformer, cancellationToken);
        }

        private Task<SessionResponse> CreateSessionInternal(string path, ModelSessionDetail data,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken, bool isEstimate)
        {
            data.IsEstimate = isEstimate;

            return apiConnection.Post<SessionResponse>(path, null, data, httpMessageTransformer, cancellationToken);
        }

        public Task<List<SessionResponse>> List(int pageNumber = 0, int pageSize = NexosisClient.DefaultPageSize)
        {
            return ListSessionsInternal(BuildListParameters(null, pageNumber, pageSize), null, CancellationToken.None);
        }

        public Task<List<SessionResponse>> List(string dataSetName, int pageNumber = 0, int pageSize = NexosisClient.DefaultPageSize)
        {
            return ListSessionsInternal(BuildListParameters(dataSetName, pageNumber, pageSize), null, CancellationToken.None);
        }

        public Task<List<SessionResponse>> List(string dataSetName, string eventName, int pageNumber = 0, int pageSize = NexosisClient.DefaultPageSize)
        {
            return ListSessionsInternal(BuildListParameters(dataSetName, pageNumber, pageSize, eventName), null, CancellationToken.None);
        }

        public Task<List<SessionResponse>> List(string dataSetName, string eventName, DateTimeOffset requestedAfterDate, DateTimeOffset requestedBeforeDate, int pageNumber = 0, int pageSize = NexosisClient.DefaultPageSize)
        {
            return ListSessionsInternal(BuildListParameters(dataSetName, pageNumber, pageSize, eventName, requestedAfterDate, requestedBeforeDate), null, CancellationToken.None);
        }

        public Task<List<SessionResponse>> List(string dataSetName, string eventName, DateTimeOffset requestedAfterDate, DateTimeOffset requestedBeforeDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer
            , int pageNumber = 0, int pageSize = NexosisClient.DefaultPageSize)
        {
            return ListSessionsInternal(BuildListParameters(dataSetName, pageNumber, pageSize, eventName, requestedAfterDate, requestedBeforeDate), httpMessageTransformer, CancellationToken.None);
        }

        public Task<List<SessionResponse>> List(string dataSetName, string eventName, DateTimeOffset requestedAfterDate, DateTimeOffset requestedBeforeDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken
            , int pageNumber = 0, int pageSize = NexosisClient.DefaultPageSize)
        {
            return ListSessionsInternal(BuildListParameters(dataSetName, pageNumber, pageSize, eventName, requestedAfterDate, requestedBeforeDate), httpMessageTransformer, cancellationToken);
        }

        private Dictionary<string, string> BuildListParameters(string dataSetName, int page, int pageSize, string eventName = null, DateTimeOffset? requestedAfterDate = null, DateTimeOffset? requestedBeforeDate = null)
        {
            var parameters = new Dictionary<string, string>{
                { nameof(page), page.ToString()},
                { nameof(pageSize), pageSize.ToString()}
            };
            if (String.IsNullOrEmpty(dataSetName) == false)
                parameters.Add(nameof(dataSetName), dataSetName);
            if (String.IsNullOrEmpty(eventName) == false)
                parameters.Add(nameof(eventName), eventName);
            if (requestedAfterDate.HasValue)
                parameters.Add(nameof(requestedAfterDate), requestedAfterDate.Value.ToString("O"));
            if (requestedBeforeDate.HasValue)
                parameters.Add(nameof(requestedBeforeDate), requestedBeforeDate.Value.ToString("O"));
            return parameters;
        }

        // we are returning just the list object, so we need a wrapper here and will then pull the results off of it.
        private class SessionResponseDto : IPagedList<SessionResponse>
        {
            [JsonProperty("items", Required = Required.Always)]
            public List<SessionResponse> Items { get; set; }
            public int PageSize { get; set; }
            public int PageNumber { get; set; }
            public int TotalPages { get; set; }
            public int TotalCount { get; set; }
            public List<Link> Links { get; set; }
        }
        private async Task<List<SessionResponse>> ListSessionsInternal(IDictionary<string, string> parameters,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var response = await apiConnection.Get<SessionResponseDto>("sessions", parameters, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
            return new PagedList<SessionResponse>(response);
        }

        public async Task Remove()
        {
            await Remove((string)null).ConfigureAwait(false);
        }

        public async Task Remove(string dataSetName)
        {
            await Remove(dataSetName, null).ConfigureAwait(false);
        }

        public async Task Remove(SessionType? type)
        {
            await Remove(null, null, type).ConfigureAwait(false);
        }

        public async Task Remove(string dataSetName, SessionType? type)
        {
            await Remove(dataSetName, null, type).ConfigureAwait(false);
        }

        public async Task Remove(string dataSetName, string eventName, SessionType? type)
        {
            var parameters = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(dataSetName))
            {
                parameters.Add(nameof(dataSetName), dataSetName);
            }
            if (!string.IsNullOrEmpty(eventName))
            {
                parameters.Add(nameof(eventName), eventName);
            }
            if (type.HasValue)
            {
                parameters.Add(nameof(type), type.Value.ToString());
            }
            await RemoveSessionsInternal(parameters, null, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task Remove(string dataSetName, string eventName, SessionType? type, DateTimeOffset requestedAfterDate, DateTimeOffset requestedBeforeDate)
        {
            await Remove(dataSetName, eventName, type, requestedAfterDate, requestedBeforeDate, null).ConfigureAwait(false);
        }

        public async Task Remove(string dataSetName, string eventName, SessionType? type, DateTimeOffset requestedAfterDate, DateTimeOffset requestedBeforeDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            await Remove(dataSetName, eventName, type, requestedAfterDate, requestedBeforeDate, httpMessageTransformer, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task Remove(string dataSetName, string eventName, SessionType? type, DateTimeOffset requestedAfterDate, DateTimeOffset requestedBeforeDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string> {
                { nameof(requestedAfterDate), requestedAfterDate.ToString("O") },
                { nameof(requestedBeforeDate), requestedBeforeDate.ToString("O") }
            };
            if (!string.IsNullOrEmpty(dataSetName))
            {
                parameters.Add(nameof(dataSetName), dataSetName);
            }
            if (!string.IsNullOrEmpty(eventName))
            {
                parameters.Add(nameof(eventName), eventName);
            }
            if (type.HasValue)
            {
                parameters.Add(nameof(type), type.Value.ToString());
            }

            await RemoveSessionsInternal(parameters, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }

        private async Task RemoveSessionsInternal(IDictionary<string, string> parameters, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            await apiConnection.Delete("sessions", parameters, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }

        public Task<SessionResponse> Get(Guid id)
        {
            return Get(id, null);
        }

        public Task<SessionResponse> Get(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return Get(id, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> Get(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            return apiConnection.Get<SessionResponse>($"sessions/{id}", null, httpMessageTransformer, cancellationToken);
        }

        public Task<SessionResultStatus> GetStatus(Guid id)
        {
            return GetStatus(id, null);
        }

        public Task<SessionResultStatus> GetStatus(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return GetStatus(id, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResultStatus> GetStatus(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Action<HttpRequestMessage, HttpResponseMessage> localTransform = (HttpRequestMessage request, HttpResponseMessage response) =>
            {
                if (response != null && response.IsSuccessStatusCode && response.Headers.Contains("Nexosis-Session-Status"))
                {
                    response.Content = new StringContent(JsonConvert.SerializeObject(new { SessionId = id, Status = response.Headers.GetValues("Nexosis-Session-Status").FirstOrDefault() }));
                }

                httpMessageTransformer?.Invoke(request, response);
            };

            return apiConnection.Head<SessionResultStatus>($"sessions/{id}", null, localTransform, cancellationToken);
        }

        public async Task Remove(Guid id)
        {
            await Remove(id, null).ConfigureAwait(false);
        }

        public async Task Remove(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            await Remove(id, httpMessageTransformer, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task Remove(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            await apiConnection.Delete($"sessions/{id}", null, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }

        public Task<SessionResult> GetResults(Guid id)
        {
            return GetResults(id, (Action<HttpRequestMessage, HttpResponseMessage>)null);
        }

        public Task<SessionResult> GetResults(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return GetResults(id, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResult> GetResults(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            return apiConnection.Get<SessionResult>($"sessions/{id}/results", null, httpMessageTransformer, cancellationToken);
        }

        public async Task GetResults(Guid id, StreamWriter output)
        {
            await GetResults(id, output, null).ConfigureAwait(false);
        }

        public async Task GetResults(Guid id, StreamWriter output, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            await GetResults(id, output, httpMessageTransformer, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task GetResults(Guid id, StreamWriter output, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNull(output, nameof(output));
            await apiConnection.Get($"sessions/{id}/results", null, httpMessageTransformer, cancellationToken, output, "text/csv").ConfigureAwait(false);
        }
    }
}
