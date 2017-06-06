using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nexosis.Api.Client.Model
{
    public class SessionRequest : ReturnsCost
    {
        [JsonProperty("sessionId", NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid? SessionId { get; set; }
    
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public SessionStatus Status { get; set; }
    
        [JsonProperty("extraParameters", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> ExtraParameters { get; set; }
    
        [JsonProperty("dataSetName", NullValueHandling = NullValueHandling.Ignore)]
        public string DataSetName { get; set; }
    
        [JsonProperty("targetColumn", NullValueHandling = NullValueHandling.Ignore)]
        public string TargetColumn { get; set; }
    
        [JsonProperty("startDate", NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTime StartDate { get; set; }
    
        [JsonProperty("endDate", NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTime EndDate { get; set; }
    }

}