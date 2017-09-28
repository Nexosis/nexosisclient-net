using Nexosis.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Nexosis.Api.Client
{
    public interface IModelClient
    {
        /// <summary>
        /// Gets a model
        /// </summary>
        /// <param name="id">The id of the model.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns>A <see cref="ModelSummary"/> with information about the model.</returns>
        /// <remarks>GET of https://ml.nexosis.com/api/model/{modelId}</remarks>
        Task<ModelSummary> Get(Guid id);

        /// <summary>
        /// Gets the list of all models that have been created.
        /// </summary>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns>A list of <see cref="ModelSummary"/>.</returns>
        /// <remarks>GET of https://ml.nexosis.com/api/model</remarks>
        Task<List<ModelSummary>>List();

        /// <summary>
        /// Gets the list of all models that have been created.
        /// </summary>
        /// <param name="page">zero index page of the results to get</param>
        /// <param name="pageSize">number of items per page</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns>A list of <see cref="ModelSummary"/>.</returns>
        /// <remarks>GET of https://ml.nexosis.com/api/model</remarks>
        Task<List<ModelSummary>>List(int page, int pageSize);

        /// <summary>
        /// Gets the list of all models that have been created.
        /// </summary>
        /// <param name="dataSourceName">Limits models to those for a particular data source.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns>A list of <see cref="ModelSummary"/>.</returns>
        /// <remarks>GET of https://ml.nexosis.com/api/model</remarks>
        Task<List<ModelSummary>>List(string dataSourceName);

        /// <summary>
        /// Gets the list of all models that have been created.
        /// </summary>
        /// <param name="dataSourceName">Limits models to those for a particular data source.</param>
        /// <param name="page">zero index page of the results to get</param>
        /// <param name="pageSize">number of items per page</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns>A list of <see cref="ModelSummary"/>.</returns>
        /// <remarks>GET of https://ml.nexosis.com/api/model</remarks>
        Task<List<ModelSummary>>List(string dataSourceName, int page, int pageSize);

        /// <summary>
        /// Gets the list of all models that have been created.
        /// </summary>
        /// <param name="dataSourceName">Limits models to those for a particular data source.</param>
        /// <param name="createdAfterDate">Limits sessions to those requested on or after the specified date.</param>
        /// <param name="createdBeforeDate">Limits sessions to those requested on or before the specified date.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns>A list of <see cref="ModelSummary"/>.</returns>
        /// <remarks>GET of https://ml.nexosis.com/api/model</remarks>
        Task<List<ModelSummary>>List(string dataSourceName, DateTimeOffset createdAfterDate, DateTimeOffset createdBeforeDate);

        /// <summary>
        /// Gets the list of all models that have been created.
        /// </summary>
        /// <param name="dataSourceName">Limits models to those for a particular data source.</param>
        /// <param name="createdAfterDate">Limits sessions to those requested on or after the specified date.</param>
        /// <param name="createdBeforeDate">Limits sessions to those requested on or before the specified date.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns>A list of <see cref="ModelSummary"/>.</returns>
        /// <remarks>GET of https://ml.nexosis.com/api/model</remarks>
        Task<List<ModelSummary>>List(string dataSourceName, DateTimeOffset createdAfterDate, DateTimeOffset createdBeforeDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Gets the list of all models that have been created.
        /// </summary>
        /// <param name="dataSourceName">Limits models to those for a particular data source.</param>
        /// <param name="createdAfterDate">Limits sessions to those requested on or after the specified date.</param>
        /// <param name="createdBeforeDate">Limits sessions to those requested on or before the specified date.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns>A list of <see cref="ModelSummary"/>.</returns>
        /// <remarks>GET of https://ml.nexosis.com/api/model</remarks>
        Task<List<ModelSummary>>List(string dataSourceName, DateTimeOffset createdAfterDate, DateTimeOffset createdBeforeDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);
    }
}