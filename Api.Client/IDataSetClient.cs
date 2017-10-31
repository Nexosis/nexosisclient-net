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
        /// Save data in a dataset.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset to which to add data.</param>
        /// <param name="data">A DataSetDetail containing the data.</param>
        /// <returns>A <see cref="DataSetSummary"/> for the dataset created.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>PUT to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetSummary> Create(string dataSetName, DataSetDetail data);

        /// <summary>
        /// Save data in a dataset.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset to which to add data.</param>
        /// <param name="data">A DataSetDetail containing the data.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns>A <see cref="DataSetSummary"/> for the dataset created.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>PUT to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetSummary> Create(string dataSetName, DataSetDetail data, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Save data in a dataset.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset to which to add data.</param>
        /// <param name="data">A DataSetDetail containing the data.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="DataSetSummary"/> for the dataset created.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>PUT to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetSummary> Create(string dataSetName, DataSetDetail data, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken);

        /// <summary>
        /// Save data in a dataset.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset to which to add data.</param>
        /// <param name="input">A reference to a <see cref="StreamReader"/> which contains the data you wish to save.</param>
        /// <returns>A <see cref="DataSetSummary"/> for the dataset created.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>PUT to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetSummary> Create(string dataSetName, StreamReader input);
        
        /// <summary>
        /// Save data in a dataset.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset to which to add data.</param>
        /// <param name="input">A reference to a <see cref="StreamReader"/> which contains the data you wish to save.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns>A <see cref="DataSetSummary"/> for the dataset created.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>PUT to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetSummary> Create(string dataSetName, StreamReader input, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Save data in a dataset.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset to which to add data.</param>
        /// <param name="input">A reference to a <see cref="StreamReader"/> which contains the data you wish to save.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="DataSetSummary"/> for the dataset created.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>PUT to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetSummary> Create(string dataSetName, StreamReader input, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken);

        /// <summary>
        /// Gets the list of all datasets that have been saved to the system.
        /// </summary>
        /// <param name="pageNumber">The page of the list results requested</param>
        /// <param name="pageSize">How many items per page to return</param>
        /// <returns>A list of <see cref="DataSetSummary"/>.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data</remarks>
        Task<List<DataSetSummary>> List(int pageNumber = 0, int pageSize = 50);

        /// <summary>
        /// Gets the list of datasets that have been saved to the system, filtering by partial name match.
        /// </summary>
        /// <param name="partialName">Limits results to only those datasets with names containing the specified value</param>
        /// <param name="pageNumber">The page of the list results requested</param>
        /// <param name="pageSize">How many items per page to return</param>
        /// <returns>A list of <see cref="DataSetSummary"/>.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data</remarks>
        Task<List<DataSetSummary>> List(string partialName, int pageNumber = 0, int pageSize = 50);

        /// <summary>
        /// Gets the list of datasets that have been saved to the system, filtering by partial name match.
        /// </summary>
        /// <param name="partialName">Limits results to only those datasets with names containing the specified value</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="pageNumber">The page of the list results requested</param>
        /// <param name="pageSize">How many items per page to return</param>
        /// <returns>A list of <see cref="DataSetSummary"/>.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data</remarks>
        Task<List<DataSetSummary>> List(string partialName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, int pageNumber = 0, int pageSize = 50);

        /// <summary>
        /// Gets the list of datasets that have been saved to the system, filtering by partial name match.
        /// </summary>
        /// <param name="partialName">Limits results to only those datasets with names containing the specified value</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <param name="pageNumber">The page of the list results requested</param>
        /// <param name="pageSize">How many items per page to return</param>
        /// <returns>A list of <see cref="DataSetSummary"/>.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data</remarks>
        Task<List<DataSetSummary>> List(string partialName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken, int pageNumber = 0, int pageSize = 50);

        /// <summary>Get the data in the set.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <returns><see cref="DataSetData" /></returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetData> Get(string dataSetName);

        /// <summary>Get the data in the set, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 1000).</param>
        /// <param name="includeColumns">Limits results to the specified columns of the dataset.</param>
        /// <returns><see cref="DataSetData" /></returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetData> Get(string dataSetName, int pageNumber, int pageSize, IEnumerable<string> includeColumns);

        /// <summary>Get the data in the set, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 1000).</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="includeColumns">Limits results to the specified columns of the dataset.</param>
        /// <returns><see cref="DataSetData" /></returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetData> Get(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns);

        /// <summary>Get the data in the set, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 1000).</param>
        /// <param name="includeColumns">Limits results to the specified columns of the dataset.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <returns><see cref="DataSetData" /></returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetData> Get(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>Get the data in the set, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 1000).</param>
        /// <param name="includeColumns">Limits results to the specified columns of the dataset.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><see cref="DataSetData" /></returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task<DataSetData> Get(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>Get the data in the set, written to a CSV file.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="StreamWriter"/> where the data should be written.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task Get(string dataSetName, StreamWriter output);

        /// <summary>Get the data in the set, written to a CSV file, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="StreamWriter"/> where the data should be written.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 1000).</param>
        /// <param name="includeColumns">Limits results to the specified columns of the dataset.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task Get(string dataSetName, StreamWriter output, int pageNumber, int pageSize, IEnumerable<string> includeColumns);

        /// <summary>Get the data in the set, written to a CSV file, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="StreamWriter"/> where the data should be written.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 1000).</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="includeColumns">Limits results to the specified columns of the dataset.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task Get(string dataSetName, StreamWriter output, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns);

        /// <summary>Get the data in the set, written to a CSV file, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="StreamWriter"/> where the data should be written.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 1000).</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="includeColumns">Limits results to the specified columns of the dataset.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task Get(string dataSetName, StreamWriter output, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>Get the data in the set, written to a CSV file, optionally filtering it.</summary>
        /// <param name="dataSetName">Name of the dataset for which to retrieve data.</param>
        /// <param name="output">A <see cref="StreamWriter"/> where the data should be written.</param>
        /// <param name="pageNumber">Zero-based page number of results to retrieve.</param>
        /// <param name="pageSize">Count of results to retrieve in each page (max 1000).</param>
        /// <param name="startDate"> Limits results to those on or after the specified date.</param>
        /// <param name="endDate">Limits results to those on or before the specified date.</param>
        /// <param name="includeColumns">Limits results to the specified columns of the dataset.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task Get(string dataSetName, StreamWriter output, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>Remove data from a dataset or the entire set.</summary>
        /// <param name="dataSetName">Name of the dataset from which to remove data.</param>
        /// <param name="options">Controls the options associated with the removal.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task Remove(string dataSetName, DataSetDeleteOptions options);

        /// <summary>Remove data from a dataset or the entire set.</summary>
        /// <param name="dataSetName">Name of the dataset from which to remove data.</param>
        /// <param name="startDate">Limits data removed to those on or after the specified date.</param>
        /// <param name="endDate">Limits data removed to those on or before the specified date.</param>
        /// <param name="options">Controls the options associated with the removal.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task Remove(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, DataSetDeleteOptions options);

        /// <summary>Remove data from a dataset or the entire set.</summary>
        /// <param name="dataSetName">Name of the dataset from which to remove data.</param>
        /// <param name="startDate">Limits data removed to those on or after the specified date.</param>
        /// <param name="endDate">Limits data removed to those on or before the specified date.</param>
        /// <param name="options">Controls the options associated with the removal.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task Remove(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, DataSetDeleteOptions options,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>Remove data from a dataset or the entire set.</summary>
        /// <param name="dataSetName">Name of the dataset from which to remove data.</param>
        /// <param name="startDate">Limits data removed to those on or after the specified date.</param>
        /// <param name="endDate">Limits data removed to those on or before the specified date.</param>
        /// <param name="options">Controls the options associated with the removal.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/data/{dataSetName}</remarks>
        Task Remove(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, DataSetDeleteOptions options,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);
    }
}