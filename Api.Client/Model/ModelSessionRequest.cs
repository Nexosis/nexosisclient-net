using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nexosis.Api.Client.Model
{
    public class ModelSessionRequest : SessionRequest
    {
        /// <summary>
        /// The <see cref="PredictionDomain"/> of the model to be built
        /// </summary>
        public PredictionDomain PredictionDomain { get; set; }

        /// <summary>
        /// Extra parameters to alter how the model is built
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

    public class ClassificationModelSessionRequest : ModelSessionRequest
    {
        public ClassificationModelSessionRequest()
        {
            PredictionDomain = PredictionDomain.Classification;
        }

        /// <summary>
        /// For classification models, whether or not to balance classes during model building (default is true)
        /// </summary>
        [JsonIgnore]
        public bool? Balance
        {
            get => GetExtraParameter("balance");
            set => SetExtraParameter("balance", value);
        }
    }

    public class AnomaliesModelSessionRequest : ModelSessionRequest
    {
        public AnomaliesModelSessionRequest()
        {
            PredictionDomain = PredictionDomain.Anomalies;
        }

        /// <summary>
        /// For anomaly detection models, whether or not the source dataset contains anomalies (default is true)
        /// </summary>
        [JsonIgnore]
        public bool? ContainsAnomalies
        {
            get => GetExtraParameter("containsAnomalies");
            set => SetExtraParameter("containsAnomalies", value);
        }
    }

    public class RegressionModelSessionRequest : ModelSessionRequest
    {
        public RegressionModelSessionRequest()
        {
            PredictionDomain = PredictionDomain.Regression;
        }
    }
}