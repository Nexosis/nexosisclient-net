using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nexosis.Api.Client.Model
{
    public abstract class ReturnsCost
    {
        [JsonIgnore]
        public string Cost { get; set; }

        [JsonIgnore]
        public string Balance { get; set; }

        public void AssignCost(IDictionary<string, IEnumerable<string>> headers)
        {
            Cost = headers["nexosis-request-cost"].FirstOrDefault() ?? "NA";
            Balance = headers["nexosis-account-balance"].FirstOrDefault() ?? "NA";
        }
    }
}