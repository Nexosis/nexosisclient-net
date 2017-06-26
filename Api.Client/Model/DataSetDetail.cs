using System;
using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class DataSetDetail
    {
        /// <summary>The data</summary>
        public List<Dictionary<string, string>> Data { get; set; } = new List<Dictionary<string, string>>();

        /// <summary>Metadata about each column in the dataset</summary>
        public Dictionary<string, ColumnMetadata> Columns { get; set; } = new Dictionary<string, ColumnMetadata>(StringComparer.OrdinalIgnoreCase);
    }
}