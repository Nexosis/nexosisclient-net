using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class DataSourceStatsResult
    {
        public string DataSetName { get; set; }
        public Dictionary<string, Dictionary<string, double>> Columns { get; set; }
    }
}