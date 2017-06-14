using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class SessionResult : SessionResponse
    {
        /// <summary>The session request that caused these results</summary>
        public SessionResponse Session { get; set; }
    
        /// <summary>Continuous results from all forecast sessions executed on the dataset</summary>
        public List<DataSetRow> Data { get; set; } = new List<DataSetRow>();

        /// <summary>For impact sessions, an object containing overall metrics about the impact</summary>
        public Dictionary<string, double> Metrics { get; set; }
    }
}
