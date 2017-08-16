using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{
    public interface IViewClient
    {

        /// <summary>
        /// Lists views that have been saved to the system
        /// </summary>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns>A listing of views associated with your company</returns>
        Task<List<ViewSummary>> List();
        
        /// <summary>
        /// Lists views that have been saved to the system
        /// </summary>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns>A listing of views associated with your company</returns>
        Task<List<ViewSummary>> List(Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Lists views that have been saved to the system
        /// </summary>
        /// <param name="query">Query parameters to filter the list of views</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns>A listing of views associated with your company</returns>
        Task<List<ViewSummary>> List(ViewQuery query, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Lists views that have been saved to the system
        /// </summary>
        /// <param name="query">Query parameters to filter the list of views</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns>A listing of views associated with your company</returns>
        Task<List<ViewSummary>> List(ViewQuery query);

        /// <summary>
        /// Gets a view
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns>A <see cref="ViewDetail"/> with data about the view definition and the view data itself.</returns>
        Task<ViewDetail> Get(string viewName);

        /// <summary>
        /// Gets a view
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <param name="options">Filtering options for the data that comes back from the view</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns>A <see cref="ViewDetail"/> with data about the view definition and the view data itself.</returns>
        Task<ViewDetail> Get(string viewName, GetViewOptions options);

        /// <summary>
        /// Gets a view
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns>A <see cref="ViewDetail"/> with data about the view definition and the view data itself.</returns>
        Task<ViewDetail> Get(string viewName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a view
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <param name="options">Filtering options for the data that comes back from the view</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns>A <see cref="ViewDetail"/> with data about the view definition and the view data itself.</returns>
        Task<ViewDetail> Get(string viewName, GetViewOptions options, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Creates or updates a view
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <param name="view">The view definition</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns>A <see cref="ViewSummary"/> containing the view definition</returns>
        Task<ViewSummary> Create(string viewName, ViewInfo view);

        /// <summary>
        /// Creates or updates a view
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <param name="view">The view definition</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns>A <see cref="ViewSummary"/> containing the view definition</returns>
        Task<ViewSummary> Create(string viewName, ViewInfo view, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a view
        /// </summary>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <param name="viewName">The name of the view</param>
        /// <returns></returns>
        Task Remove(string viewName);

        /// <summary>
        /// Deletes a view
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <param name="options">Options for additional data to delete along with the view</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns></returns>
        Task Remove(string viewName, ViewDeleteOptions options);

        /// <summary>
        /// Deletes a view
        /// </summary>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <param name="viewName">The name of the view</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns></returns>
        Task Remove(string viewName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a view
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <param name="options">Options for additional data to delete along with the view</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns></returns>
        Task Remove(string viewName, ViewDeleteOptions options, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);
    }
}