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
		
		Action<HttpRequestMessage, HttpResponseMessage> HttpMessageTransformer { get; set; }
		
		/// <summary>
		/// Lists views that have been saved to the system
		/// </summary>
		/// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
		/// <returns>A listing of views associated with your company</returns>
		Task<ViewDefinitionList> List(ViewQuery viewQuery = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Gets a view
		/// </summary>
		/// <param name="query">The query critera for the data returned</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
		/// <returns>A <see cref="ViewDetail"/> with data about the view definition and the view data itself.</returns>
		Task<ViewDetail> Get(ViewDataQuery query, CancellationToken cancellationToken = default(CancellationToken));


		/// <summary>
		/// Creates a view
		/// </summary>
		/// <param name="viewName">The name of the view</param>
		/// <param name="view">The <see cref="ViewInfo"/> with the specifics of the view</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>The definition of the created view</returns>
		Task<ViewDefinition> Create(string viewName, ViewInfo view, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Deletes a view
		/// </summary>
		/// <param name="criteria">The criteria for the view delete as well as which data related to the view to delete</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns></returns>
		Task Remove(ViewDeleteCriteria criteria, CancellationToken cancellationToken = default(CancellationToken));
	}
}