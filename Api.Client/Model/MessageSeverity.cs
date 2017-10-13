using System.Runtime.Serialization;

namespace Nexosis.Api.Client.Model
{
    public enum MessageSeverity
    {
        [EnumMember(Value = "debug")]
        Debug = -1,

        [EnumMember(Value = "informational")]
        Informational = 0,

        [EnumMember(Value = "warning")]
        Warning = 1,

        [EnumMember(Value = "error")]
        Error = 2
    }
}