using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nexosis.Api.Client.Model
{
    public class ModelPredictionRequest
    {
        public ModelPredictionRequest()
        {
            ExtraParameters = new Dictionary<string, string>();
        }
        public ModelPredictionRequest(Guid modelId, List<Dictionary<string, string>> data) : this()
        {
            ModelId = modelId;
            Data = data;
        }

        /// <summary>
        /// The Model that we're using for predictions
        /// </summary>
        public Guid ModelId { get; set; }

        /// <summary>
        /// The Feature data to use when predicting
        /// </summary>
        public List<Dictionary<string, string>> Data { get; set; }

        /// <summary>
        /// Extra parameters to alter the results returned from predicting
        /// </summary>
        public Dictionary<string, string> ExtraParameters { get; set; }

        protected bool? GetExtraParameter(string name)
        {
            return ExtraParameters != null
                && ExtraParameters.TryGetValue(name, out string stringValue)
                && bool.TryParse(stringValue, out bool boolValue)
                ? (bool?)boolValue
                : null;
        }
        protected void SetExtraParameter(string name, bool? value)
        {
            if (ExtraParameters == null)
                ExtraParameters = new Dictionary<string, string>();

            if (value.HasValue)
                ExtraParameters[name] = value.ToString();
        }
    }

    public class ClassificationModelPredictionRequest : ModelPredictionRequest
    {
        /// <summary>
        /// For classification models, whether or not to include class scores for each possible class (default is false)
        /// </summary>
        [JsonIgnore]
        public bool? IncludeClassScores
        {
            get => GetExtraParameter("includeClassScores");
            set => SetExtraParameter("includeClassScores", value);
        }
    }
}