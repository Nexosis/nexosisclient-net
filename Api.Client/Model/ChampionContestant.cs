using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class ChampionContestant : Resource
    {
        /// <summary>
        /// The Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Details of the algorithm used
        /// </summary>
        public Algorithm Algorithm { get; set; }

        /// <summary>
        /// Information about the datasource preparations that were performed
        /// </summary>
        public string[] DataSourceProperties { get; set; }

        /// <summary>
        /// Scoring metrics generated from the session
        /// </summary>
        public Dictionary<string, double> Metrics { get; set; }
    }
}