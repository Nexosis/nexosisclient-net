using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.Client.Tests.DataSetTests
{
    public class ListForecastModelTests : NexosisClient_TestsBase
    {
        public ListForecastModelTests() : base(new {})
        {
        }

        [Fact]
        public async Task ThrowsExceptionWhenMissingDataSetName()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.DataSets.ListForecastModels(""));

            Assert.Equal("dataSetName", exception.ParamName);
        }

        [Fact]
        public async Task PassesDataSetAndTargetColumn()
        {
            await target.DataSets.ListForecastModels("tango");

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "data/tango/forecast/model"), handler.Request.RequestUri);
        }

    }
}
