using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.DataSetTests
{
    public class StatsTests : NexosisClient_TestsBase
    {
        public StatsTests() : base(new DataSourceStatsResult()
        {
            DataSetName = "test",
            Columns = new Dictionary<string, JObject>()
            {
                ["column1"] = JObject.Parse("{ \"count\": 1 }")
            }
        })
        { }

        [Fact]
        public async Task GetStatsByName()
        {
            await target.DataSets.Stats("test");

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"data/test/stats"), handler.Request.RequestUri);
        }
    }
}