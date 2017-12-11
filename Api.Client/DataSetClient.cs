using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{
    public class DataSetClient : IDataSetClient
    {
        private readonly ApiConnection apiConnection;

        public Action<HttpRequestMessage, HttpResponseMessage> HttpMessageTransformer { get; set; }

        public DataSetClient(ApiConnection apiConnection)
        {
            this.apiConnection = apiConnection;
        }
        
        public async Task<DataSetSummary> Create(IDataSetSource source, CancellationToken cancellationToken = default(CancellationToken))
        {
            Argument.IsNotNullOrEmpty(source.Name, nameof(source.Name));
            
            switch (source)
            {
                    case DataSetDetailSource detail:
                        Argument.IsNotNull(detail.Data, nameof(detail.Data));
                        return await apiConnection.Put<DataSetSummary>($"data/{detail.Name}", null, detail.Data, HttpMessageTransformer, cancellationToken).ConfigureAwait(false);
                    case DataSetStreamSource stream:
                        Argument.IsNotNull(stream.Data, nameof(stream.Data));
                        return await apiConnection.Put<DataSetSummary>($"data/{stream.Name}", null, stream.Data, HttpMessageTransformer, cancellationToken).ConfigureAwait(false);
                    default:
                        throw new NotImplementedException($"No DataSet create supported for {source.GetType()}");
            }
        }

        public async Task<DataSetSummaryList> List(DataSetSummaryQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = query.ToParameters();
            
            var result = await apiConnection
                .Get<DataSetSummaryList>("data", parameters, HttpMessageTransformer, cancellationToken)
                .ConfigureAwait(false);

            return result;
        }

        public Task<DataSetData> Get(DataSetDataQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            Argument.IsNotNullOrEmpty(query.Name, nameof(query.Name));
            var parameters = query.ToParameters();
            return apiConnection.Get<DataSetData>($"data/{query.Name}", parameters, HttpMessageTransformer, cancellationToken);
        }

        public async Task Remove(DataSetRemoveCriteria criteria, CancellationToken cancellationToken = default(CancellationToken))
        {
            Argument.IsNotNullOrEmpty(criteria.Name, nameof(criteria.Name));
            
            var parameters = criteria.ToParameters();
            await apiConnection.Delete($"data/{criteria.Name}", parameters, HttpMessageTransformer, CancellationToken.None).ConfigureAwait(false);
        }
    }
    
}
