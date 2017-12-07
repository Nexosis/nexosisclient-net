using System;
using System.Collections.Generic;
using Nexosis.Api.Client.Utility;

namespace Nexosis.Api.Client.Model
{
    /// <summary>
    /// Search criteria for determining the data to remove from a dataset
    /// </summary>
    public class DataSetRemoveCriteria
    {

        public DataSetRemoveCriteria(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// The name of the dataset
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The start date (for Time Series DataSets) after which data should be removed
        /// </summary>
        public DateTimeOffset? StartDate { get; set; }

        /// <summary>
        /// The end date (for Time Series DataSets) before whcih data should be removed
        /// </summary>
        public DateTimeOffset? EndDate { get; set; }

        /// <summary>
        /// Options for deleting additional data related to the DataSet
        /// </summary>
        public DataSetDeleteOptions? Options { get; set; }

        internal IEnumerable<KeyValuePair<string, string>> ToParameters()
        {
            var builder = new ParameterBuilder();
            builder.Add("startDate", StartDate);
            builder.Add("endDate", EndDate);
            
            if ((Options & DataSetDeleteOptions.CascadeForecast) != 0) builder.Add("cascade", "forecast");
            if ((Options & DataSetDeleteOptions.CascadeSessions) != 0) builder.Add("cascade", "session");
            if ((Options & DataSetDeleteOptions.CascadeViews) != 0) builder.Add("cascade", "view");

            return builder.GetParameters();
        }
    }
}