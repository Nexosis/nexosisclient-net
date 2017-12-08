using System;
using System.Collections.Generic;
using Nexosis.Api.Client.Utility;

namespace Nexosis.Api.Client.Model
{
    public class SessionRemoveCriteria
    {
        /// <summary>
        /// The sessions removed should be only those based on this DataSource
        /// </summary>
        public string DataSourceName { get; set; }

        /// <summary>
        /// Only sessions of this type should be removed
        /// </summary>
        public SessionType? Type { get; set; }

        /// <summary>
        /// Only sessions requested after this date should be removed
        /// </summary>
        public DateTimeOffset? RequestedAfterDate { get; set; }

        /// <summary>
        /// Only sessions requested before this date should be removed
        /// </summary>
        public DateTimeOffset? RequestedBeforeDate { get; set; }


        internal IEnumerable<KeyValuePair<string, string>> ToParameters()
        {
            var builder = new ParameterBuilder();
            builder.Add("dataSourceName", DataSourceName);
            builder.Add("requestedAfterDate", RequestedAfterDate);
            builder.Add("requestedBeforeDate", RequestedBeforeDate);
            builder.Add("type", Type);
            return builder.GetParameters();
        }
    }
}