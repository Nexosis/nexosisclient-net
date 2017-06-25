using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nexosis.Api.Client.Model
{
    public class DataSet
    {
        /// <summary>The data</summary>
        public List<Dictionary<string, string>> Data { get; set; } = new List<Dictionary<string, string>>();

        /// <summary>Metadata about each column in the dataset </summary>
        public Dictionary<string, ColumnMetadata> Columns { get; set; } = new Dictionary<string, ColumnMetadata>();
    }
}