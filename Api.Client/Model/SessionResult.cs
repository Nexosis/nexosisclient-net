using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class SessionResult : SessionResponse
    {
        /// <summary>The data</summary>
        public List<Dictionary<string, string>> Data { get; set; } = new List<Dictionary<string, string>>();

        /// <summary>For impact sessions, an object containing overall metrics about the impact</summary>
        public Dictionary<string, double> Metrics { get; set; }
    }
}
