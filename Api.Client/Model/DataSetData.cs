using System;
using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class DataSetData : Paged<Dictionary<string, string>>
    {
        public List<Dictionary<string, string>> Data { get; set; }

        /// <summary>Metadata about each column in the dataset</summary>
        /// <remarks>This is initialized as a case-insensitive dictionary. The API ignores case for column names.</remarks>
        public Dictionary<string, ColumnMetadata> Columns { get; set; } = new Dictionary<string, ColumnMetadata>(StringComparer.OrdinalIgnoreCase);
        
    }
}