using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class ContestResponse : SessionResponse
    {
        /// <summary>
        /// The metric used to determine the champion algorithm that should be used for this session
        /// </summary>
        public string ChampionMetric { get; set; }

        /// <summary>
        /// The contestant selected as the champion
        /// </summary>
        public ChampionContestant Champion { get; set; }

        /// <summary>
        /// Other contestants that were considered in the selection process
        /// </summary>
        public List<ChampionContestant> Contestants { get; set; }
    }
}