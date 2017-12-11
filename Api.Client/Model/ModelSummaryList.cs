using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class ModelSummaryList : Paged<ModelSummary>
    {
        /// <summary>
        /// The models
        /// </summary>
        public List<ModelSummary> Items { get; set; }
    }
}