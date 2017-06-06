using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nexosis.Api.Client.Model
{
    // TODO: we have SessionResults and Sessions.Results which is confusing
    public class SessionResult : ReturnsCost
    {
        /// <summary>The session request that caused these results</summary>
        [JsonProperty("session", Required = Required.Always, NullValueHandling = NullValueHandling.Ignore)]
        public SessionRequest Session { get; set; }
    
        /// <summary>For impact sessions, an object containing overall metrics about the impact</summary>
        [JsonProperty("metrics", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, double> Metrics { get; set; }
    
        /// <summary>Continuous results from all forecast sessions executed on the dataset</summary>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<DataSetRow> Data { get; set; } = new List<DataSetRow>();
    }
}