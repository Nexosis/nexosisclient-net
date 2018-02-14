using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{

    public interface IDataSetClient
    {
        
        Action<HttpRequestMessage, HttpResponseMessage> HttpMessageTransformer { get; set; }

        /// <summary>
        /// Save data in a dataset.
        /// </summary>
        /// <param name="source">A <see cref="IDataSetSource"/> containing the data.  Create one of these with <see cref="DataSet.From"/>.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="DataSetSummary"/> for the dataset created.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>PUT to https://ml.nexosis.com/v1/v1/{dataSetName}</remarks>
        Task<DataSetSummary> Create(IDataSetSource source, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the list of datasets that have been saved to the system, filtering by partial name match.
        /// </summary>
        /// <param name="query">A <see cref="DataSetSummaryQuery"/> with the filter criteria for the DataSets to retrieve.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A list of <see cref="DataSetSummary"/>.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/v1/data</remarks>
        Task<DataSetSummaryList> List(DataSetSummaryQuery query = null, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>Get the data in the set, written to a CSV file, optionally filtering it.</summary>
        /// <param name="query">A <see cref="DataSetDataQuery"/> with the filter criteria for retrieving data from the DataSet.  Create one of these with <see cref="DataSet.Where"/></param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/v1/data/{dataSetName}</remarks>
        Task<DataSetData> Get(DataSetDataQuery query, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>Remove data from a dataset or the entire set.</summary>
        /// <param name="criteria">A <see cref="DataSetRemoveCriteria"/> with the criteria for which data to remove</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/v1/data/{dataSetName}</remarks>
        Task Remove(DataSetRemoveCriteria criteria, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Retrieve statistics about a dataset
        /// </summary>
        /// <param name="dataSetName">The dataset whose stats should be retrieved</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <remarks>GET to https://ml.nexosis.com/v1/data/{dataSetName}/stats</remarks>
        Task<DataSourceStatsResult> Stats(string dataSetName, CancellationToken cancellationToken = default(CancellationToken));

    }
}