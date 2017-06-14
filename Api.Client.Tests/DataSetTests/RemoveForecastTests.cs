using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.Client.Tests.DataSetTests
{
    public class RemoveForecastTests : NexosisClient_TestsBase
    {
        public RemoveForecastTests() : base(new {})
        {
        }

        [Fact]
        public async Task ThrowsExceptionWithoutDataSetName()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.DataSets.RemoveForecast(null));

            Assert.Equal("dataSetName", exception.ParamName);
        }

        [Fact]
        public async Task PopulatesParametersForRemoval()
        {
            await target.DataSets.RemoveForecast("alpha", DateTimeOffset.Parse("2017-01-01 00:00:00 -0:00"), DateTimeOffset.Parse("2017-05-01 00:00:00 -0:00"));

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "data/alpha?startDate=2017-01-01T00:00:00.0000000%2B00:00&endDate=2017-05-01T00:00:00.0000000%2B00:00"), handler.Request.RequestUri);
        }

    }
}
