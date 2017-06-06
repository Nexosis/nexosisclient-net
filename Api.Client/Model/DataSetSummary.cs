using Newtonsoft.Json;

namespace Nexosis.Api.Client.Model
{
    public class DataSetSummary : ReturnsCost
    {
        [JsonProperty("dataSetName", NullValueHandling = NullValueHandling.Ignore)]
        public string DataSetName { get; set; }
    }

}