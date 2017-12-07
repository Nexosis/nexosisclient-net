using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    /// <summary>
    /// Listing of DataSets
    /// </summary>
    public class DataSetSummaryList : Paged<DataSetSummary>
    {
        /// <summary>
        /// The DataSets
        /// </summary>
        public List<DataSetSummary> Items { get; set; }
    }
}