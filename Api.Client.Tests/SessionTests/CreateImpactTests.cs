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
    public class CreateImpactTests : NexosisClient_TestsBase
    {
        public CreateImpactTests() : base(new { })
        {
        }

        [Fact]
        public async Task SetsDataSetNameWhenGiven()
        {
            
            await target.Sessions.AnalyzeImpact(Sessions.Impact("data-set-name", DateTimeOffset.Parse("2017-12-12 10:11:12 -0:00"), DateTimeOffset.Parse("2017-12-22 22:23:24 -0:00"), ResultInterval.Day, "event-name", "target-column"));

            Assert.Equal(HttpMethod.Post, handler.Request.Method);

            var actualBody = JsonConvert.DeserializeObject<ImpactSessionRequest>(handler.RequestBody);
            
            Assert.Equal("data-set-name", actualBody.DataSourceName);
        }
        
        [Fact]
        public async Task SetsTargetColumnWhenGiven()
        {
            await target.Sessions.AnalyzeImpact(Sessions.Impact("data-set-name", DateTimeOffset.Parse("2017-12-12 10:11:12 -0:00"), DateTimeOffset.Parse("2017-12-22 22:23:24 -0:00"), ResultInterval.Day, "event-name", "target-column"));

            Assert.Equal(HttpMethod.Post, handler.Request.Method);

            var request = JsonConvert.DeserializeObject<ImpactSessionRequest>(handler.RequestBody);

            Assert.Equal("target-column", request.TargetColumn);
        }

        [Fact]
        public async Task SetsCallbackUrlWhenGiven()
        {
            var request = Sessions.Impact("data-set-name",
                DateTimeOffset.Parse("2017-12-12 10:11:12 -0:00"), DateTimeOffset.Parse("2017-12-22 22:23:24 -0:00"),
                ResultInterval.Day, "event-name", "target-column");

            request.CallbackUrl = "http://callback.url";
            
            await target.Sessions.AnalyzeImpact(request);

            Assert.Equal(HttpMethod.Post, handler.Request.Method);

            var sessionRequest = JsonConvert.DeserializeObject<ImpactSessionRequest>(handler.RequestBody);

            Assert.Equal("http://callback.url", sessionRequest.CallbackUrl);
        }
        
        [Fact]
        public async Task ReqiresNotNullOrEmptyDataSetName()
        {
            var request = Sessions.Impact(null, DateTimeOffset.MinValue, DateTimeOffset.MaxValue, ResultInterval.Day,
                "event", "target");
            
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.Sessions.AnalyzeImpact(request));

            Assert.Equal("dataSourceName", exception.ParamName);
        }
        
        
        
        [Fact]
        public async Task ReqiresNotNullOrEmptyEventName()
        {
            var request = Sessions.Impact("dataSource", DateTimeOffset.MinValue, DateTimeOffset.MaxValue,
                ResultInterval.Day, null, "target-column");
            
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.Sessions.AnalyzeImpact(request));

            Assert.Equal("eventName", exception.ParamName);
        }
    }
}
