using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{
    /// <summary>
    /// The primary interface to the Nexosis API.
    /// </summary>
    public interface INexosisClient
    {
        /// <summary>Gets the current account balance.</summary>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions</remarks>
        Task<AccountBalance> GetAccountBalance();

        /// <summary>Gets the current account balance.</summary>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions</remarks>
        Task<AccountBalance> GetAccountBalance(Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>Gets the current account balance.</summary>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions</remarks>
        Task<AccountBalance> GetAccountBalance(Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Forecast from CSV formatted data.
        /// </summary>
        /// <param name="input">A reference to a <see cref="TextReader"/> which contains the data you wish to submit.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data for prediction.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> CreateForecastSession(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// Forecast from CSV formatted data.
        /// </summary>
        /// <param name="input">A reference to a <see cref="TextReader"/> which contains the data you wish to submit.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data for prediction.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> CreateForecastSession(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl);

        /// <summary>
        /// Forecast from CSV formatted data.
        /// </summary>
        /// <param name="input">A reference to a <see cref="TextReader"/> which contains the data you wish to submit.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data for prediction.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> CreateForecastSession(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Forecast from CSV formatted data.
        /// </summary>
        /// <param name="input">A reference to a <see cref="TextReader"/> which contains the data you wish to submit.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data for prediction.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> CreateForecastSession(StreamReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Forecast from data posted in the request.
        /// </summary>
        /// <param name="data">A list of data set rows containing the data used for the forecast.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> CreateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// Forecast from data posted in the request.
        /// </summary>
        /// <param name="data">A list of data set rows containing the data used for the forecast.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> CreateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl);

        /// <summary>
        /// Forecast from data posted in the request.
        /// </summary>
        /// <param name="data">A list of data set rows containing the data used for the forecast.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> CreateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Forecast from data posted in the request.
        /// </summary>
        /// <param name="data">A list of data set rows containing the data used for the forecast.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> CreateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Forecast from data already saved to the API.
        /// </summary>
        /// <param name="dataSetName">The name of the saved data set that has the data to forecast on.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> CreateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// Forecast from data already saved to the API.
        /// </summary>
        /// <param name="dataSetName">The name of the saved data set that has the data to forecast on.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> CreateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl);

        /// <summary>
        /// Forecast from data already saved to the API.
        /// </summary>
        /// <param name="dataSetName">The name of the saved data set that has the data to forecast on.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> CreateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Forecast from data already saved to the API.
        /// </summary>
        /// <param name="dataSetName">The name of the saved data set that has the data to forecast on.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> CreateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Analyze impact for an event with CSV formatted data.
        /// </summary>
        /// <param name="input">A reference to a local file which contains the data you wish to submit.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> CreateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// Analyze impact for an event with CSV formatted data.
        /// </summary>
        /// <param name="input">A reference to a local file which contains the data you wish to submit.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> CreateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl);

        /// <summary>
        /// Analyze impact for an event with CSV formatted data.
        /// </summary>
        /// <param name="input">A reference to a local file which contains the data you wish to submit.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> CreateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Analyze impact for an event with CSV formatted data.
        /// </summary>
        /// <param name="input">A reference to a local file which contains the data you wish to submit.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> CreateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Analyze impact for an event with data in the request.
        /// </summary>
        /// <param name="data">The data to run the impact analysis on.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> CreateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// Analyze impact for an event with data in the request.
        /// </summary>
        /// <param name="data">The data to run the impact analysis on.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> CreateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl);

        /// <summary>
        /// Analyze impact for an event with data in the request.
        /// </summary>
        /// <param name="data">The data to run the impact analysis on.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> CreateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Analyze impact for an event with data in the request.
        /// </summary>
        /// <param name="data">The data to run the impact analysis on.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> CreateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Analyze impact for an event with data already saved to the API.
        /// </summary>
        /// <param name="dataSetName">The name of the saved data set that has the data to run the impact analysis on.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> CreateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// Analyze impact for an event with data already saved to the API.
        /// </summary>
        /// <param name="dataSetName">The name of the saved data set that has the data to run the impact analysis on.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> CreateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl);

        /// <summary>
        /// Analyze impact for an event with data already saved to the API.
        /// </summary>
        /// <param name="dataSetName">The name of the saved data set that has the data to run the impact analysis on.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> CreateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Analyze impact for an event with data already saved to the API.
        /// </summary>
        /// <param name="dataSetName">The name of the saved data set that has the data to run the impact analysis on.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> CreateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Estimate the cost of a forecast from the contents of a CSV file.
        /// </summary>
        /// <param name="input">A reference to a <see cref="TextReader"/> which contains the data you wish to submit.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data for prediction.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> EstimateForecastSession(TextReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// Estimate the cost of a forecast from the contents of a CSV file.
        /// </summary>
        /// <param name="input">A reference to a <see cref="TextReader"/> which contains the data you wish to submit.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data for prediction.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> EstimateForecastSession(TextReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Estimate the cost of a forecast from the contents of a CSV file.
        /// </summary>
        /// <param name="input">A reference to a <see cref="TextReader"/> which contains the data you wish to submit.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data for prediction.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> EstimateForecastSession(TextReader input, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Estimate the cost of a forecast from data posted in the request.
        /// </summary>
        /// <param name="data">A list of data set rows containing the data used for the forecast.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> EstimateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// Estimate the cost of a forecast from data posted in the request.
        /// </summary>
        /// <param name="data">A list of data set rows containing the data used for the forecast.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> EstimateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Estimate the cost of a forecast from data posted in the request.
        /// </summary>
        /// <param name="data">A list of data set rows containing the data used for the forecast.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> EstimateForecastSession(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Estimate the cost of a forecast from data already saved to the API.
        /// </summary>
        /// <param name="dataSetName">The name of the saved data set that has the data to forecast on.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> EstimateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// Estimate the cost of a forecast from data already saved to the API.
        /// </summary>
        /// <param name="dataSetName">The name of the saved data set that has the data to forecast on.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> EstimateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Estimate the cost of a forecast from data already saved to the API.
        /// </summary>
        /// <param name="dataSetName">The name of the saved data set that has the data to forecast on.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> EstimateForecastSession(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Estimate the cost of impact analysis for an event with data from a CSV file.
        /// </summary>
        /// <param name="input">A reference to a <see cref="TextReader"/> which contains the data you wish to submit.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> EstimateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// Estimate the cost of impact analysis for an event with data from a CSV file.
        /// </summary>
        /// <param name="input">A reference to a <see cref="TextReader"/> which contains the data you wish to submit.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> EstimateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Estimate the cost of impact analysis for an event with data from a CSV file.
        /// </summary>
        /// <param name="input">A reference to a <see cref="TextReader"/> which contains the data you wish to submit.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> EstimateImpactSession(TextReader input, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Estimate the cost of impact analysis for an event with data in the request.
        /// </summary>
        /// <param name="data">The data to run the impact analysis on.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> EstimateImpactSession(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// Estimate the cost of impact analysis for an event with data already saved to the API.
        /// </summary>
        /// <param name="dataSetName">The name of the saved data set that has the data to run the impact analysis on.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> EstimateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Estimate the cost of impact analysis for an event with data already saved to the API.
        /// </summary>
        /// <param name="dataSetName">The name of the saved data set that has the data to run the impact analysis on.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> EstimateImpactSession(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// List sessions that have been run. This will show the information about them such as the id, status, and the analysis date range. 
        /// </summary>
        /// <returns>The list of <see cref="SessionResponse"/> objects.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions</remarks>
        Task<List<SessionResponse>> ListSessions();

        /// <summary>
        /// List sessions that have been run. This will show the information about them such as the id, status, and the analysis date range. 
        /// </summary>
        /// <param name="dataSetName">Limits sessions to those with the specified name.</param>
        /// <returns>The list of <see cref="SessionResponse"/> objects.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions</remarks>
        Task<List<SessionResponse>> ListSessions(string dataSetName);

        /// <summary>
        /// List sessions that have been run. This will show the information about them such as the id, status, and the analysis date range. 
        /// </summary>
        /// <param name="dataSetName">Limits sessions to those with the specified name.</param>
        /// <param name="eventName">Limits impact sessions to those for a particular event.</param>
        /// <returns>The list of <see cref="SessionResponse"/> objects.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions</remarks>
        Task<List<SessionResponse>> ListSessions(string dataSetName, string eventName);

        /// <summary>
        /// List sessions that have been run. This will show the information about them such as the id, status, and the analysis date range. 
        /// </summary>
        /// <param name="dataSetName">Limits sessions to those with the specified name.</param>
        /// <param name="eventName">Limits impact sessions to those for a particular event.</param>
        /// <param name="startDate">Limits sessions to those created on or after the specified date.</param>
        /// <param name="endDate">Limits sessions to those created on or before the specified date.</param>
        /// <returns>The list of <see cref="SessionResponse"/> objects.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions</remarks>
        Task<List<SessionResponse>> ListSessions(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// List sessions that have been run. This will show the information about them such as the id, status, and the analysis date range. 
        /// </summary>
        /// <param name="dataSetName">Limits sessions to those with the specified name.</param>
        /// <param name="eventName">Limits impact sessions to those for a particular event.</param>
        /// <param name="startDate">Limits sessions to those created on or after the specified date.</param>
        /// <param name="endDate">Limits sessions to those created on or before the specified date.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns>The list of <see cref="SessionResponse"/> objects.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions</remarks>
        Task<List<SessionResponse>> ListSessions(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// List sessions that have been run. This will show the information about them such as the id, status, and the analysis date range. 
        /// </summary>
        /// <param name="dataSetName">Limits sessions to those with the specified name.</param>
        /// <param name="eventName">Limits impact sessions to those for a particular event.</param>
        /// <param name="startDate">Limits sessions to those created on or after the specified date.</param>
        /// <param name="endDate">Limits sessions to those created on or before the specified date.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The list of <see cref="SessionResponse"/> objects.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions</remarks>
        Task<List<SessionResponse>> ListSessions(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Remove sessions that have been run.
        /// </summary>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/sessions</remarks>
        Task RemoveSessions();

        /// <summary>
        /// Remove sessions that have been run.
        /// </summary>
        /// <param name="dataSetName">Limits sessions to those with the specified name.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/sessions</remarks>
        Task RemoveSessions(string dataSetName);

        /// <summary>
        /// Remove sessions that have been run. All parameters are optional and will be used to limit the sessions removed.
        /// </summary>
        /// <param name="dataSetName">Limits sessions to those with the specified name.</param>
        /// <param name="eventName">Limits impact sessions to those for a particular event.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/sessions</remarks>
        Task RemoveSessions(string dataSetName, string eventName);

        /// <summary>
        /// Remove sessions that have been run. All parameters are optional and will be used to limit the sessions removed.
        /// </summary>
        /// <param name="dataSetName">Limits sessions to those with the specified name.</param>
        /// <param name="eventName">Limits impact sessions to those for a particular event.</param>
        /// <param name="startDate">Limits sessions to those created on or after the specified date.</param>
        /// <param name="endDate">Limits sessions to those created on or before the specified date.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/sessions</remarks>
        Task RemoveSessions(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// Remove sessions that have been run. All parameters are optional and will be used to limit the sessions removed.
        /// </summary>
        /// <param name="dataSetName">Limits sessions to those with the specified name.</param>
        /// <param name="eventName">Limits impact sessions to those for a particular event.</param>
        /// <param name="startDate">Limits sessions to those created on or after the specified date.</param>
        /// <param name="endDate">Limits sessions to those created on or before the specified date.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/sessions</remarks>
        Task RemoveSessions(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Remove sessions that have been run. All parameters are optional and will be used to limit the sessions removed.
        /// </summary>
        /// <param name="dataSetName">Limits sessions to those with the specified name.</param>
        /// <param name="eventName">Limits impact sessions to those for a particular event.</param>
        /// <param name="startDate">Limits sessions to those created on or after the specified date.</param>
        /// <param name="endDate">Limits sessions to those created on or before the specified date.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/sessions</remarks>
        Task RemoveSessions(string dataSetName, string eventName, DateTimeOffset startDate, DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Get a specific session by id.
        /// </summary>
        /// <param name="id">The identifier of the session.</param>
        /// <returns>A <see cref="SessionResponse" /> populated with the sessions data.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions/{id}</remarks>
        Task<SessionResponse> GetSession(Guid id);

        /// <summary>
        /// Get a specific session by id.
        /// </summary>
        /// <param name="id">The identifier of the session.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns>A <see cref="SessionResponse" /> populated with the sessions data.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions/{id}</remarks>
        Task<SessionResponse> GetSession(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Get a specific session by id.
        /// </summary>
        /// <param name="id">The identifier of the session.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="SessionResponse" /> populated with the sessions data.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions/{id}</remarks>
        Task<SessionResponse> GetSession(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Lookup the status of the session. 
        /// </summary>
        /// <param name="id">The identifier of the session.</param>
        /// <returns>A <see cref="SessionResultStatus"/> with the status set.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>HEAD of https://ml.nexosis.com/api/sessions/{id}</remarks>
        Task<SessionResultStatus> GetSessionStatus(Guid id);

        /// <summary>
        /// Lookup the status of the session. 
        /// </summary>
        /// <param name="id">The identifier of the session.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns>A <see cref="SessionResultStatus"/> with the status set.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>HEAD of https://ml.nexosis.com/api/sessions/{id}</remarks>
        Task<SessionResultStatus> GetSessionStatus(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Lookup the status of the session. 
        /// </summary>
        /// <param name="id">The identifier of the session.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="SessionResultStatus"/> with the status set.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>HEAD of https://ml.nexosis.com/api/sessions/{id}</remarks>
        Task<SessionResultStatus> GetSessionStatus(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Remove the session. 
        /// </summary>
        /// <param name="id">The identifier of the session to remove.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/sessions/{id}</remarks>
        Task RemoveSession(Guid id);

        /// <summary>
        /// Remove the session. 
        /// </summary>
        /// <param name="id">The identifier of the session to remove.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/sessions/{id}</remarks>
        Task RemoveSession(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Remove the session. 
        /// </summary>
        /// <param name="id">The identifier of the session to remove.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/sessions/{id}</remarks>
        Task RemoveSession(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Get the results of the session.
        /// </summary>
        /// <param name="id">The identifier of the session.</param>
        /// <returns>A <see cref="SessionResult"/> which contains the results of the run.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions/{id}/results</remarks>
        Task<SessionResult> GetSessionResults(Guid id);

        /// <summary>
        /// Get the results of the session.
        /// </summary>
        /// <param name="id">The identifier of the session.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns>A <see cref="SessionResult"/> which contains the results of the run.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions/{id}/results</remarks>
        Task<SessionResult> GetSessionResults(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Get the results of the session.
        /// </summary>
        /// <param name="id">The identifier of the session.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="SessionResult"/> which contains the results of the run.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions/{id}/results</remarks>
        Task<SessionResult> GetSessionResults(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Get results of the session written to a file as CSV. It will only write the values of the forecast or impact session and not any of the 
        /// other data normally returned in a <see cref="SessionResult"/>.
        /// </summary>
        /// <param name="id">The identifier of the session.</param>
        /// <param name="output">An <see cref="TextWriter"/> where the results should be written.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions/{id}/results</remarks>
        Task GetSessionResults(Guid id, TextWriter output);

        /// <summary>
        /// Get results of the session written to a file as CSV. It will only write the values of the forecast or impact session and not any of the 
        /// other data normally returned in a <see cref="SessionResult"/>.
        /// </summary>
        /// <param name="id">The identifier of the session.</param>
        /// <param name="output">An <see cref="TextWriter"/> where the results should be written.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions/{id}/results</remarks>
        Task GetSessionResults(Guid id, TextWriter output, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Get results of the session written to a file as CSV. It will only write the values of the forecast or impact session and not any of the 
        /// other data normally returned in a <see cref="SessionResult"/>.
        /// </summary>
        /// <param name="id">The identifier of the session.</param>
        /// <param name="output">An <see cref="TextWriter"/> where the results should be written.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions/{id}/results</remarks>
        Task GetSessionResults(Guid id, TextWriter output, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Save data in a data set.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset to which to add data.</param>
        /// <param name="data">A DataSet containing the data.</param>
        /// <returns>A <see cref="DataSetSummary"/> for the data set created.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>PUT to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetSummary> CreateDataSet(string dataSetName, IEnumerable<DataSetRow> data);

        /// <summary>
        /// Save data in a data set.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset to which to add data.</param>
        /// <param name="data">A DataSet containing the data.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns>A <see cref="DataSetSummary"/> for the data set created.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>PUT to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetSummary> CreateDataSet(string dataSetName, IEnumerable<DataSetRow> data, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Save data in a data set.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset to which to add data.</param>
        /// <param name="data">A DataSet containing the data.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="DataSetSummary"/> for the data set created.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>PUT to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetSummary> CreateDataSet(string dataSetName, IEnumerable<DataSetRow> data, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the list of all data sets that have been saved to the system.
        /// </summary>
        /// <returns>A list of <see cref="DataSetSummary"/>.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data</remarks>
        Task<List<DataSetSummary>> ListDataSets();

        /// <summary>
        /// Gets the list of data sets that have been saved to the system, filtering by partial name match.
        /// </summary>
        /// <param name="nameFilter">Limits results to only those datasets with names containing the specified value</param>
        /// <returns>A list of <see cref="DataSetSummary"/>.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data</remarks>
        Task<List<DataSetSummary>> ListDataSets(string nameFilter);

        /// <summary>
        /// Gets the list of data sets that have been saved to the system, filtering by partial name match.
        /// </summary>
        /// <param name="nameFilter">Limits results to only those datasets with names containing the specified value</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns>A list of <see cref="DataSetSummary"/>.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data</remarks>
        Task<List<DataSetSummary>> ListDataSets(string nameFilter, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Gets the list of data sets that have been saved to the system, filtering by partial name match.
        /// </summary>
        /// <param name="nameFilter">Limits results to only those datasets with names containing the specified value</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A list of <see cref="DataSetSummary"/>.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data</remarks>
        Task<List<DataSetSummary>> ListDataSets(string nameFilter, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>Get the data in the set.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <returns><see cref="DataSetData" /></returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetData> GetDataSet(string dataSetName);

        /// <summary>Get the data in the set, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <returns><see cref="DataSetData" /></returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetData> GetDataSet(string dataSetName, int pageNumber, int pageSize, IEnumerable<string> includeColumns);

        /// <summary>Get the data in the set, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <returns><see cref="DataSetData" /></returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetData> GetDataSet(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns);

        /// <summary>Get the data in the set, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns><see cref="DataSetData" /></returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetData> GetDataSet(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>Get the data in the set, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><see cref="DataSetData" /></returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetData> GetDataSet(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>Get the data in the set, written to a CSV file.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="TextWriter"/> where the data should be written.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task GetDataSet(string dataSetName, TextWriter output);

        /// <summary>Get the data in the set, written to a CSV file, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="TextWriter"/> where the data should be written.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task GetDataSet(string dataSetName, TextWriter output, int pageNumber, int pageSize, IEnumerable<string> includeColumns);

        /// <summary>Get the data in the set, written to a CSV file, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="TextWriter"/> where the data should be written.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task GetDataSet(string dataSetName, TextWriter output, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns);

        /// <summary>Get the data in the set, written to a CSV file, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="TextWriter"/> where the data should be written.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task GetDataSet(string dataSetName, TextWriter output, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>Get the data in the set, written to a CSV file, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="TextWriter"/> where the data should be written.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task GetDataSet(string dataSetName, TextWriter output, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>Remove data from a data set or the entire set.</summary>
        /// <param name="dataSetName">Name of the dataset from which to remove data.</param>
        /// <param name="options">Controls the options associated with the removal.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task RemoveDataSet(string dataSetName, DataSetDeleteOptions options);

        /// <summary>Remove data from a data set or the entire set.</summary>
        /// <param name="dataSetName">Name of the dataset from which to remove data.</param>
        /// <param name="startDate">Limits data removed to those on or after the specified date.</param>
        /// <param name="endDate">Limits data removed to those on or before the specified date.</param>
        /// <param name="options">Controls the options associated with the removal.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task RemoveDataSet(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, DataSetDeleteOptions options);

        /// <summary>Remove data from a data set or the entire set.</summary>
        /// <param name="dataSetName">Name of the dataset from which to remove data.</param>
        /// <param name="startDate">Limits data removed to those on or after the specified date.</param>
        /// <param name="endDate">Limits data removed to those on or before the specified date.</param>
        /// <param name="options">Controls the options associated with the removal.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task RemoveDataSet(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, DataSetDeleteOptions options, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>Remove data from a data set or the entire set.</summary>
        /// <param name="dataSetName">Name of the dataset from which to remove data.</param>
        /// <param name="startDate">Limits data removed to those on or after the specified date.</param>
        /// <param name="endDate">Limits data removed to those on or before the specified date.</param>
        /// <param name="options">Controls the options associated with the removal.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task RemoveDataSet(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, DataSetDeleteOptions options, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>Gets the forecasts associated with a data set.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns><see cref="DataSetData" /></returns>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task<DataSetData> GetDataSetForecast(string dataSetName);

        /// <summary>Gets the forecasts associated with a data set.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns><see cref="DataSetData" /></returns>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task<DataSetData> GetDataSetForecast(string dataSetName, int pageNumber, int pageSize, IEnumerable<string> includeColumns);

        /// <summary>Gets the forecasts associated with a data set.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns><see cref="DataSetData" /></returns>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task<DataSetData> GetDataSetForecast(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, int pageNumber, int pageSize, IEnumerable<string> includeColumns);

        /// <summary>Gets the forecasts associated with a data set.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns><see cref="DataSetData" /></returns>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task<DataSetData> GetDataSetForecast(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>Gets the forecasts associated with a data set.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns><see cref="DataSetData" /></returns>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task<DataSetData> GetDataSetForecast(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>Gets the forecasts associated with a data set, written as CSV.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="TextWriter"/> where the data should be written.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns><see cref="DataSetData" /></returns>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task GetDataSetForecast(string dataSetName, TextWriter output);

        /// <summary>Gets the forecasts associated with a data set, written as CSV.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="TextWriter"/> where the data should be written.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns><see cref="DataSetData" /></returns>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task GetDataSetForecast(string dataSetName, TextWriter output, int pageNumber, int pageSize, IEnumerable<string> includeColumns);

        /// <summary>Gets the forecasts associated with a data set, written as CSV.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="TextWriter"/> where the data should be written.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns><see cref="DataSetData" /></returns>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task GetDataSetForecast(string dataSetName, TextWriter output, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns);

        /// <summary>Gets the forecasts associated with a data set, written as CSV.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="TextWriter"/> where the data should be written.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns><see cref="DataSetData" /></returns>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task GetDataSetForecast(string dataSetName, TextWriter output, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>Gets the forecasts associated with a data set, written as CSV.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="TextWriter"/> where the data should be written.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns><see cref="DataSetData" /></returns>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task GetDataSetForecast(string dataSetName, TextWriter output, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Removes the forecasts associated with a data set.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset for which to remove data.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task RemoveDataSetForecast(string dataSetName);

        /// <summary>
        /// Removes the forecasts associated with a data set.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset for which to remove data.</param>
        /// <param name="startDate"> Limits deletion to those on or after the specified date.</param>
        /// <param name="endDate">Limits deletion to those on or before the specified date.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task RemoveDataSetForecast(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// Removes the forecasts associated with a data set.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset for which to remove data.</param>
        /// <param name="startDate"> Limits deletion to those on or after the specified date.</param>
        /// <param name="endDate">Limits deletion to those on or before the specified date.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task RemoveDataSetForecast(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Describes the algorithms run and the models generated in the process of deciding how to generate the forecasts for all columns in a data set.
        /// </summary>
        /// <param name="dataSetName">The name of the data set.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast/model</remarks>
        Task<List<ForecastModel>> ListDataSetForecastModels(string dataSetName);

        /// <summary>
        /// Describes the algorithms run and the models generated in the process of deciding how to generate the forecasts for all columns in a data set.
        /// </summary>
        /// <param name="dataSetName">The name of the data set.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast/model</remarks>
        Task<List<ForecastModel>> ListDataSetForecastModels(string dataSetName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Describes the algorithms run and the models generated in the process of deciding how to generate the forecasts for all columns in a data set.
        /// </summary>
        /// <param name="dataSetName">The name of the data set.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast/model</remarks>
        Task<List<ForecastModel>> ListDataSetForecastModels(string dataSetName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Describes the algorithms run and the models generated in the process of deciding how to generate the forecasts for a specific column in a data set.
        /// </summary>
        /// <param name="dataSetName">The name of the data set.</param>
        /// <param name="targetColumn">The name of the column in the data set used in forecasting.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast/model/{targetColumn}</remarks>
        Task<ForecastModel> GetDataSetForecastModel(string dataSetName, string targetColumn);

        /// <summary>
        /// Describes the algorithms run and the models generated in the process of deciding how to generate the forecasts for a specific column in a data set.
        /// </summary>
        /// <param name="dataSetName">The name of the data set.</param>
        /// <param name="targetColumn">The name of the column in the data set used in forecasting.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast/model/{targetColumn}</remarks>
        Task<ForecastModel> GetDataSetForecastModel(string dataSetName, string targetColumn, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Describes the algorithms run and the models generated in the process of deciding how to generate the forecasts for a specific column in a data set.
        /// </summary>
        /// <param name="dataSetName">The name of the data set.</param>
        /// <param name="targetColumn">The name of the column in the data set used in forecasting.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast/model/{targetColumn}</remarks>
        Task<ForecastModel> GetDataSetForecastModel(string dataSetName, string targetColumn, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Gives back the raw prediction data for a particular algorithm evaluated during the process of generating the forecasts for the target column in a data set.
        /// </summary>
        /// <param name="dataSetName">The name of the data set.</param>
        /// <param name="targetColumn">The name of the column in the data set used in forecasting.</param>
        /// <param name="algorithmKey">The name of the the algorithm used to generate a specific set of forecasts considered.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast/model/{targetColumn}/{algorithmKey}</remarks>
        Task<DataSetData> GetDataSetForecastModelForecast(string dataSetName, string targetColumn, string algorithmKey);

        /// <summary>
        /// Gives back the raw prediction data for a particular algorithm evaluated during the process of generating the forecasts for the target column in a data set.
        /// </summary>
        /// <param name="dataSetName">The name of the data set.</param>
        /// <param name="targetColumn">The name of the column in the data set used in forecasting.</param>
        /// <param name="algorithmKey">The name of the the algorithm used to generate a specific set of forecasts considered.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast/model/{targetColumn}/{algorithmKey}</remarks>
        Task<DataSetData> GetDataSetForecastModelForecast(string dataSetName, string targetColumn, string algorithmKey, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);
    }
}