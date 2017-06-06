using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nexosis.Api.Client.Model
{
    public class Sessions
    {
        // TODO: we have SessionResults and Sessions.Results which is confusing
        [JsonProperty("results", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public List<SessionRequest> Results { get; set; }
    }
}