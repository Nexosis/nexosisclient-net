using System;
using System.Collections.Generic;
using Nexosis.Api.Client.Utility;

namespace Nexosis.Api.Client.Model
{
    public class ImportDetailQuery
    {
        /// <summary>
        /// Limit imports to those with the specified name
        /// </summary>
        public string DataSetName { get; set; }

        /// <summary>
        /// Limit imports to those requested before this date
        /// </summary>
        public DateTimeOffset? RequestedBeforeDate { get; set; }

        /// <summary>
        /// Limit imports to those requested after this date
        /// </summary>
        public DateTimeOffset? RequestedAfterDate { get; set; }

        /// <summary>
        /// Paging parameters for the response
        /// </summary>
        public PagingInfo Page { get; set; }

        internal IEnumerable<KeyValuePair<string, string>> ToParameters()
        {
            var builder = new ParameterBuilder();
            var parameters = new Dictionary<string, string>();
            builder.Add("dataSetName", DataSetName);
            builder.Add("requestedAfterDate", RequestedAfterDate);
            builder.Add("requestedBeforeDate", RequestedBeforeDate);
            builder.Add(Page);
            return builder.GetParameters();

        }
    }
}