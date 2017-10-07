using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nexosis.Api.Client.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PredictionDomain
    {
        [EnumMember(Value = "regression")]
        Regression = 0,
    }
}
