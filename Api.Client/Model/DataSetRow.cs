using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nexosis.Api.Client.Model
{
    public class DataSetRow 
    {
        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Timestamp { get; set; }
    
        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, double> Values { get; set; }
    }

}