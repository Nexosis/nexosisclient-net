using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.Client.Tests.DataSetTests
{
    public class GetForecastModelTests : NexosisClient_TestsBase
    {
        public GetForecastModelTests() : base(new {})
        {
        }

        [Fact]
        public async Task ThrowsExceptionWhenMissingDataSetName()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.DataSets.GetForecastModel("", null));

            Assert.Equal("dataSetName", exception.ParamName);
        }

        [Fact]
        public async Task ThrowsExceptionWhenMissingTargetColumn()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.DataSets.GetForecastModel("uniform", null));
            
            Assert.Equal("targetColumn", exception.ParamName);
        }

        [Fact]
        public async Task PassesDataSetAndTargetColumn()
        {
            await target.DataSets.GetForecastModel("uniform", "victor");

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "data/uniform/forecast/model/victor"), handler.Request.RequestUri);
        }
    }
}
