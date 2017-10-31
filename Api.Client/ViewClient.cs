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

        public Task<List<ViewDefinition>> List()
        {
            return List(null, CancellationToken.None);
        }

        public Task<List<ViewDefinition>> List(Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            return List(new ViewQuery(), httpMessageTransformer, cancellationToken);
        }

        private class ViewListResponse : IPagedList<ViewDefinition>
        {
            [Newtonsoft.Json.JsonProperty("items")]
            public List<ViewDefinition> Items { get; set; }
            public int PageSize { get; set; }
            public int PageNumber { get; set; }
            public int TotalPages { get; set; }
            public int TotalCount { get; set; }
            public List<Link> Links { get; set; }
        }

        public async Task<List<ViewDefinition>> List(ViewQuery query, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
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
            return new PagedList<ViewDefinition>(await apiConnection.Get<ViewListResponse>($"views", parameters, httpMessageTransformer, cancellationToken).ConfigureAwait(false));
        }

        public Task<List<ViewDefinition>> List(ViewQuery query)
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
            Argument.IsNotNullOrEmpty(viewName, nameof(viewName));

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

            if (options?.Include?.Any() == true)
            {
                var includes = options.Include.Select(ic => new KeyValuePair<string, string>("include", ic));
                getParameters = parameters.Union(includes);
            }

            return await apiConnection.Get<ViewDetail>($"views/{viewName}", getParameters, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }

        public Task<ViewDefinition> Create(string viewName, ViewInfo view)
        {
            return Create(viewName, view, null, CancellationToken.None);
        }

        public Task<ViewDefinition> Create(string viewName, string primaryDataSetName, string joinDataSetName, Dictionary<string, ColumnMetadata> columns)
        {
            return Create(viewName, primaryDataSetName, joinDataSetName, columns, null, CancellationToken.None);
        }

        public Task<ViewDefinition> Create(string viewName, string primaryDataSetName, string joinDataSetName, Dictionary<string, ColumnMetadata> columns,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            return Create(viewName, new ViewInfo()
            {
                DataSetName = primaryDataSetName,
                Columns = columns,
                Joins = new[]
                {
                    new JoinMetadata() {DataSet = new DataSetJoinSource() {Name = joinDataSetName}},
                }
            }, httpMessageTransformer, cancellationToken);
        }

        public async Task<ViewDefinition> Create(string viewName, ViewInfo view, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNullOrEmpty(viewName, nameof(viewName));
            Argument.IsNotNull(view, nameof(view));
            return await apiConnection.Put<ViewDefinition>($"views/{viewName}", null, view, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ViewDefinition> Create(string viewName, string primaryDataSetName, string calendarName, string timeZone, Dictionary<string, ColumnMetadata> columns)
        {
            var viewDefinition = new ViewInfo
            {
                Columns = columns,
                DataSetName = primaryDataSetName,
                Joins = new[]
                {
                    new JoinMetadata{ Calendar = new CalendarJoinSource { Name = calendarName, TimeZone = timeZone } }
                }
            };
            return await this.Create(viewName, viewDefinition, null, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<ViewDefinition> Create(string viewName, string primaryDataSetName, Uri iCalUri, string timeZone, Dictionary<string, ColumnMetadata> columns)
        {
            var viewDefinition = new ViewInfo
            {
                Columns = columns,
                DataSetName = primaryDataSetName,
                Joins = new[]
                {
                    new JoinMetadata{ Calendar = new CalendarJoinSource { Url = iCalUri.AbsoluteUri, TimeZone = timeZone } }
                }
            };
            return await this.Create(viewName, viewDefinition, null, CancellationToken.None).ConfigureAwait(false);
        }

        public Task Remove(string viewName)
        {
            return Remove(viewName, new ViewDeleteOptions());
        }

        public Task Remove(string viewName, ViewDeleteOptions options)
        {
            return Remove(viewName, options, null, CancellationToken.None);
        }

        public Task Remove(string viewName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            return Remove(viewName, new ViewDeleteOptions(), httpMessageTransformer, cancellationToken);
        }

        public async Task Remove(string viewName, ViewDeleteOptions options, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNullOrEmpty(viewName, nameof(viewName));

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