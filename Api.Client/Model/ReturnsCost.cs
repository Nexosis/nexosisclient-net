using System;
using System.Linq;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Nexosis.Api.Client.Model
{
    public class CurrencyAmount
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }

    public abstract class ReturnsCost
    {
        [JsonIgnore]
        public CurrencyAmount Cost { get; set; }

        [JsonIgnore]
        public CurrencyAmount Balance { get; set; }

        public void AssignCost(HttpResponseHeaders headers)
        {
            if (headers.Contains("nexosis-request-cost"))
                Cost = ParseValue(headers.GetValues("nexosis-request-cost").FirstOrDefault());
            if (headers.Contains("nexosis-account-balance"))
                Balance = ParseValue(headers.GetValues("nexosis-account-balance").FirstOrDefault());
        }

        private CurrencyAmount ParseValue(string value)
        {
            var parts = value.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 1)
            {
                return new CurrencyAmount { Amount = decimal.Parse(parts[0]), Currency = parts[1] };
            }
            else if (parts.Length == 1)
            {
                return new CurrencyAmount { Amount = decimal.Parse(parts[0]) };
            }
            return null;
        }
    }
}