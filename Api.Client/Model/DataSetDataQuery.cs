using System;
using System.Collections.Generic;
using System.Linq;
using Nexosis.Api.Client.Utility;

namespace Nexosis.Api.Client.Model
{
    /// <summary>
    /// Query critera for retrieving data from a dataset.  Use the DataSet.Data method to create me
    /// </summary>
    public class DataSetDataQuery
    {
        public DataSetDataQuery(string name = null)
        {
            this.Name = name;
        }

        /// <summary>
        /// The name of the DataSet whose data should be retrieved
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Data after this date should be returned (Time Series DataSets only) 
        /// </summary>
        public DateTimeOffset? StartDate { get; set; }

        /// <summary>
        /// Data before this date should be returned (Time Series DataSets only) 
        /// </summary>
        public DateTimeOffset? EndDate { get; set; }

        /// <summary>
        /// Columns from the DataSet to be returned
        /// </summary>
        public IEnumerable<string> IncludedColumns { get; set; }

        /// <summary>
        /// Paging information for the response
        /// </summary>
        public PagingInfo Page { get; set; }

        internal List<KeyValuePair<string, string>> ToParameters()
        {
            var builder = new ParameterBuilder();
            builder.Add("startDate", StartDate);
            builder.Add("endDate", EndDate);
            if (IncludedColumns != null && IncludedColumns.Any())
            {
                foreach (var col in IncludedColumns)
                {
                    builder.Add("include", col);
                }
            }
            builder.Add(Page);

            return builder.GetParameters();
        }


    }
}