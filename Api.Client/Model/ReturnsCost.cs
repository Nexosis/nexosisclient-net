using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Nexosis.Api.Client.Model
{
    public abstract class ReturnsCost
    {
        [JsonIgnore]
        public string Cost { get; set; }

        [JsonIgnore]
        public string Balance { get; set; }

        public void AssignCost(HttpResponseHeaders headers)
        {
            if (headers.Contains("nexosis-request-cost"))
                Cost = headers.GetValues("nexosis-request-cost").FirstOrDefault() ?? "NA";
            if (headers.Contains("nexosis-account-balance"))
                Balance = headers.GetValues("nexosis-account-balance").FirstOrDefault() ?? "NA";
        }
    }
}