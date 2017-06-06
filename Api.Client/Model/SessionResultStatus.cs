using System;

namespace Nexosis.Api.Client.Model
{
    public enum SessionStatus
    {
        [System.Runtime.Serialization.EnumMember(Value = "requested")]
        Requested = 0,
    
        [System.Runtime.Serialization.EnumMember(Value = "started")]
        Started = 1,
    
        [System.Runtime.Serialization.EnumMember(Value = "completed")]
        Completed = 2,
    
        [System.Runtime.Serialization.EnumMember(Value = "cancelled")]
        Cancelled = 3,
    
        [System.Runtime.Serialization.EnumMember(Value = "failed")]
        Failed = 4,
    
        [System.Runtime.Serialization.EnumMember(Value = "estimated")]
        Estimated = 5,
    
    }

    public class SessionResultStatus 
    {
        public Guid SessionId { get; set; }
        public SessionStatus Status { get; set; }
    }

}