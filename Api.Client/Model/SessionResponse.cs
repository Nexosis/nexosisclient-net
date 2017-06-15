using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nexosis.Api.Client.Model
{
    public class SessionResponse : ReturnsCost
    {
        public Guid SessionId { get; set; }
    
        [JsonConverter(typeof(StringEnumConverter))]
        public SessionType Type { get; set; }
    
        [JsonConverter(typeof(StringEnumConverter))]
        public SessionStatus Status { get; set; }

        public List<StatusChange> StatusHistory { get; set; } = new List<StatusChange>();
    
        public Dictionary<string, string> ExtraParameters { get; set; }
    
        public string DataSetName { get; set; }
    
        public string TargetColumn { get; set; }
    
        public string EventName { get; set; }

        public DateTimeOffset StartDate { get; set; }
    
        public DateTimeOffset EndDate { get; set; }

        public List<Link> Links { get; set; }
    }

}