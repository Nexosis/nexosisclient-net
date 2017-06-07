using System;
using System.Collections.Generic;
using System.IO;
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
        Task<AccountBalance> GetAccountBalanceAsync();

        /// <summary>
        /// Forecast from the contents of a CSV file.
        /// </summary>
        /// <param name="file">A reference to a local file which contains the data you wish to submit.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data for prediction.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> CreateForecastSessionAsync(TextReader file, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl);

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
        Task<SessionResponse> CreateForecastSessionAsync(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl);

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
        Task<SessionResponse> CreateForecastSessionAsync(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl);

        /// <summary>
        /// Analyze impact for an event with data from a CSV file.
        /// </summary>
        /// <param name="file">A reference to a local file which contains the data you wish to submit.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <param name="statusCallbackUrl">An optional url used for callbacks when the forecast session status changes.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> CreateImpactSessionAsync(TextReader file, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl);

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
        Task<SessionResponse> CreateImpactSessionAsync(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl);

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
        Task<SessionResponse> CreateImpactSessionAsync(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate, string statusCallbackUrl);

        /// <summary>
        /// Estimate the cost of a forecast from the contents of a CSV file.
        /// </summary>
        /// <param name="file">A reference to a local file which contains the data you wish to submit.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data for prediction.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> EstimateForecastSessionAsync(TextReader file, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate);

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
        Task<SessionResponse> EstimateForecastSessionAsync(IEnumerable<DataSetRow> data, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate);

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
        Task<SessionResponse> EstimateForecastSessionAsync(string dataSetName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// Estimate the cost of impact analysis for an event with data from a CSV file.
        /// </summary>
        /// <param name="file">A reference to a local file which contains the data you wish to submit.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> EstimateImpactSessionAsync(TextReader file, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate);

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
        Task<SessionResponse> EstimateImpactSessionAsync(IEnumerable<DataSetRow> data, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// Estimate the cost of impact analysis for an event with data already saved to the API.
        /// </summary>
        /// <param name="dataSetName">The name of the saved data set that has the data to run the impact analysis on.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <param name="startDate">The starting date of the forecast period.</param>
        /// <param name="endDate">The ending date of the forecast period.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> EstimateImpactSessionAsync(string dataSetName, string eventName, string targetColumn, DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// List sessions that have been run. This will show the information about them such as the id, status, and the analysis date range. 
        /// All parameters are optional and will be used to limit the list returned.
        /// </summary>
        /// <param name="dataSetName">Limits sessions to those with the specified name.</param>
        /// <param name="eventName">Limits impact sessions to those for a particular event.</param>
        /// <param name="startDate">Limits sessions to those created on or after the specified date.</param>
        /// <param name="endDate">Limits sessions to those created on or before the specified date.</param>
        /// <returns>The list of <see cref="SessionResponse"/> objects.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions</remarks>
        Task<List<SessionResponse>> GetSessionsAsync(string dataSetName, string eventName, DateTimeOffset? startDate, DateTimeOffset? endDate);

        /// <summary>
        /// Remove sessions that have been run. All parameters are optional and will be used to limit the sessions removed.
        /// </summary>
        /// <param name="dataSetName">Limits sessions to those with the specified name.</param>
        /// <param name="eventName">Limits impact sessions to those for a particular event.</param>
        /// <param name="startDate">Limits sessions to those created on or after the specified date.</param>
        /// <param name="endDate">Limits sessions to those created on or before the specified date.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/sessions</remarks>
        Task RemoveSessionsAsync(string dataSetName, string eventName, DateTimeOffset? startDate, DateTimeOffset? endDate);

        /// <summary>
        /// Get a specific session by id.
        /// </summary>
        /// <param name="id">The identifier of the session.</param>
        /// <returns>A <see cref="SessionResponse" /> populated with the sessions data.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions/{id}</remarks>
        Task<SessionResponse> GetSessionAsync(Guid id);

        /// <summary>
        /// Lookup the status of the session. 
        /// </summary>
        /// <param name="id">The identifier of the session.</param>
        /// <returns>A <see cref="SessionResultStatus"/> with the status set.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>HEAD of https://ml.nexosis.com/api/sessions/{id}</remarks>
        Task<SessionResultStatus> GetSessionStatusAsync(Guid id);

        /// <summary>
        /// Remove the session. 
        /// </summary>
        /// <param name="id">The identifier of the session to remove.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/sessions/{id}</remarks>
        Task RemoveSessionAsync(Guid id);

        /// <summary>
        /// Get the results of the session.
        /// </summary>
        /// <param name="id">The identifier of the session.</param>
        /// <returns>A <see cref="SessionResult"/> which contains the results of the run.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions/{id}/results</remarks>
        Task<SessionResult> GetSessionResultsAsync(Guid id);

        /// <summary>
        /// Save data in a data set.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset to which to add data.</param>
        /// <param name="data">A DataSet containing the data.</param>
        /// <returns>A <see cref="DataSetSummary"/> for the data set created.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetSummary> SaveDataSetAsync(string dataSetName, IEnumerable<DataSetRow> data);

        /// <summary>
        /// Gets the list of all data sets that have been saved to the system.
        /// </summary>
        /// <returns>List of DataSetSummary</returns>
        /// <returns>A list of <see cref="DataSetSummary"/>.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data</remarks>
        Task<List<DataSetSummary>> ListDataSetsAsync();

        /// <summary>
        /// Gets the list of data sets that have been saved to the system, filtering by partial name match.
        /// </summary>
        /// <param name="nameFilter">Limits results to only those datasets with names containing the specified value</param>
        /// <returns>A list of <see cref="DataSetSummary"/>.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data</remarks>
        Task<List<DataSetSummary>> ListDataSetsAsync(string nameFilter);

        /// <summary>Get the data in the set, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <returns><see cref="DataSetData" /></returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetData> GetDataSetAsync(string dataSetName, DateTimeOffset? startDate, DateTimeOffset? endDate, int? pageNumber, int? pageSize, IEnumerable<string> includeColumns);

        /// <summary>Remove data from a data set or the entire set.</summary>
        /// <param name="dataSetName">Name of the dataset from which to remove data.</param>
        /// <param name="startDate">Limits data removed to those on or after the specified date.</param>
        /// <param name="endDate">Limits data removed to those on or before the specified date.</param>
        /// <param name="options">Controls the options associated with the removal.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task RemoveDataSetAsync(string dataSetName, DateTimeOffset? startDate, DateTimeOffset? endDate, DataSetDeleteOptions options);

        /// <summary>Gets the forecasts associated with a data set.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns><see cref="DataSetData" /></returns>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task<DataSetData> GetDataSetForecastAsync(string dataSetName, DateTimeOffset? startDate, DateTimeOffset? endDate, int? pageNumber, int? pageSize, IEnumerable<string> includeColumns);

        /// <summary>
        /// Removes the forecasts associated with a data set.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset for which to remove data.</param>
        /// <param name="startDate"> Limits deletion to those on or after the specified date.</param>
        /// <param name="endDate">Limits deletion to those on or before the specified date.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task RemoveDataSetForecastAsync(string dataSetName, DateTimeOffset? startDate, DateTimeOffset? endDate);

        /// <summary>
        /// Describes the algorithms run and the models generated in the process of deciding how to generate the forecasts for all columns in a data set.
        /// </summary>
        /// <param name="dataSetName">The name of the data set.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast/model</remarks>
        Task<List<ForecastModel>> GetDataSetForecastModelsAsync(string dataSetName);

        /// <summary>
        /// Describes the algorithms run and the models generated in the process of deciding how to generate the forecasts for a specific column in a data set.
        /// </summary>
        /// <param name="dataSetName">The name of the data set.</param>
        /// <param name="targetColumn">The name of the column in the data set used in forecasting.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast/model/{targetColumn}</remarks>
        Task<ForecastModel> GetDataSetForecastModelAsync(string dataSetName, string targetColumn);

        /// <summary>
        /// Gives back the raw prediction data for a particular algorithm evaluated during the process of generating the forecasts for the target column in a data set.
        /// </summary>
        /// <param name="dataSetName">The name of the data set.</param>
        /// <param name="targetColumn">The name of the column in the data set used in forecasting.</param>
        /// <param name="algorithmKey">The name of the the algorithm used to generate a specific set of forecasts considered.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast/model/{targetColumn}/{algorithmKey}</remarks>
        Task<DataSetData> GetDataSetForecastModelForecastAsync(string dataSetName, string targetColumn, string algorithmKey);
    }
}