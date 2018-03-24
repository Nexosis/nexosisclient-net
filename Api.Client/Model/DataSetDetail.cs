using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nexosis.Api.Client.Model
{
    public class DataSetDetail
    {
        public DataSetDetail() { }

        /// <summary>
        /// Create a DataSetDetail using a collection of .NET object
        /// </summary>
        /// <param name="obj">The objects must be simple objects that can be deserialized into a simple string dictionary</param>
        public DataSetDetail(List<dynamic> obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            Data = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);
        }

        /// <summary>The data</summary>
        /// <remarks>The dictionaries added to the list should treat the keys case insensitive. The API ignores case on column names.</remarks>
        public List<Dictionary<string, string>> Data { get; set; } = new List<Dictionary<string, string>>();

        /// <summary>Metadata about each column in the dataset</summary>
        /// <remarks>This is initialized as a case-insensitive dictionary. The API ignores case for column names.</remarks>
        public Dictionary<string, ColumnMetadata> Columns { get; set; } = new Dictionary<string, ColumnMetadata>(StringComparer.OrdinalIgnoreCase);
    }
}