using System;
using System.Collections.Generic;
using System.Threading;
using Nexosis.Api.Client.Utility;

namespace Nexosis.Api.Client.Model
{
    public class ModelSummaryQuery
    {
        /// <summary>
        /// Limits models to those for a particular data source
        /// </summary>
        public string DataSourceName { get; set; }

        /// <summary>
        /// Limits models to those created after this date
        /// </summary>
        public DateTimeOffset? CreatedAfterDate { get; set; }

        /// <summary>
        /// Limits models to those created before this date
        /// </summary>
        public DateTimeOffset? CreatedBeforeDate { get; set; }

        /// <summary>
        /// Paging info for the response
        /// </summary>
        public PagingInfo Page { get; set; }

        public IEnumerable<KeyValuePair<string, string>> ToParameters()
        {
            var builder = new ParameterBuilder();
            builder.Add("dataSourceName", DataSourceName);
            builder.Add("createdAfterDate", CreatedAfterDate);
            builder.Add("createdBeforeDate", CreatedBeforeDate);
            builder.Add(Page);
            return builder.GetParameters();
        }
    }
}