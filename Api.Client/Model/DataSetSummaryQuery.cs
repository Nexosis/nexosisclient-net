using System.Collections.Generic;
using Nexosis.Api.Client.Utility;

namespace Nexosis.Api.Client.Model
{
    /// <summary>
    /// Query criteria to be used when retrieving DataSets
    /// </summary>
    public class DataSetSummaryQuery
    {
        /// <summary>
        /// All DataSets whose name contains this string will be reuturned  
        /// </summary>
        public string PartialName { get; set; }

        /// <summary>
        /// Paging information for the response
        /// </summary>
        public PagingInfo Page { get; set; }

        internal List<KeyValuePair<string, string>> ToParameters()
        {
            var builder = new ParameterBuilder();
            builder.Add("partialName", PartialName);
            builder.Add(Page);
            return builder.GetParameters();
        }
    }
}