using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{

    public class ViewClient : IViewClient
    {
        private readonly ApiConnection apiConnection;

        public ViewClient(ApiConnection apiConnection)
        {
            this.apiConnection = apiConnection;
            
        }
        
        public Action<HttpRequestMessage, HttpResponseMessage> HttpMessageTransformer { get; set; }
        
        public async Task<ViewDefinitionList> List(ViewQuery viewQuery = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = viewQuery.ToParameters();
            
            return await apiConnection.Get<ViewDefinitionList>($"views", parameters, HttpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ViewDetail> Get(ViewDataQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            Argument.IsNotNullOrEmpty(query?.Name, nameof(ViewDataQuery.Name));
            
            var parameters = query.ToParameters();

            return await apiConnection.Get<ViewDetail>($"views/{query.Name}", parameters, HttpMessageTransformer, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<ViewDefinition> Create(string viewName, ViewInfo view, CancellationToken cancellationToken = default(CancellationToken))
        {
            Argument.IsNotNullOrEmpty(viewName, nameof(viewName));
            Argument.IsNotNull(view, nameof(view));
            
            return await apiConnection.Put<ViewDefinition>($"views/{viewName}", null, view, HttpMessageTransformer, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task Remove(ViewDeleteCriteria criteria, CancellationToken cancellationToken = default(CancellationToken))
        {
            Argument.IsNotNullOrEmpty(criteria?.Name, nameof(criteria.Name));

            var parameters = criteria.ToParameters();

            await apiConnection.Delete($"views/{criteria.Name}", parameters, HttpMessageTransformer, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}