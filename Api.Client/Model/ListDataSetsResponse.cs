using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nexosis.Api.Client.Model
{
    /// <summary>A list of all <see cref="DataSet">DataSet</see>s that have been saved.</summary>
    public class ListDataSetsResponse 
    {
        /// <summary>Summaries of the data sets that have been uploaded</summary>
        [JsonProperty("dataSets", NullValueHandling = NullValueHandling.Ignore)]
        public List<DataSetSummary> DataSets { get; set; }
    }

}