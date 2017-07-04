using System.Runtime.Serialization;

namespace Nexosis.Api.Client.Model
{
    public enum Status
    {
        [EnumMember(Value = "requested")]
        Requested = 0,
    
        [EnumMember(Value = "started")]
        Started = 1,
    
        [EnumMember(Value = "completed")]
        Completed = 2,
    
        [EnumMember(Value = "cancelled")]
        Cancelled = 3,
    
        [EnumMember(Value = "failed")]
        Failed = 4,
    
        [EnumMember(Value = "estimated")]
        Estimated = 5,
    
    }
}