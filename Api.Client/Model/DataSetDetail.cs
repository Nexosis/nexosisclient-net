using System;
using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class DataSetDetail
    {
        /// <summary>The data</summary>
        /// <remarks>The dictionaries added to the list should treat the keys case insensitive. The API ignores case on column names.</remarks>
        public List<Dictionary<string, string>> Data { get; set; } = new List<Dictionary<string, string>>();

        /// <summary>Metadata about each column in the dataset</summary>
        /// <remarks>This is initialized as a case-insensitive dictionary. The API ignores case for column names.</remarks>
        public Dictionary<string, ColumnMetadata> Columns { get; set; } = new Dictionary<string, ColumnMetadata>(StringComparer.OrdinalIgnoreCase);

    }
}