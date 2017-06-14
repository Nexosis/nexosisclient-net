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
        Task<DataSetSummary> Create(string dataSetName, IEnumerable<DataSetRow> data);

        /// <summary>
        /// Save data in a data set.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset to which to add data.</param>
        /// <param name="data">A DataSet containing the data.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns>A <see cref="DataSetSummary"/> for the data set created.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>PUT to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetSummary> Create(string dataSetName, IEnumerable<DataSetRow> data, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

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
        Task<DataSetSummary> Create(string dataSetName, IEnumerable<DataSetRow> data, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Save data in a data set.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset to which to add data.</param>
        /// <param name="input">A reference to a <see cref="StreamReader"/> which contains the data you wish to save.</param>
        /// <returns>A <see cref="DataSetSummary"/> for the data set created.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>PUT to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetSummary> Create(string dataSetName, StreamReader input);

        /// <summary>
        /// Save data in a data set.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset to which to add data.</param>
        /// <param name="input">A reference to a <see cref="StreamReader"/> which contains the data you wish to save.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns>A <see cref="DataSetSummary"/> for the data set created.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>PUT to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetSummary> Create(string dataSetName, StreamReader input, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Save data in a data set.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset to which to add data.</param>
        /// <param name="input">A reference to a <see cref="StreamReader"/> which contains the data you wish to save.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="DataSetSummary"/> for the data set created.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>PUT to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetSummary> Create(string dataSetName, StreamReader input, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);
        /// <summary>
        /// Gets the list of all data sets that have been saved to the system.
        /// </summary>
        /// <returns>A list of <see cref="DataSetSummary"/>.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data</remarks>
        Task<List<DataSetSummary>> List();

        /// <summary>
        /// Gets the list of data sets that have been saved to the system, filtering by partial name match.
        /// </summary>
        /// <param name="partialName">Limits results to only those datasets with names containing the specified value</param>
        /// <returns>A list of <see cref="DataSetSummary"/>.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data</remarks>
        Task<List<DataSetSummary>> List(string partialName);

        /// <summary>
        /// Gets the list of data sets that have been saved to the system, filtering by partial name match.
        /// </summary>
        /// <param name="partialName">Limits results to only those datasets with names containing the specified value</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns>A list of <see cref="DataSetSummary"/>.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data</remarks>
        Task<List<DataSetSummary>> List(string partialName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Gets the list of data sets that have been saved to the system, filtering by partial name match.
        /// </summary>
        /// <param name="partialName">Limits results to only those datasets with names containing the specified value</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A list of <see cref="DataSetSummary"/>.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data</remarks>
        Task<List<DataSetSummary>> List(string partialName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>Get the data in the set.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <returns><see cref="DataSetData" /></returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetData> Get(string dataSetName);

        /// <summary>Get the data in the set, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <returns><see cref="DataSetData" /></returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetData> Get(string dataSetName, int pageNumber, int pageSize, IEnumerable<string> includeColumns);

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
        Task<DataSetData> Get(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns);

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
        Task<DataSetData> Get(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

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
        Task<DataSetData> Get(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>Get the data in the set, written to a CSV file.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="StreamWriter"/> where the data should be written.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task Get(string dataSetName, StreamWriter output);

        /// <summary>Get the data in the set, written to a CSV file, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="StreamWriter"/> where the data should be written.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task Get(string dataSetName, StreamWriter output, int pageNumber, int pageSize, IEnumerable<string> includeColumns);

        /// <summary>Get the data in the set, written to a CSV file, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="StreamWriter"/> where the data should be written.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task Get(string dataSetName, StreamWriter output, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns);

        /// <summary>Get the data in the set, written to a CSV file, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="StreamWriter"/> where the data should be written.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task Get(string dataSetName, StreamWriter output, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>Get the data in the set, written to a CSV file, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="StreamWriter"/> where the data should be written.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task Get(string dataSetName, StreamWriter output, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>Remove data from a data set or the entire set.</summary>
        /// <param name="dataSetName">Name of the dataset from which to remove data.</param>
        /// <param name="options">Controls the options associated with the removal.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task Remove(string dataSetName, DataSetDeleteOptions options);

        /// <summary>Remove data from a data set or the entire set.</summary>
        /// <param name="dataSetName">Name of the dataset from which to remove data.</param>
        /// <param name="startDate">Limits data removed to those on or after the specified date.</param>
        /// <param name="endDate">Limits data removed to those on or before the specified date.</param>
        /// <param name="options">Controls the options associated with the removal.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task Remove(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, DataSetDeleteOptions options);

        /// <summary>Remove data from a data set or the entire set.</summary>
        /// <param name="dataSetName">Name of the dataset from which to remove data.</param>
        /// <param name="startDate">Limits data removed to those on or after the specified date.</param>
        /// <param name="endDate">Limits data removed to those on or before the specified date.</param>
        /// <param name="options">Controls the options associated with the removal.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task Remove(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, DataSetDeleteOptions options, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>Remove data from a data set or the entire set.</summary>
        /// <param name="dataSetName">Name of the dataset from which to remove data.</param>
        /// <param name="startDate">Limits data removed to those on or after the specified date.</param>
        /// <param name="endDate">Limits data removed to those on or before the specified date.</param>
        /// <param name="options">Controls the options associated with the removal.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task Remove(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, DataSetDeleteOptions options, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>Gets the forecasts associated with a data set.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns><see cref="DataSetData" /></returns>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task<DataSetData> GetForecast(string dataSetName);

        /// <summary>Gets the forecasts associated with a data set.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns><see cref="DataSetData" /></returns>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task<DataSetData> GetForecast(string dataSetName, int pageNumber, int pageSize, IEnumerable<string> includeColumns);

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
        Task<DataSetData> GetForecast(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, int pageNumber, int pageSize, IEnumerable<string> includeColumns);

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
        Task<DataSetData> GetForecast(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

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
        Task<DataSetData> GetForecast(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>Gets the forecasts associated with a data set, written as CSV.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="StreamWriter"/> where the data should be written.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns><see cref="DataSetData" /></returns>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task GetForecast(string dataSetName, StreamWriter output);

        /// <summary>Gets the forecasts associated with a data set, written as CSV.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="StreamWriter"/> where the data should be written.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns><see cref="DataSetData" /></returns>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task GetForecast(string dataSetName, StreamWriter output, int pageNumber, int pageSize, IEnumerable<string> includeColumns);

        /// <summary>Gets the forecasts associated with a data set, written as CSV.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="StreamWriter"/> where the data should be written.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns><see cref="DataSetData" /></returns>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task GetForecast(string dataSetName, StreamWriter output, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns);

        /// <summary>Gets the forecasts associated with a data set, written as CSV.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="StreamWriter"/> where the data should be written.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 100).</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="includeColumns">Limits results to the specified columns of the data set.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns><see cref="DataSetData" /></returns>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task GetForecast(string dataSetName, StreamWriter output, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>Gets the forecasts associated with a data set, written as CSV.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="StreamWriter"/> where the data should be written.</param>
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
        Task GetForecast(string dataSetName, StreamWriter output, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Removes the forecasts associated with a data set.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset for which to remove data.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task RemoveForecast(string dataSetName);

        /// <summary>
        /// Removes the forecasts associated with a data set.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset for which to remove data.</param>
        /// <param name="startDate"> Limits deletion to those on or after the specified date.</param>
        /// <param name="endDate">Limits deletion to those on or before the specified date.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/data/{dataSetName}/forecast</remarks>
        Task RemoveForecast(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate);

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
        Task RemoveForecast(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Describes the algorithms run and the models generated in the process of deciding how to generate the forecasts for all columns in a data set.
        /// </summary>
        /// <param name="dataSetName">The name of the data set.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast/model</remarks>
        Task<List<ForecastModel>> ListForecastModels(string dataSetName);

        /// <summary>
        /// Describes the algorithms run and the models generated in the process of deciding how to generate the forecasts for all columns in a data set.
        /// </summary>
        /// <param name="dataSetName">The name of the data set.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast/model</remarks>
        Task<List<ForecastModel>> ListForecastModels(string dataSetName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Describes the algorithms run and the models generated in the process of deciding how to generate the forecasts for all columns in a data set.
        /// </summary>
        /// <param name="dataSetName">The name of the data set.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast/model</remarks>
        Task<List<ForecastModel>> ListForecastModels(string dataSetName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Describes the algorithms run and the models generated in the process of deciding how to generate the forecasts for a specific column in a data set.
        /// </summary>
        /// <param name="dataSetName">The name of the data set.</param>
        /// <param name="targetColumn">The name of the column in the data set used in forecasting.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast/model/{targetColumn}</remarks>
        Task<ForecastModel> GetForecastModel(string dataSetName, string targetColumn);

        /// <summary>
        /// Describes the algorithms run and the models generated in the process of deciding how to generate the forecasts for a specific column in a data set.
        /// </summary>
        /// <param name="dataSetName">The name of the data set.</param>
        /// <param name="targetColumn">The name of the column in the data set used in forecasting.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast/model/{targetColumn}</remarks>
        Task<ForecastModel> GetForecastModel(string dataSetName, string targetColumn, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Describes the algorithms run and the models generated in the process of deciding how to generate the forecasts for a specific column in a data set.
        /// </summary>
        /// <param name="dataSetName">The name of the data set.</param>
        /// <param name="targetColumn">The name of the column in the data set used in forecasting.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}/forecast/model/{targetColumn}</remarks>
        Task<ForecastModel> GetForecastModel(string dataSetName, string targetColumn, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

    }
}
