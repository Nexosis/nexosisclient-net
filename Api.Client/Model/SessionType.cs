namespace Nexosis.Api.Client.Model
{
    public enum SessionType
    {
        [System.Runtime.Serialization.EnumMember(Value = "import")]
        Import = 0,

        [System.Runtime.Serialization.EnumMember(Value = "forecast")]
        Forecast = 1,

        [System.Runtime.Serialization.EnumMember(Value = "impact")]
        Impact = 2,
    }
}