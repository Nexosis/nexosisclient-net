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

        public Task<SessionResponse> CreateForecast(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return CreateForecast(input, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> CreateForecast(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl)
        {
            return CreateForecast(input, targetColumn, startDate, endDate, statusCallbackUrl, null);
        }

        public Task<SessionResponse> CreateForecast(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return CreateForecast(input, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> CreateForecast(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNull(input, nameof(input));
            Argument.IsNotNullOrEmpty(targetColumn, nameof(targetColumn));

            return CreateSessionInternal("sessions/forecast", input, null /* eventName */, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, cancellationToken, isEstimate: false);
        }

        public Task<SessionResponse> CreateForecast(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return CreateForecast(data, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> CreateForecast(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl)
        {
            return CreateForecast(data, targetColumn, startDate, endDate, statusCallbackUrl, null);
        }

        public Task<SessionResponse> CreateForecast(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return CreateForecast(data, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> CreateForecast(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNull(data, nameof(data));
            Argument.IsNotNullOrEmpty(targetColumn, nameof(targetColumn));

            return CreateSessionInternal("sessions/forecast", data, null /* eventName */, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, cancellationToken, isEstimate: false);
        }

        public Task<SessionResponse> CreateForecast(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return CreateForecast(dataSetName, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> CreateForecast(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl)
        {
            return CreateForecast(dataSetName, targetColumn, startDate, endDate, statusCallbackUrl, null);
        }

        public Task<SessionResponse> CreateForecast(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return CreateForecast(dataSetName, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> CreateForecast(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));
            Argument.IsNotNullOrEmpty(targetColumn, nameof(targetColumn));

            return CreateSessionInternal("sessions/forecast", dataSetName, null /* eventName */, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, cancellationToken, isEstimate: false);
        }

        public Task<SessionResponse> AnalyzeImpact(StreamReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            return AnalyzeImpact(input, eventName, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> AnalyzeImpact(StreamReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl)
        {
            return AnalyzeImpact(input, eventName, targetColumn, startDate, endDate, statusCallbackUrl, null);
        }

        public Task<SessionResponse> AnalyzeImpact(StreamReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return AnalyzeImpact(input, eventName, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> AnalyzeImpact(StreamReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNull(input, nameof(input));
            Argument.IsNotNullOrEmpty(targetColumn, nameof(targetColumn));
            Argument.IsNotNullOrEmpty(eventName, nameof(eventName));

            return CreateSessionInternal("sessions/impact", input, eventName, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, cancellationToken, isEstimate: false);
        }

        public Task<SessionResponse> AnalyzeImpact(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            return AnalyzeImpact(data, eventName, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> AnalyzeImpact(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl)
        {
            return AnalyzeImpact(data, eventName, targetColumn, startDate, endDate, statusCallbackUrl, null);
        }

        public Task<SessionResponse> AnalyzeImpact(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return AnalyzeImpact(data, eventName, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> AnalyzeImpact(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            Argument.IsNotNull(data, nameof(data));
            Argument.IsNotNullOrEmpty(targetColumn, nameof(targetColumn));
            Argument.IsNotNullOrEmpty(eventName, nameof(eventName));

            return CreateSessionInternal("sessions/impact", data, eventName, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, cancellationToken, isEstimate: false);
        }

        public Task<SessionResponse> AnalyzeImpact(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return AnalyzeImpact(dataSetName, eventName, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> AnalyzeImpact(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl)
        {
            return AnalyzeImpact(dataSetName, eventName, targetColumn, startDate, endDate, statusCallbackUrl, null);
        }

        public Task<SessionResponse> AnalyzeImpact(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return AnalyzeImpact(dataSetName, eventName, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> AnalyzeImpact(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));
            Argument.IsNotNullOrEmpty(targetColumn, nameof(targetColumn));
            Argument.IsNotNullOrEmpty(eventName, nameof(eventName));

            return CreateSessionInternal("sessions/impact", dataSetName, eventName, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, cancellationToken, isEstimate: false);
        }

        public Task<SessionResponse> EstimateForecast(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return EstimateForecast(input, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> EstimateForecast(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return EstimateForecast(input, targetColumn, startDate, endDate, httpMessageTransformer);
        }

        public Task<SessionResponse> EstimateForecast(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNull(input, nameof(input));
            Argument.IsNotNullOrEmpty(targetColumn, nameof(targetColumn));

            return CreateSessionInternal("sessions/forecast", input, null /* eventName */, targetColumn, startDate, endDate, null, httpMessageTransformer, cancellationToken, isEstimate: true);
        }

        public Task<SessionResponse> EstimateForecast(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return EstimateForecast(data, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> EstimateForecast(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return EstimateForecast(data, targetColumn, startDate, endDate, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> EstimateForecast(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNull(data, nameof(data));
            Argument.IsNotNullOrEmpty(targetColumn, nameof(targetColumn));

            return CreateSessionInternal("sessions/forecast", data, null /* eventName */, targetColumn, startDate, endDate, null, httpMessageTransformer, cancellationToken, isEstimate: true);
        }

        public Task<SessionResponse> EstimateForecast(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return EstimateForecast(dataSetName, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> EstimateForecast(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return EstimateForecast(dataSetName, targetColumn, startDate, endDate, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> EstimateForecast(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));
            Argument.IsNotNullOrEmpty(targetColumn, nameof(targetColumn));

            return CreateSessionInternal("sessions/forecast", dataSetName, null /* eventName */, targetColumn, startDate, endDate, null, httpMessageTransformer, cancellationToken, isEstimate: true);
        }

        public Task<SessionResponse> EstimateImpact(StreamReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            return EstimateImpact(input, eventName, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> EstimateImpact(StreamReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return EstimateImpact(input, eventName, targetColumn, startDate, endDate, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> EstimateImpact(StreamReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNull(input, nameof(input));
            Argument.IsNotNullOrEmpty(targetColumn, nameof(targetColumn));
            Argument.IsNotNullOrEmpty(eventName, nameof(eventName));

            return CreateSessionInternal("sessions/impact", input, eventName, targetColumn, startDate, endDate, null, httpMessageTransformer, cancellationToken, isEstimate: true);
        }

        public Task<SessionResponse> EstimateImpact(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            return EstimateImpact(data, eventName, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> EstimateImpact(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return EstimateImpact(data, eventName, targetColumn, startDate, endDate, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> EstimateImpact(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNull(data, nameof(data));
            Argument.IsNotNullOrEmpty(targetColumn, nameof(targetColumn));
            Argument.IsNotNullOrEmpty(eventName, nameof(eventName));

            return CreateSessionInternal("sessions/impact", data, eventName, targetColumn, startDate, endDate, null, httpMessageTransformer, cancellationToken, isEstimate: true);
        }

        public Task<SessionResponse> EstimateImpact(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return EstimateImpact(dataSetName, eventName, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> EstimateImpact(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return EstimateImpact(dataSetName, eventName, targetColumn, startDate, endDate, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> EstimateImpact(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));
            Argument.IsNotNullOrEmpty(targetColumn, nameof(targetColumn));
            Argument.IsNotNullOrEmpty(eventName, nameof(eventName));

            return CreateSessionInternal("sessions/impact", dataSetName, eventName, targetColumn, startDate, endDate, null, httpMessageTransformer, cancellationToken, isEstimate: true);
        }

        private Task<SessionResponse> CreateSessionInternal(string path, StreamReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken, bool isEstimate)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(targetColumn), targetColumn },
                { nameof(startDate), startDate.ToString("O") },
                { nameof(endDate), endDate.ToString("O") },
                { nameof(isEstimate), isEstimate.ToString().ToLowerInvariant() }
            };
            if (!String.IsNullOrEmpty(eventName))
            {
                parameters.Add(nameof(eventName), eventName);
            }
            if (!String.IsNullOrEmpty(statusCallbackUrl))
            {
                parameters.Add("callbackUrl", statusCallbackUrl);
            }
            return apiConnection.Post<SessionResponse>(path, parameters, input, httpMessageTransformer, cancellationToken);
        }

        private Task<SessionResponse> CreateSessionInternal(string path, IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken, bool isEstimate)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(targetColumn), targetColumn },
                { nameof(startDate), startDate.ToString("O") },
                { nameof(endDate), endDate.ToString("O") },
                { nameof(isEstimate), isEstimate.ToString().ToLowerInvariant() }
            };
            if (!String.IsNullOrEmpty(eventName))
            {
                parameters.Add(nameof(eventName), eventName);
            }
            if (!String.IsNullOrEmpty(statusCallbackUrl))
            {
                parameters.Add("callbackUrl", statusCallbackUrl);
            }
            return apiConnection.Post<SessionResponse>(path, parameters, new { data }, httpMessageTransformer, cancellationToken);
        }

        private Task<SessionResponse> CreateSessionInternal(string path, string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken, bool isEstimate)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(dataSetName), dataSetName },
                { nameof(targetColumn), targetColumn },
                { nameof(startDate), startDate.ToString("O") },
                { nameof(endDate), endDate.ToString("O") },
                { nameof(isEstimate), isEstimate.ToString().ToLowerInvariant() }
            };
            if (!String.IsNullOrEmpty(eventName))
            {
                parameters.Add(nameof(eventName), eventName);
            }
            if (!String.IsNullOrEmpty(statusCallbackUrl))
            {
                parameters.Add("callbackUrl", statusCallbackUrl);
            }
            return apiConnection.Post<SessionResponse>(path, parameters, (object)null, httpMessageTransformer, cancellationToken);
        }

        public Task<List<SessionResponse>> List()
        {
            return ListSessionsInternal(new Dictionary<string, string>(), null, CancellationToken.None);
        }

        public Task<List<SessionResponse>> List(string dataSetName)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(dataSetName), dataSetName },
            };
            return ListSessionsInternal(parameters, null, CancellationToken.None);
        }

        public Task<List<SessionResponse>> List(string dataSetName, string eventName)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(dataSetName), dataSetName },
                { nameof(eventName), eventName },
            };
            return ListSessionsInternal(parameters, null, CancellationToken.None);
        }

        public Task<List<SessionResponse>> List(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return List(dataSetName, eventName, startDate, endDate, null, CancellationToken.None);
        }

        public Task<List<SessionResponse>> List(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return List(dataSetName, eventName, startDate, endDate, httpMessageTransformer, CancellationToken.None);
        }

        public Task<List<SessionResponse>> List(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string> {
                { nameof(dataSetName), dataSetName },
                { nameof(eventName), eventName },
                { nameof(startDate), startDate.ToString("O") },
                { nameof(endDate), endDate.ToString("O") }
            };

            return ListSessionsInternal(parameters, httpMessageTransformer, cancellationToken);
        }

        // we are returning just the list object, so we need a wrapper here and will then pull the results off of it.
        private class SessionResponseDto
        {
            [JsonProperty("items", Required = Required.Always )]
            public List<SessionResponse> items { get; set; }
        }
        private async Task<List<SessionResponse>> ListSessionsInternal(IDictionary<string, string> parameters,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var response = await apiConnection.Get<SessionResponseDto>("sessions", parameters, httpMessageTransformer, cancellationToken);
            return response?.items;
        }

        public async Task Remove()
        {
            await Remove((string)null);
        }

        public async Task Remove(string dataSetName)
        {
            await Remove(dataSetName, null);
        }

        public async Task Remove(SessionType? type)
        {
            await Remove(null, null, type);
        }

        public async Task Remove(string dataSetName, SessionType? type)
        {
            await Remove(dataSetName, null, type);
        }

        public async Task Remove(string dataSetName, string eventName, SessionType? type)
        {
            var parameters = new Dictionary<string, string>();
            if (!String.IsNullOrEmpty(dataSetName))
            {
                parameters.Add(nameof(dataSetName), dataSetName); 
            }
            if (!String.IsNullOrEmpty(eventName))
            {
                parameters.Add(nameof(eventName), eventName); 
            }
            if (type.HasValue)
            {
                parameters.Add(nameof(type), type.Value.ToString());
            }
            await RemoveSessionsInternal(parameters, null, CancellationToken.None);
        }

        public async Task Remove(string dataSetName, string eventName, SessionType? type, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            await Remove(dataSetName, eventName, type, startDate, endDate, null);
        }

        public async Task Remove(string dataSetName, string eventName, SessionType? type, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            await Remove(dataSetName, eventName, type, startDate, endDate, httpMessageTransformer, CancellationToken.None);
        }

        public async Task Remove(string dataSetName, string eventName, SessionType? type, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string> {
                { nameof(startDate), startDate.ToString("O") },
                { nameof(endDate), endDate.ToString("O") }
            };
            if (!String.IsNullOrEmpty(dataSetName))
            {
                parameters.Add(nameof(dataSetName), dataSetName); 
            }
            if (!String.IsNullOrEmpty(eventName))
            {
                parameters.Add(nameof(eventName), eventName); 
            }
            if (type.HasValue)
            {
                parameters.Add(nameof(type), type.Value.ToString());
            }

            await RemoveSessionsInternal(parameters, httpMessageTransformer, cancellationToken);
        }

        private async Task RemoveSessionsInternal(IDictionary<string, string> parameters, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            await apiConnection.Delete("sessions", parameters, httpMessageTransformer, cancellationToken);
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
                    response.Content = new StringContent(JsonConvert.SerializeObject(new { SessionId = id, Status = response.Headers.GetValues("Nexosis-Session-Status").FirstOrDefault()}));
                }

                httpMessageTransformer?.Invoke(request, response);
            };

            return apiConnection.Head<SessionResultStatus>($"sessions/{id}", null, localTransform, cancellationToken);
        }

        public async Task Remove(Guid id)
        {
            await Remove(id, null);
        }

        public async Task Remove(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            await Remove(id, httpMessageTransformer, CancellationToken.None);
        }

        public async Task Remove(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            await apiConnection.Delete($"sessions/{id}", null, httpMessageTransformer, cancellationToken);
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
            await GetResults(id, output, null);
        }

        public async Task GetResults(Guid id, StreamWriter output, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            await GetResults(id, output, httpMessageTransformer, CancellationToken.None);
        }

        public async Task GetResults(Guid id, StreamWriter output, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNull(output, nameof(output));
            await apiConnection.Get($"sessions/{id}/results", null, httpMessageTransformer, cancellationToken, output, "text/csv");
        }
    }
}
