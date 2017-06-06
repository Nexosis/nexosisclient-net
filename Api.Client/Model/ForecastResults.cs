using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nexosis.Api.Client.Model
{
    public class ForecastResults 
    {
        /// <summary>Continuous results from all forecast sessions executed on the dataset</summary>
        [JsonProperty("data", Required = Required.Always)]
        public List<DataSetRow> Data { get; set; } = new List<DataSetRow>();
    }
    
}