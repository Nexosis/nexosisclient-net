using System;
using System.Linq;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Nexosis.Api.Client.Model
{

    public class Quota
    {

        public static Quota Parse(string prefix, HttpResponseHeaders headers)
        {
            var value = new Quota();
            var allottedKey = $"{prefix}-allotted";
            var currentKey = $"{prefix}-current";
           
            if (headers.Contains(allottedKey) && Int32.TryParse(headers.GetValues(allottedKey).FirstOrDefault(), out var allotted))
            {
                value.Allotted = allotted;
            }

            if (headers.Contains(currentKey) && Int32.TryParse(headers.GetValues(currentKey).FirstOrDefault(), out var current))
            {
                value.Current = current;
            }
            return value;
        }
        
        public int Allotted { get; set; }
        public int Current { get; set; }
    }
    
    public abstract class ReturnsQuotas
    {
        [JsonIgnore]
        public Quota DataSetCount { get; set; }

        [JsonIgnore]
        public Quota PredictionCount { get; set; }

        [JsonIgnore]
        public Quota SessionCount { get; set; }

        public void AssignQuotas(HttpResponseHeaders headers)
        {            
            DataSetCount = Quota.Parse("nexosis-account-datasetcount", headers);
            PredictionCount = Quota.Parse("nexosis-account-predictioncount", headers);
            SessionCount = Quota.Parse("nexosis-account-sessioncount", headers);
        }

    }
}