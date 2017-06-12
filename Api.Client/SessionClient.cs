using System;
using System.Collections.Generic;
using System.IO;
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
            return CreateForecastSession(input, targetColumn, startDate, endDate, null, null, CancellationToken.None);
        }

        public Task<SessionResponse> CreateForecastSession(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl)
        {
            return CreateForecastSession(input, targetColumn, startDate, endDate, statusCallbackUrl, null, CancellationToken.None);
        }

        public Task<SessionResponse> CreateForecastSession(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return CreateForecastSession(input, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> CreateForecastSession(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(targetColumn), targetColumn },
                { nameof(startDate), startDate.ToString("O") },
                { nameof(endDate), endDate.ToString("O") }
            };
            if (!String.IsNullOrEmpty(statusCallbackUrl))
            {
                parameters.Add("callbackUrl", statusCallbackUrl);
            }
            return apiConnection.Post<SessionResponse>("sessions/forecast", parameters, input, httpMessageTransformer, cancellationToken);
        }

        public Task<SessionResponse> CreateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return CreateForecastSession(data, targetColumn, startDate, endDate, null, null, CancellationToken.None);
        }

        public Task<SessionResponse> CreateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl)
        {
            return CreateForecastSession(data, targetColumn, startDate, endDate, statusCallbackUrl, null, CancellationToken.None);
        }

        public Task<SessionResponse> CreateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return CreateForecastSession(data, targetColumn, startDate, endDate, statusCallbackUrl, httpMessageTransformer, CancellationToken.None);
        }

        public Task<SessionResponse> CreateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(targetColumn), targetColumn },
                { nameof(startDate), startDate.ToString("O") },
                { nameof(endDate), endDate.ToString("O") }
            };
            if (!String.IsNullOrEmpty(statusCallbackUrl))
            {
                parameters.Add("callbackUrl", statusCallbackUrl);
            }
            return apiConnection.Post<SessionResponse>("sessions/forecast", parameters, new { data = data }, httpMessageTransformer, cancellationToken);
        }

        public async Task<SessionResponse> CreateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> CreateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateForecastSession(TextReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateForecastSession(TextReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateForecastSession(TextReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> EstimateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate,
            DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<SessionResponse>> ListSessions()
        {
            return ListSessions(new Dictionary<string, string>(), null, CancellationToken.None);
        }

        public Task<List<SessionResponse>> ListSessions(string dataSetName)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(dataSetName), dataSetName },
            };
            return ListSessions(parameters, null, CancellationToken.None);
        }

        public Task<List<SessionResponse>> ListSessions(string dataSetName, string eventName)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(dataSetName), dataSetName },
                { nameof(eventName), eventName },
            };
            return ListSessions(parameters, null, CancellationToken.None);
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

            return ListSessions(parameters, httpMessageTransformer, cancellationToken);
        }

        // we are returning just the list object, so we need a wrapper here and will then pull the results off of it.
        private class SessionResponseDto
        {
            [JsonProperty("results", Required = Required.Always )]
            public List<SessionResponse> Results { get; set; }
        }
        private async Task<List<SessionResponse>> ListSessions(IDictionary<string, string> parameters,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var response = await apiConnection.Get<SessionResponseDto>("sessions", parameters, httpMessageTransformer, cancellationToken);
            return response?.Results;
        }

        public async Task RemoveSessions()
        {
            throw new NotImplementedException();
        }

        public async Task RemoveSessions(string dataSetName)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveSessions(string dataSetName, string eventName)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveSessions(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveSessions(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveSessions(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> GetSession(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> GetSession(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponse> GetSession(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResultStatus> GetSessionStatus(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResultStatus> GetSessionStatus(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResultStatus> GetSessionStatus(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveSession(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveSession(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveSession(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResult> GetSessionResults(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResult> GetSessionResults(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResult> GetSessionResults(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task GetSessionResults(Guid id, TextWriter output)
        {
            throw new NotImplementedException();
        }

        public async Task GetSessionResults(Guid id, TextWriter output, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task GetSessionResults(Guid id, TextWriter output, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
