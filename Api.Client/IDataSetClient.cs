using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{
    public interface IDataSetClient
    {
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
