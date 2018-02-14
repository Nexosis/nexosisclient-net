using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.DataSetTests
{
    public class StatsTests : NexosisClient_TestsBase
    {
        public StatsTests() : base(new DataSourceStatsResult()
        {
            DataSetName = "test",
            Columns = new List<Dictionary<string, Dictionary<string, double>>>()
            {
                new Dictionary<string, Dictionary<string, double>>()
                {
                    ["column1"] = new Dictionary<string, double>() {["count"] = 1}
                }
            }
        }) { }

        [Fact]
        public async Task GetStatsByName()
        {
            await target.DataSets.Stats("test");

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"data/test/stats"), handler.Request.RequestUri);
        }
    }
}