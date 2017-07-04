using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nexosis.Api.Client.Model
{
    public class SessionResponse : ReturnsCost
    {
        /// <summary>The unique identifier of this session.</summary>
        public Guid SessionId { get; set; }
    
        [JsonConverter(typeof(StringEnumConverter))]
        public SessionType Type { get; set; }
    
        /// <summary>The current <see cref="Model.Status">status</see> of the session.</summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        /// <summary>The values of session status over time.</summary>
        public List<StatusChange> StatusHistory { get; set; } = new List<StatusChange>();
    
        public Dictionary<string, string> ExtraParameters { get; set; }
    
        /// <summary>The name of the saved data set that has the data to forecast on.</summary>
        public string DataSetName { get; set; }
    
        /// <summary>The name of the column for which you want predictions.</summary>
        public string TargetColumn { get; set; }
    
        public string EventName { get; set; }

        /// <summary>The date and time the request for the session was initiated.</summary>
        public DateTimeOffset RequestedDate { get; set; }

        /// <summary>The ending date of the session results.</summary>
        public DateTimeOffset StartDate { get; set; }
    
        /// <summary>The ending date of the session results.</summary>
        public DateTimeOffset EndDate { get; set; }

        /// <summary>Metadata about each column in the dataset</summary>
        /// <remarks>Initialized with a case-insensitive key comparer as the API ignores case on column names.</remarks>
        public Dictionary<string, ColumnMetadata> Columns { get; set; } = new Dictionary<string, ColumnMetadata>(StringComparer.OrdinalIgnoreCase);
        
        public List<Link> Links { get; set; }
    }
}