using System;
using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class ModelPredictionResult : ModelSummary
    {
        public List<Dictionary<string, string>> Data { get; set; } = new List<Dictionary<string, string>>();

        public List<StatusMessage> Messages { get; set; } = new List<StatusMessage>();
    }
}
