using System;
using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class ModelPredictionRequest
    {
        public ModelPredictionRequest(Guid modelId, List<Dictionary<string, string>> data)
        {
            this.ModelId = modelId;
            this.Data = data;
        }
        
        /// <summary>
        /// The Model that we're using for predictions
        /// </summary>
        public Guid ModelId { get; set; }

        /// <summary>
        /// The Feature data to use when predicting
        /// </summary>
        public List<Dictionary<string, string>> Data { get; set; }
    }
}