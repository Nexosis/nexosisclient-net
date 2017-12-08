using System;
using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public abstract class ImportRequest
    {
        /// <summary>
        /// The name of the DataSet into which the data should be imported
        /// </summary>
        public string DataSetName { get; set; }

        /// <summary>
        /// Describes the columns that are in the data to be imported 
        /// </summary>
        public Dictionary<string, ColumnMetadata> Columns { get; set; } =
            new Dictionary<string, ColumnMetadata>(StringComparer.OrdinalIgnoreCase);

    }
}