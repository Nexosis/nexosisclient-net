using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nexosis.Api.Client.Model
{
    public class SessionResult : SessionResponse
    {
        /// <summary>Continuous results from all forecast sessions executed on the dataset</summary>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<DataSetRow> Data { get; set; } = new List<DataSetRow>();

        /// <summary>For impact sessions, an object containing overall metrics about the impact</summary>
        [JsonProperty("metrics", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, double> Metrics { get; set; }
    }
}