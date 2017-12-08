using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nexosis.Api.Client.Utility;

namespace Nexosis.Api.Client.Model
{
    public class ModelRemoveCriteria
    {

        /// <summary>
        /// The Id of the model to be removed
        /// </summary>
        public Guid? ModelId { get; set; }
        
        /// <summary>
        /// All models built from this DataSource should be removed
        /// </summary>
        public string DataSourceName { get; set; }

        /// <summary>
        /// Models created after this date should be removed
        /// </summary>
        public DateTimeOffset? CreatedAfterDate { get; set; }

        /// <summary>
        /// Models created before this date should be removed
        /// </summary>
        public DateTimeOffset? CreatedBeforeDate { get; set; }

        internal IEnumerable<KeyValuePair<string, string>> ToParameters()
        {
            var builder = new ParameterBuilder();
            builder.Add("modelId", ModelId);
            builder.Add("dataSourceName", DataSourceName);
            builder.Add("createdAfterDate", CreatedAfterDate);
            builder.Add("createdBeforeDate", CreatedBeforeDate);

            return builder.GetParameters();
        }

    }
}