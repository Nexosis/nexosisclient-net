using System;
using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class GetViewOptions
    {
        /// <summary>
        /// Limits view data rows to those on or after the specified date
        /// </summary>
        public DateTimeOffset? StartDate { get; set; }

        /// <summary>
        /// Limits view data rows to those on or before the specified date
        /// </summary>
        public DateTimeOffset? EndDate { get; set; }

        /// <summary>
        /// Zero-based page number of view data rows to retrieve 
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// Count of view data rows to retrieve in each page (max 1000)
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// Includes only the specified columns in view data rows
        /// </summary>
        public IEnumerable<string> Include { get; set; }
    }
}