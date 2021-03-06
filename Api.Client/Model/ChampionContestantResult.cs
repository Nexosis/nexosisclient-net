using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class ChampionContestantResult : ChampionContestant
    {
        /// <summary>
        /// The test data used when scoring the contestant
        /// </summary>
        public Dictionary<string, string>[] Data { get; set; }
    }
}