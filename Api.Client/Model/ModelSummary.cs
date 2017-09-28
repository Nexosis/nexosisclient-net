using System;
using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class ModelSummary
    {
        /// <summary>
        /// Unique identifier of this model.
        /// </summary>
        public Guid ModelId { get; set; }

        /// <summary>
        /// Type of prediction that this model is intended to make.
        /// </summary>
        public string PredictionDomain { get; set; }

        /// <summary>
        /// Name of the datasource used as the training data for this model.
        /// </summary>
        public string DataSourceName { get; set; }

        /// <summary>
        /// The date and time that the model was first created.
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// Details about the algorithm used by this model.
        /// </summary>
        public Algorithm Algorithm { get; set; }

        /// <summary>Metadata about each column used in the model</summary>
        /// <remarks>This is initialized as a case-insensitive dictionary. The API ignores case for column names.</remarks>
        public Dictionary<string, ColumnMetadata> Columns { get; set; } = new Dictionary<string, ColumnMetadata>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Information about the model.
        /// </summary>
        public Dictionary<string, double> Metrics { get; set; } = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
    }
}
