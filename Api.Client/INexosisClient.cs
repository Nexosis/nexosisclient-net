using System;
using System.Net.Http;
using System.Threading;
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
        Task<AccountBalance> GetAccountBalance();

        /// <summary>Gets the current account balance.</summary>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions</remarks>
        Task<AccountBalance> GetAccountBalance(Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer);

        /// <summary>Gets the current account balance.</summary>
        /// <param name="httpMessageTransformer">A function that is called immediately before sending the request and after receiving a response which allows for message transformation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions</remarks>
        Task<AccountBalance> GetAccountBalance(Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken);

        /// <summary>
        /// Access to the Session based operations in the API.
        /// </summary>
        ISessionClient Sessions { get; }

        /// <summary>
        /// Access to the DataSet based operations in the API.
        /// </summary>
        IDataSetClient DataSets { get; }
        
        /// <summary>
        /// Access to the Import operations in the API
        /// </summary>
        IImportClient Imports { get; }
        
        /// <summary>
        /// Access to the View operations in the API
        /// </summary>
        IViewClient Views { get; }
    }
}
