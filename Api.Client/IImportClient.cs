using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{
    public interface IImportClient
    {
        
        Action<HttpRequestMessage, HttpResponseMessage> HttpMessageTransformer { get; set; }
        
        /// <summary>
        /// List imports that have been run. This will show information about them such as id and status
        /// </summary>
        /// <param name="query">The query criteria for the Imports to be returned</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The list of <see cref="ImportDetail"/> objects.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/imports</remarks>
        Task<ImportDetailList> List(ImportDetailQuery query = null, CancellationToken cancellationToken= default(CancellationToken));


        /// <summary>
        /// Retrieve an import that has been requested.  This will show information such as id and status
        /// </summary>
        /// <param name="id">The identifier of the import</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>     
        /// <returns>A <see cref="ImportDetail" /> populated with the import information</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/imports/{id}</remarks>
        Task<ImportDetail> Get(Guid id, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// Import data into the Nexosis Api from a file on AWS S3
        /// </summary>
        
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>     
        /// <remarks>POST of https://ml.nexosis.com/api/imports</remarks>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <returns>A <see cref="ImportDetail" /> populated with the import information</returns>
        Task<ImportDetail> ImportFromS3(ImportFromS3Request detail, CancellationToken cancellationToken = default(CancellationToken));
    }
}