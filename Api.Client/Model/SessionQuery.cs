using System;
using System.Collections.Generic;
using Nexosis.Api.Client.Utility;

namespace Nexosis.Api.Client.Model
{
    public class SessionQuery
    {
        /// <summary>
        /// Only sessions associated with this data source should be returned
        /// </summary>
        public string DataSourceName { get; set; }

        /// <summary>
        /// Only sessions requested after this date should be returned
        /// </summary>
        public DateTimeOffset? RequestedAfterDate { get; set; }

        /// <summary>
        /// Only sessions requested before this date should be returned
        /// </summary>
        public DateTimeOffset? RequestedBeforeDate { get; set; }

        /// <summary>
        /// Paging info for the response
        /// </summary>
        public PagingInfo Page { get; set; }

        internal IEnumerable<KeyValuePair<string, string>> ToParameters()
        {
            var builder = new ParameterBuilder();
            builder.Add("dataSourceName", DataSourceName);
            builder.Add("requestedAfterDate", RequestedAfterDate);
            builder.Add("requestedBeforeDate", RequestedBeforeDate);
            builder.Add(Page);
            return builder.GetParameters();
        }
    }
}