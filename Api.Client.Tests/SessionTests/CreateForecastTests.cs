using System;
using System.Net.Http;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.SessionTests
{
    public class CreateForecastTests : NexosisClient_TestsBase
    {
        public CreateForecastTests() : base(new { })
        {
        }

        [Fact]
        public async Task SetsDataSetNameWhenGiven()
        {
            await target.Sessions.CreateForecast("data-set-name", "target-column", DateTimeOffset.Parse("2017-12-12 10:11:12 -0:00"), DateTimeOffset.Parse("2017-12-22 22:23:24 -0:00"), ResultInterval.Day, "http://this.is.a.callback.url");

            Assert.Equal(HttpMethod.Post, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "sessions/forecast?dataSetName=data-set-name&targetColumn=target-column&startDate=2017-12-12T10:11:12.0000000%2B00:00&endDate=2017-12-22T22:23:24.0000000%2B00:00&isEstimate=false&resultInterval=day&callbackUrl=http:%2F%2Fthis.is.a.callback.url"), handler.Request.RequestUri);
        }
        [Fact]
        public async Task ReqiresNotNullDetail()
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await target.Sessions.CreateForecast((SessionDetail)null, "", DateTimeOffset.MinValue, DateTimeOffset.MaxValue, ResultInterval.Day));

            Assert.Equal("data", exception.ParamName);
        }
        [Fact]
        public async Task ReqiresNotNullOrEmptyDataSetName()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.Sessions.CreateForecast((string)null, "", DateTimeOffset.MinValue, DateTimeOffset.MaxValue, ResultInterval.Day));

            Assert.Equal("dataSetName", exception.ParamName);
        }
        [Fact]
        public async Task ReqiresNotNullOrEmptyTargetColumn()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.Sessions.CreateForecast("dataSet", "", DateTimeOffset.MinValue, DateTimeOffset.MaxValue, ResultInterval.Day));

            Assert.Equal("targetColumn", exception.ParamName);
        }
    }
}
