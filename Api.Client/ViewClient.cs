using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
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

        public Task<List<ViewSummary>> List()
        {
            return List(null, CancellationToken.None);
        }

        public Task<List<ViewSummary>> List(Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            return List(new ViewQuery(), httpMessageTransformer, cancellationToken);
        }

        private class ViewListResponse
        {
            public List<ViewSummary> items { get; set; }
        }
        
        public async Task<List<ViewSummary>> List(ViewQuery query, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(query?.DataSetName))
            {
                parameters.Add(nameof(query.DataSetName), query.DataSetName);
            }
            if (!string.IsNullOrEmpty(query?.PartialName))
            {
                parameters.Add(nameof(query.PartialName), query.PartialName);
            }
            if (query?.Page != null)
            {
                parameters.Add(nameof(query.Page), query.Page.ToString());
            }
            if (query?.PageSize != null)
            {
                parameters.Add(nameof(query.PageSize), query.PageSize.ToString());
            }

            return (await apiConnection.Get<ViewListResponse>($"views", parameters, httpMessageTransformer, cancellationToken).ConfigureAwait(false))?.items;
        }

        public Task<List<ViewSummary>> List(ViewQuery query)
        {
            return List(query, null, CancellationToken.None);
        }

        public Task<ViewDetail> Get(string viewName)
        {
            return Get(viewName, new GetViewOptions());
        }

        public Task<ViewDetail> Get(string viewName, GetViewOptions options)
        {
            return Get(viewName, options, null, CancellationToken.None);
        }

        public Task<ViewDetail> Get(string viewName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            return Get(viewName, new GetViewOptions(), httpMessageTransformer, cancellationToken);
        }

        public async Task<ViewDetail> Get(string viewName, GetViewOptions options, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string>();
            if (options?.StartDate != null)
            {
                parameters.Add(nameof(options.StartDate), options.StartDate.Value.ToString("O"));
            }
            if (options?.EndDate != null)
            {
                parameters.Add(nameof(options.EndDate), options.EndDate.Value.ToString("O"));
            }
            if (options?.Page != null)
            {
                parameters.Add(nameof(options.Page), options.Page.ToString());
            }
            if (options?.PageSize != null)
            {
                parameters.Add(nameof(options.PageSize), options.PageSize.ToString());
            }

            IEnumerable<KeyValuePair<string, string>> getParameters = parameters;

            if (options?.Include.Any() == true)
            {
                var includes = options.Include.Select(ic => new KeyValuePair<string, string>("include", ic));
                getParameters = parameters.Union(includes);
            }
            
            return await apiConnection.Get<ViewDetail>($"views/{viewName}", getParameters, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }

        public Task<ViewSummary> Put(string viewName, ViewInfo view)
        {
            return Put(viewName, view, null, CancellationToken.None);
        }

        public async Task<ViewSummary> Put(string viewName, ViewInfo view, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            return await apiConnection.Put<ViewSummary>($"views/{viewName}", null, view, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }

        public Task Delete(string viewName)
        {
            return Delete(viewName, new ViewDeleteOptions());
        }

        public Task Delete(string viewName, ViewDeleteOptions options)
        {
            return Delete(viewName, options, null, CancellationToken.None);
        }

        public Task Delete(string viewName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            return Delete(viewName, new ViewDeleteOptions(), httpMessageTransformer, cancellationToken);
        }

        public async Task Delete(string viewName, ViewDeleteOptions options, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var parameters = new List<KeyValuePair<string, string>>();

            if (options?.Cascade != null)
            {
                if ((options.Cascade & ViewCascadeOptions.CascadeSessions) != 0)
                {
                    parameters.Add(new KeyValuePair<string, string>("cascade", "sessions"));
                }
            }
            
            await apiConnection.Delete($"views/{viewName}", parameters, httpMessageTransformer, cancellationToken).ConfigureAwait(false);

        }
    }
}