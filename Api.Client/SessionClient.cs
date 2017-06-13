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

        public Task<SessionResponse> CreateForecastSession(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return CreateForecastSession(input, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> CreateForecastSession(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl)
        {
            return CreateForecastSession(input, targetColumn, startDate, endDate, statusCallbackUrl, null);
        }

        public Task<SessionResponse> CreateForecastSession(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return CreateForecastSession(input, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> CreateForecastSession(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input reader cannot be null.");
            if (String.IsNullOrEmpty(targetColumn))
                throw new ArgumentException("Must specify a target column to forecast on.", nameof(targetColumn));

            return CreateSessionInternal("sessions/forecast", input, null /* eventName */, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, cancellationToken, isEstimate: false);
        }

        public Task<SessionResponse> CreateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return CreateForecastSession(data, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> CreateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl)
        {
            return CreateForecastSession(data, targetColumn, startDate, endDate, statusCallbackUrl, null);
        }

        public Task<SessionResponse> CreateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return CreateForecastSession(data, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> CreateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (String.IsNullOrEmpty(targetColumn))
                throw new ArgumentException("Must specify a target column to forecast on.", nameof(targetColumn));

            return CreateSessionInternal("sessions/forecast", data, null /* eventName */, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, cancellationToken, isEstimate: false);
        }

        public Task<SessionResponse> CreateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return CreateForecastSession(dataSetName, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> CreateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl)
        {
            return CreateForecastSession(dataSetName, targetColumn, startDate, endDate, statusCallbackUrl, null);
        }

        public Task<SessionResponse> CreateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return CreateForecastSession(dataSetName, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> CreateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(dataSetName))
                throw new ArgumentException("Must specify a data set name to forecast on.", nameof(dataSetName));
            if (String.IsNullOrEmpty(targetColumn))
                throw new ArgumentException("Must specify a target column to forecast on.", nameof(targetColumn));

            return CreateSessionInternal("sessions/forecast", dataSetName, null /* eventName */, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, cancellationToken, isEstimate: false);
        }

        public Task<SessionResponse> CreateImpactSession(StreamReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            return CreateImpactSession(input, eventName, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> CreateImpactSession(StreamReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl)
        {
            return CreateImpactSession(input, eventName, targetColumn, startDate, endDate, statusCallbackUrl, null);
        }

        public Task<SessionResponse> CreateImpactSession(StreamReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return CreateImpactSession(input, eventName, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> CreateImpactSession(StreamReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Must give an input reader.");
            if (String.IsNullOrEmpty(eventName))
                throw new ArgumentException("Must specify an event name for this impact session.", nameof(eventName));
            if (String.IsNullOrEmpty(targetColumn))
                throw new ArgumentException("Must specify a target column for impact.", nameof(targetColumn));

            return CreateSessionInternal("sessions/impact", input, eventName, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, cancellationToken, isEstimate: false);
        }

        public Task<SessionResponse> CreateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            return CreateImpactSession(data, eventName, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> CreateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl)
        {
            return CreateImpactSession(data, eventName, targetColumn, startDate, endDate, statusCallbackUrl, null);
        }

        public Task<SessionResponse> CreateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return CreateImpactSession(data, eventName, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> CreateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data), "Must give data for impact session..");
            if (String.IsNullOrEmpty(eventName))
                throw new ArgumentException("Must specify an event name for this impact session.", nameof(eventName));
            if (String.IsNullOrEmpty(targetColumn))
                throw new ArgumentException("Must specify a target column for impact.", nameof(targetColumn));

            return CreateSessionInternal("sessions/impact", data, eventName, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, cancellationToken, isEstimate: false);
        }

        public Task<SessionResponse> CreateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return CreateImpactSession(dataSetName, eventName, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> CreateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl)
        {
            return CreateImpactSession(dataSetName, eventName, targetColumn, startDate, endDate, statusCallbackUrl, null);
        }

        public Task<SessionResponse> CreateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return CreateImpactSession(dataSetName, eventName, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> CreateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(dataSetName))
                throw new ArgumentException("Must specify a data set name for impact.", nameof(dataSetName));
            if (String.IsNullOrEmpty(eventName))
                throw new ArgumentException("Must specify an event name for impact.", nameof(eventName));
            if (String.IsNullOrEmpty(targetColumn))
                throw new ArgumentException("Must specify a target column for impact.", nameof(targetColumn));

            return CreateSessionInternal("sessions/impact", dataSetName, eventName, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, cancellationToken, isEstimate: false);
        }

        public Task<SessionResponse> EstimateForecastSession(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return EstimateForecastSession(input, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> EstimateForecastSession(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return EstimateForecastSession(input, targetColumn, startDate, endDate, httpMessageTransformer);
        }

        public Task<SessionResponse> EstimateForecastSession(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input reader cannot be null.");
            if (String.IsNullOrEmpty(targetColumn))
                throw new ArgumentException("Must specify a target column to forecast on.", nameof(targetColumn));

            return CreateSessionInternal("sessions/forecast", input, null /* eventName */, targetColumn, startDate, endDate, null, httpMessageTransformer, cancellationToken, isEstimate: true);
        }

        public Task<SessionResponse> EstimateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return EstimateForecastSession(data, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> EstimateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return EstimateForecastSession(data, targetColumn, startDate, endDate, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> EstimateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (String.IsNullOrEmpty(targetColumn))
                throw new ArgumentException("Must specify a target column to forecast on.", nameof(targetColumn));

            return CreateSessionInternal("sessions/forecast", data, null /* eventName */, targetColumn, startDate, endDate, null, httpMessageTransformer, cancellationToken, isEstimate: true);
        }

        public Task<SessionResponse> EstimateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return EstimateForecastSession(dataSetName, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> EstimateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return EstimateForecastSession(dataSetName, targetColumn, startDate, endDate, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> EstimateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(dataSetName))
                throw new ArgumentException("Must specify a data set name to forecast on.", nameof(dataSetName));
            if (String.IsNullOrEmpty(targetColumn))
                throw new ArgumentException("Must specify a target column to forecast on.", nameof(targetColumn));

            return CreateSessionInternal("sessions/forecast", dataSetName, null /* eventName */, targetColumn, startDate, endDate, null, httpMessageTransformer, cancellationToken, isEstimate: true);
        }

        public Task<SessionResponse> EstimateImpactSession(StreamReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            return EstimateImpactSession(input, eventName, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> EstimateImpactSession(StreamReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return EstimateImpactSession(input, eventName, targetColumn, startDate, endDate, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> EstimateImpactSession(StreamReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Must give an input reader.");
            if (String.IsNullOrEmpty(eventName))
                throw new ArgumentException("Must specify an event name for this impact session.", nameof(eventName));
            if (String.IsNullOrEmpty(targetColumn))
                throw new ArgumentException("Must specify a target column for impact.", nameof(targetColumn));

            return CreateSessionInternal("sessions/impact", input, eventName, targetColumn, startDate, endDate, null, httpMessageTransformer, cancellationToken, isEstimate: true);
        }

        public Task<SessionResponse> EstimateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            return EstimateImpactSession(data, eventName, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> EstimateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return EstimateImpactSession(data, eventName, targetColumn, startDate, endDate, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> EstimateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data), "Must give data for impact session.");
            if (String.IsNullOrEmpty(eventName))
                throw new ArgumentException("Must specify an event name for this impact session.", nameof(eventName));
            if (String.IsNullOrEmpty(targetColumn))
                throw new ArgumentException("Must specify a target column for impact.", nameof(targetColumn));

            return CreateSessionInternal("sessions/impact", data, eventName, targetColumn, startDate, endDate, null, httpMessageTransformer, cancellationToken, isEstimate: true);
        }

        public Task<SessionResponse> EstimateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return EstimateImpactSession(dataSetName, eventName, targetColumn, startDate, endDate, null);
        }

        public Task<SessionResponse> EstimateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return EstimateImpactSession(dataSetName, eventName, targetColumn, startDate, endDate, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> EstimateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(dataSetName))
                throw new ArgumentException("Must specify a data set name for impact.", nameof(dataSetName));
            if (String.IsNullOrEmpty(eventName))
                throw new ArgumentException("Must specify an event name for impact.", nameof(eventName));
            if (String.IsNullOrEmpty(targetColumn))
                throw new ArgumentException("Must specify a target column for impact.", nameof(targetColumn));

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

        public Task<List<SessionResponse>> ListSessions()
        {
            return ListSessionsInternal(new Dictionary<string, string>(), null, CancellationToken.None);
        }

        public Task<List<SessionResponse>> ListSessions(string dataSetName)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(dataSetName), dataSetName },
            };
            return ListSessionsInternal(parameters, null, CancellationToken.None);
        }

        public Task<List<SessionResponse>> ListSessions(string dataSetName, string eventName)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(dataSetName), dataSetName },
                { nameof(eventName), eventName },
            };
            return ListSessionsInternal(parameters, null, CancellationToken.None);
        }

        public Task<List<SessionResponse>> ListSessions(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return ListSessions(dataSetName, eventName, startDate, endDate, null, CancellationToken.None);
        }

        public Task<List<SessionResponse>> ListSessions(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return ListSessions(dataSetName, eventName, startDate, endDate, httpMessageTransformer, CancellationToken.None);
        }

        public Task<List<SessionResponse>> ListSessions(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate,
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
            [JsonProperty("results", Required = Required.Always )]
            public List<SessionResponse> Results { get; set; }
        }
        private async Task<List<SessionResponse>> ListSessionsInternal(IDictionary<string, string> parameters,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var response = await apiConnection.Get<SessionResponseDto>("sessions", parameters, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
            return response?.Results;
        }

        public async Task RemoveSessions()
        {
            await RemoveSessions((string)null).ConfigureAwait(false);
        }

        public async Task RemoveSessions(string dataSetName)
        {
            await RemoveSessions(dataSetName, null).ConfigureAwait(false);
        }

        public async Task RemoveSessions(SessionType? type)
        {
            await RemoveSessions(null, null, type).ConfigureAwait(false);
        }

        public async Task RemoveSessions(string dataSetName, SessionType? type)
        {
            await RemoveSessions(dataSetName, null, type).ConfigureAwait(false);
        }

        public async Task RemoveSessions(string dataSetName, string eventName, SessionType? type)
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
            await RemoveSessionsInternal(parameters, null, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task RemoveSessions(string dataSetName, string eventName, SessionType? type, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            await RemoveSessions(dataSetName, eventName, type, startDate, endDate, null).ConfigureAwait(false);
        }

        public async Task RemoveSessions(string dataSetName, string eventName, SessionType? type, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            await RemoveSessions(dataSetName, eventName, type, startDate, endDate, httpMessageTransformer, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task RemoveSessions(string dataSetName, string eventName, SessionType? type, DateTimeOffset startDate, DateTimeOffset endDate,
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

            await RemoveSessionsInternal(parameters, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }

        private async Task RemoveSessionsInternal(IDictionary<string, string> parameters, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            await apiConnection.Delete("sessions", parameters, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }

        public Task<SessionResponse> GetSession(Guid id)
        {
            return GetSession(id, null);
        }

        public Task<SessionResponse> GetSession(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return GetSession(id, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> GetSession(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            return apiConnection.Get<SessionResponse>($"sessions/{id}", null, httpMessageTransformer, cancellationToken);
        }

        public Task<SessionResultStatus> GetSessionStatus(Guid id)
        {
            return GetSessionStatus(id, null);
        }

        public Task<SessionResultStatus> GetSessionStatus(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return GetSessionStatus(id, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResultStatus> GetSessionStatus(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
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

        public async Task RemoveSession(Guid id)
        {
            await RemoveSession(id, null).ConfigureAwait(false);
        }

        public async Task RemoveSession(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            await RemoveSession(id, httpMessageTransformer, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task RemoveSession(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            await apiConnection.Delete($"sessions/{id}", null, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }

        public Task<SessionResult> GetSessionResults(Guid id)
        {
            return GetSessionResults(id, (Action<HttpRequestMessage, HttpResponseMessage>)null);
        }

        public Task<SessionResult> GetSessionResults(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return GetSessionResults(id, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResult> GetSessionResults(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            return apiConnection.Get<SessionResult>($"sessions/{id}/results", null, httpMessageTransformer, cancellationToken);
        }

        public async Task GetSessionResults(Guid id, StreamWriter output)
        {
            await GetSessionResults(id, output, null).ConfigureAwait(false);
        }

        public async Task GetSessionResults(Guid id, StreamWriter output, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            await GetSessionResults(id, output, httpMessageTransformer, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task GetSessionResults(Guid id, StreamWriter output, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            await apiConnection.Get($"sessions/{id}/results", null, httpMessageTransformer, cancellationToken, output, "text/csv").ConfigureAwait(false);
        }
    }
}
