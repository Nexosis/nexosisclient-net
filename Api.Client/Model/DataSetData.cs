using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nexosis.Api.Client.Model
{
    public class DataSetData
    {
        /// <summary>The data</summary>
        [JsonProperty("data", Required = Required.Always)]
        public List<DataSetRow> Data { get; set; } = new List<DataSetRow>();
    }

}