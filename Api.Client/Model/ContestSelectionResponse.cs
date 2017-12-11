using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class ContestSelectionResponse : SessionResponse
    {
        public List<MetricSet> MetricSets { get; set; }

        public class MetricSet
        {
            /// <summary>
            /// Information about the data source preparations that were performed
            /// </summary>
            public string[] DataSetProperties { get; set; }

            /// <summary>
            /// Selection metrics used when determining which algorithms to run
            /// </summary>
            public Dictionary<string, double> Metrics { get; set; }
        }
    }
}