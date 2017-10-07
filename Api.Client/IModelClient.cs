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
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns>A <see cref="ModelSummary"/> with information about the model.</returns>
        /// <remarks>GET of https://ml.nexosis.com/api/model/{modelId}</remarks>
        Task<ModelSummary> Get(Guid id);

        /// <summary>
        /// Gets the list of all models that have been created.
        /// </summary>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns>A list of <see cref="ModelSummary"/>.</returns>
        /// <remarks>GET of https://ml.nexosis.com/api/model</remarks>
        Task<List<ModelSummary>>List();

        /// <summary>
        /// Gets the list of all models that have been created.
        /// </summary>
        /// <param name="page">zero index page of the results to get</param>
        /// <param name="pageSize">number of items per page</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns>A list of <see cref="ModelSummary"/>.</returns>
        /// <remarks>GET of https://ml.nexosis.com/api/model</remarks>
        Task<List<ModelSummary>>List(int page, int pageSize);

        /// <summary>
        /// Gets the list of all models that have been created.
        /// </summary>
        /// <param name="dataSourceName">Limits models to those for a particular data source.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns>A list of <see cref="ModelSummary"/>.</returns>
        /// <remarks>GET of https://ml.nexosis.com/api/model</remarks>
        Task<List<ModelSummary>>List(string dataSourceName);

        /// <summary>
        /// Gets the list of all models that have been created.
        /// </summary>
        /// <param name="dataSourceName">Limits models to those for a particular data source.</param>
        /// <param name="page">zero index page of the results to get</param>
        /// <param name="pageSize">number of items per page</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns>A list of <see cref="ModelSummary"/>.</returns>
        /// <remarks>GET of https://ml.nexosis.com/api/model</remarks>
        Task<List<ModelSummary>>List(string dataSourceName, int page, int pageSize);

        /// <summary>
        /// Gets the list of all models that have been created.
        /// </summary>
        /// <param name="dataSourceName">Limits models to those for a particular data source.</param>
        /// <param name="createdAfterDate">Limits sessions to those requested on or after the specified date.</param>
        /// <param name="createdBeforeDate">Limits sessions to those requested on or before the specified date.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
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
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
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
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns>A list of <see cref="ModelSummary"/>.</returns>
        /// <remarks>GET of https://ml.nexosis.com/api/model</remarks>
        Task<List<ModelSummary>>List(string dataSourceName, DateTimeOffset createdAfterDate, DateTimeOffset createdBeforeDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Predicts target values for a set of features using the specified model. 
        /// </summary>
        /// <param name="modelId">The identifier of the model to use for prediction.</param>
        /// <param name="data">Column and value pairs of the features which will be used in this prediction.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns><see cref="ModelPredictionResult"/> containing the predicted results and features that were sent.</returns>
        /// <remarks>POST of https://ml.nexosis.com/api/model/{modelId}/predict</remarks>
        Task<ModelPredictionResult> Predict(Guid modelId, List<Dictionary<string, string>> data);

        /// <summary>
        /// Predicts target values for a set of features using the specified model. 
        /// </summary>
        /// <param name="modelId">The identifier of the model to use for prediction.</param>
        /// <param name="data">Column and value pairs of the features which will be used in this prediction.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns><see cref="ModelPredictionResult"/> containing the predicted results and features that were sent.</returns>
        /// <remarks>POST of https://ml.nexosis.com/api/model/{modelId}/predict</remarks>
        Task<ModelPredictionResult> Predict(Guid modelId, List<Dictionary<string, string>> data, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Predicts target values for a set of features using the specified model. 
        /// </summary>
        /// <param name="modelId">The identifier of the model to use for prediction.</param>
        /// <param name="data">Column and value pairs of the features which will be used in this prediction.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns><see cref="ModelPredictionResult"/> containing the predicted results and features that were sent.</returns>
        /// <remarks>POST of https://ml.nexosis.com/api/model/{modelId}/predict</remarks>
        Task<ModelPredictionResult> Predict(Guid modelId, List<Dictionary<string, string>> data, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Removes a single Model from your account
        /// </summary>
        /// <param name="modelId">The identifier of the model which should be deleted.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>DELETE of https://ml.nexosis.com/api/model/{modelId}</remarks>
        Task Remove(Guid modelId);

        /// <summary>
        /// Removes a single Model from your account
        /// </summary>
        /// <param name="modelId">The identifier of the model which should be deleted.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>DELETE of https://ml.nexosis.com/api/model/{modelId}</remarks>
        Task Remove(Guid modelId, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Removes a single Model from your account
        /// </summary>
        /// <param name="modelId">The identifier of the model which should be deleted.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>DELETE of https://ml.nexosis.com/api/model/{modelId}</remarks>
        Task Remove(Guid modelId, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// Removes a Models from your account which match the specified parameters
        /// </summary>
        /// <param name="dataSourceName">Limits models removed to those with the given data source name.</param>
        /// <param name="createdAfterDate">Limits sessions to those requested on or after the specified date.</param>
        /// <param name="createdBeforeDate">Limits sessions to those requested on or before the specified date.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>DELETE of https://ml.nexosis.com/api/model</remarks>
        Task Remove(string dataSourceName, DateTimeOffset createdAfterDate, DateTimeOffset createdBeforeDate);

        /// <summary>
        /// Removes a Models from your account which match the specified parameters
        /// </summary>
        /// <param name="dataSourceName">Limits models removed to those with the given data source name.</param>
        /// <param name="createdAfterDate">Limits sessions to those requested on or after the specified date.</param>
        /// <param name="createdBeforeDate">Limits sessions to those requested on or before the specified date.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>DELETE of https://ml.nexosis.com/api/model</remarks>
        Task Remove(string dataSourceName, DateTimeOffset createdAfterDate, DateTimeOffset createdBeforeDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>
        /// Removes a Models from your account which match the specified parameters
        /// </summary>
        /// <param name="dataSourceName">Limits models removed to those with the given data source name.</param>
        /// <param name="createdAfterDate">Limits sessions to those requested on or after the specified date.</param>
        /// <param name="createdBeforeDate">Limits sessions to those requested on or before the specified date.</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>DELETE of https://ml.nexosis.com/api/model</remarks>
        Task Remove(string dataSourceName, DateTimeOffset createdAfterDate, DateTimeOffset createdBeforeDate, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);
 
    }
}