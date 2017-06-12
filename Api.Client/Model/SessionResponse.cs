using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nexosis.Api.Client.Model
{
    public class SessionResponse : ReturnsCost
    {
        [JsonProperty("sessionId")]
        public System.Guid SessionId { get; set; }
    
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public SessionType Type { get; set; }
    
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public SessionStatus Status { get; set; }
    
        [JsonProperty("extraParameters", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> ExtraParameters { get; set; }
    
        [JsonProperty("dataSetName", NullValueHandling = NullValueHandling.Ignore)]
        public string DataSetName { get; set; }
    
        [JsonProperty("targetColumn", NullValueHandling = NullValueHandling.Ignore)]
        public string TargetColumn { get; set; }
    
        [JsonProperty("eventName", NullValueHandling = NullValueHandling.Ignore)]
        public string EventName { get; set; }

        [JsonProperty("startDate", NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset StartDate { get; set; }
    
        [JsonProperty("endDate", NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset EndDate { get; set; }
    }

}