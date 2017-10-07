using System;
using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class ModelSessionDetail
    { 
        /// <summary>Name of the datasource on which the session will operate</summary>
        public string DataSourceName { get; set; }

        /// <summary>Metadata about each column in the dataset, for purposes of the session</summary>
        /// <remarks>This is initialized as a case-insensitive dictionary. The API ignores case for column names.</remarks>
        public Dictionary<string, ColumnMetadata> Columns { get; set; } = new Dictionary<string, ColumnMetadata>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Type of prediction the build model is intended to make.
        /// </summary>
        public PredictionDomain PredictionDomain { get; set; }

        /// <summary>
        /// The Webhook url that will receive updates when the Session status changes<br/>
        /// If you provide a callback url, your response will contain a header named Nexosis-Webhook-Token.  You will receive this 
        /// same header in the request message to your Webhook, which you can use to validate that the message came from Nexosis.
        /// </summary>
        public string CallbackUrl { get; set; }

        /// <summary>
        /// If specified, the session will not be processed.  The returned `Nexosis-Request-Cost` header will be populated with the estimated cost that the request would have incurred.
        /// </summary>
        public bool IsEstimate { get; set; }
    }
}
