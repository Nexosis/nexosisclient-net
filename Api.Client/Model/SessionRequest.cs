using System;
using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public abstract class SessionRequest
    {
        /// <summary>
        /// The data source to use for the session
        /// </summary>
        public string DataSourceName { get; set; }

        /// <summary>
        /// The column within the Data Source to be used as the target
        /// </summary>
        public string TargetColumn { get; set; }

        /// <summary>Metadata about each column in the dataset, for purposes of the session</summary>
        /// <remarks>This is initialized as a case-insensitive dictionary. The API ignores case for column names.</remarks>
        public Dictionary<string, ColumnMetadata> Columns { get; set; } =
            new Dictionary<string, ColumnMetadata>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// A url to receive a callback when the status of the Session changes
        /// </summary>
        public string CallbackUrl { get; set; }

    }
}