using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class SessionResult : SessionResponse
    {
        /// <summary>The data</summary>
        public List<Dictionary<string, string>> Data { get; set; } = new List<Dictionary<string, string>>();

        /// <summary>
        /// Overall metrics about the results of the session, including impact, accuracy scores, etc.
        /// </summary>
        public Dictionary<string, double> Metrics { get; set; }

        
    }
}
