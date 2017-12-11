using System;

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

        
    }
}