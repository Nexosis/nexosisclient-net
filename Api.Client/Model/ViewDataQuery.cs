using System;
using System.Collections.Generic;
using System.Linq;
using Nexosis.Api.Client.Utility;

namespace Nexosis.Api.Client.Model
{
    public class ViewDataQuery
    {
        public ViewDataQuery(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        /// <summary>
        /// Limits view data rows to those on or after the specified date
        /// </summary>
        public DateTimeOffset? StartDate { get; set; }

        /// <summary>
        /// Limits view data rows to those on or before the specified date
        /// </summary>
        public DateTimeOffset? EndDate { get; set; }

        /// <summary>
        /// Includes only the specified columns in view data rows
        /// </summary>
        public IEnumerable<string> Include { get; set; }

        /// <summary>
        /// The paging info for the response
        /// </summary>
        public PagingInfo Page { get; set; }

        internal List<KeyValuePair<string, string>> ToParameters()
        {
            var builder = new ParameterBuilder();
            builder.Add("startDate", StartDate);
            builder.Add("endDate", EndDate);
            builder.Add(Page);
            if (Include != null && Include.Any())
            {
                foreach (var col in Include)
                {
                    builder.Add("include", col);
                }
            }

            return builder.GetParameters();
        }
    }
}