using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nexosis.Api.Client;
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
            var request = Sessions.Forecast("data-set-name", DateTimeOffset.Parse("2017-12-12 10:11:12 -0:00"),
                DateTimeOffset.Parse("2017-12-22 22:23:24 -0:00"), ResultInterval.Day,
                "target-column");

            request.CallbackUrl = "http://this.is.a.callback.url";
            
            await target.Sessions.CreateForecast(request);

            Assert.Equal(HttpMethod.Post, handler.Request.Method);

            var actualBody = JsonConvert.DeserializeObject<ForecastSessionRequest>(handler.RequestBody);
            
            Assert.Equal("data-set-name", actualBody.DataSourceName);
        }
        [Fact]
        public async Task SetsTargetColumnWhenGiven()
        {
            var request = Sessions.Forecast("data-set-name", DateTimeOffset.Parse("2017-12-12 10:11:12 -0:00"),
                DateTimeOffset.Parse("2017-12-22 22:23:24 -0:00"), ResultInterval.Day,
                "target-column");

            request.CallbackUrl = "http://this.is.a.callback.url";

            await target.Sessions.CreateForecast(request);

            Assert.Equal(HttpMethod.Post, handler.Request.Method);

            var sessionDetail = JsonConvert.DeserializeObject<ForecastSessionRequest>(handler.RequestBody);
           

            Assert.Equal("target-column", sessionDetail.TargetColumn);
        }
        
        
        [Fact]
        public async Task ReqiresNotNullOrEmptyDataSetName()
        {
            var request = Sessions.Forecast(null, DateTimeOffset.MinValue, DateTimeOffset.MaxValue, ResultInterval.Day, "target_column");
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.Sessions.CreateForecast(request));

            Assert.Equal("dataSourceName", exception.ParamName);
        }
        
    }
}
