using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nexosis.Api.Client.Model
{
    public class StatusChange
    {
        public DateTimeOffset Date { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }
    }
}
