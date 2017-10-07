using System.Runtime.Serialization;

namespace Nexosis.Api.Client.Model
{
    public enum SessionType
    {
        [EnumMember(Value = "import")]
        Import = 0,

        [EnumMember(Value = "forecast")]
        Forecast = 1,

        [EnumMember(Value = "impact")]
        Impact = 2,

        [EnumMember(Value = "model")]
        Model = 3,
    }
}