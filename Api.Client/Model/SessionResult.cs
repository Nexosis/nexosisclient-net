using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class SessionResult : SessionResponse
    {
        /// <summary>The data</summary>
        public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();

        /// <summary>For impact sessions, an object containing overall metrics about the impact</summary>
        public Dictionary<string, double> Metrics { get; set; }
    }
}
