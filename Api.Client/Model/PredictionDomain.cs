using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nexosis.Api.Client.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PredictionDomain : sbyte
    {
        [EnumMember(Value = "regression")]
        Regression = 0,
        [EnumMember(Value = "classification")]
        Classification = 1,
        [EnumMember(Value = "forecast")]
        Forecast = 2,
        [EnumMember(Value = "impact")]
        Impact = 3,
        [EnumMember(Value = "anomalies")]
        Anomalies = 4
    }
}
