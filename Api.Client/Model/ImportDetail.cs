using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Nexosis.Api.Client.Model
{
    public class ImportDetail : ReturnsQuotas
    {
        /// <summary>
        /// The unique identifier of the Import
        /// </summary>
        public Guid ImportId { get; set; }

        /// <summary>
        /// The <see cref="ImportType">type</see> of import 
        /// </summary>
        public ImportType Type { get; set; }

        /// <summary>
        /// The current <see cref="Status">status</see> of the import 
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// The DataSet into which the data was imported
        /// </summary>
        public string DataSetName { get; set; }

        /// <summary>
        /// Additional data used as part of configuring the import
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// The date and time that the import was initially requested
        /// </summary>
        public DateTimeOffset RequestedDate { get; set; }

        /// <summary>
        /// The history of status changes on the import
        /// </summary>
        public List<StatusChange> StatusHistory { get; set; } = new List<StatusChange>();

        /// <summary>
        /// Messages to the user about the import
        /// </summary>
        public List<StatusMessage> Messages { get; set; } = new List<StatusMessage>();

        /// <summary>
        /// Describes the columns that are in the data to be imported 
        /// </summary>
        public Dictionary<string, ColumnMetadata> Columns { get; set; } =
            new Dictionary<string, ColumnMetadata>(StringComparer.OrdinalIgnoreCase);
    }
}