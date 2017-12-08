using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{
    public class ImportClient : IImportClient
    {
        private readonly ApiConnection apiConnection;
        

        public ImportClient(ApiConnection apiConnection)
        {
            this.apiConnection = apiConnection;
        }

        public Action<HttpRequestMessage, HttpResponseMessage> HttpMessageTransformer { get; set; }
        
        public async Task<ImportDetailList> List(ImportDetailQuery query = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = query?.ToParameters();
            
            var list = await apiConnection
                .Get<ImportDetailList>("imports", parameters, HttpMessageTransformer, cancellationToken).ConfigureAwait(false);

            return list;
        }

        public async Task<ImportDetail> Get(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await apiConnection
                .Get<ImportDetail>($"imports/{id}", null, HttpMessageTransformer, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<ImportDetail> ImportFromS3(ImportFromS3Request detail, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await apiConnection
                .Post<ImportDetail>("imports/s3", null, detail, HttpMessageTransformer, cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
    
}