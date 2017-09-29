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
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns>A listing of views associated with your company</returns>
        Task<List<ViewDefinition>> List();
        
        /// <summary>
        /// Lists views that have been saved to the system
        /// </summary>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns>A listing of views associated with your company</returns>
        Task<List<ViewDefinition>> List(Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Lists views that have been saved to the system
        /// </summary>
        /// <param name="query">Query parameters to filter the list of views</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns>A listing of views associated with your company</returns>
        Task<List<ViewDefinition>> List(ViewQuery query, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Lists views that have been saved to the system
        /// </summary>
        /// <param name="query">Query parameters to filter the list of views</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns>A listing of views associated with your company</returns>
        Task<List<ViewDefinition>> List(ViewQuery query);

        /// <summary>
        /// Gets a view
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns>A <see cref="ViewDetail"/> with data about the view definition and the view data itself.</returns>
        Task<ViewDetail> Get(string viewName);

        /// <summary>
        /// Gets a view
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <param name="options">Filtering options for the data that comes back from the view</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns>A <see cref="ViewDetail"/> with data about the view definition and the view data itself.</returns>
        Task<ViewDetail> Get(string viewName, GetViewOptions options);

        /// <summary>
        /// Gets a view
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns>A <see cref="ViewDetail"/> with data about the view definition and the view data itself.</returns>
        Task<ViewDetail> Get(string viewName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a view
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <param name="options">Filtering options for the data that comes back from the view</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns>A <see cref="ViewDetail"/> with data about the view definition and the view data itself.</returns>
        Task<ViewDetail> Get(string viewName, GetViewOptions options, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Creates or updates a view
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <param name="view">The view definition</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns>A <see cref="ViewDefinition"/> containing the view definition</returns>
        Task<ViewDefinition> Create(string viewName, ViewInfo view);

        /// <summary>
        /// Creates or updates a view
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <param name="primaryDataSetName">The primary data set from which the view is based</param>
        /// <param name="joinDataSetName">The data set to join to on its timestamp column</param>
        /// <param name="columns">Column definitions for the resulting view</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns>A <see cref="ViewDefinition"/> containing the view definition</returns>
        Task<ViewDefinition> Create(string viewName, string primaryDataSetName, string joinDataSetName, Dictionary<string, ColumnMetadata> columns);

        /// <summary>
        /// Creates or updates a view
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <param name="primaryDataSetName">The primary data set from which the view is based</param>
        /// <param name="joinDataSetName">The data set to join to on its timestamp column</param>
        /// <param name="columns">Column definitions for the resulting view</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns>A <see cref="ViewDefinition"/> containing the view definition</returns>
        Task<ViewDefinition> Create(string viewName, string primaryDataSetName, string joinDataSetName,
            Dictionary<string, ColumnMetadata> columns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Creates or updates a view
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <param name="view">The view definition</param>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns>A <see cref="ViewDefinition"/> containing the view definition</returns>
        Task<ViewDefinition> Create(string viewName, ViewInfo view, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Create the specified view with the given calendar name
        /// </summary>
        /// <returns>The definition of the created view</returns>
        /// <param name="viewName">View name.</param>
        /// <param name="primaryDataSetName">Primary data set name.</param>
        /// <param name="calendarName">Calendar name.</param>
        /// <param name="timeZone">Time zone id from tz-db to set the calendar events' time zone</param>
        /// <param name="columns">column metadata to override column information from the dataset</param>
        Task<ViewDefinition> Create(string viewName, string primaryDataSetName, string calendarName, string timeZone, Dictionary<string, ColumnMetadata> columns);

		/// <summary>
		/// Create the specified view with the calendar events identified by iCal
		/// </summary>
		/// <returns>The definition of the created view</returns>
		/// <param name="viewName">View name.</param>
		/// <param name="primaryDataSetName">Primary data set name.</param>
		/// <param name="iCalUri">Uri of publicly available iCal</param>
		/// <param name="timeZone">Time zone id from tz-db to set the calendar events' time zone</param>
		/// <param name="columns">column metadata to override column information from the dataset</param>
        Task<ViewDefinition> Create(string viewName, string primaryDataSetName, Uri iCalUri, string timeZone, Dictionary<string, ColumnMetadata> columns);

        /// <summary>
        /// Deletes a view
        /// </summary>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <param name="viewName">The name of the view</param>
        /// <returns></returns>
        Task Remove(string viewName);

        /// <summary>
        /// Deletes a view
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <param name="options">Options for additional data to delete along with the view</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns></returns>
        Task Remove(string viewName, ViewDeleteOptions options);

        /// <summary>
        /// Deletes a view
        /// </summary>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
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
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns></returns>
        Task Remove(string viewName, ViewDeleteOptions options, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);
    }
}