using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
            await target.Sessions.AnalyzeImpact("data-set-name", "event-name", "target-column", DateTimeOffset.Parse("2017-12-12 10:11:12 -0:00"), DateTimeOffset.Parse("2017-12-22 22:23:24 -0:00"), ResultInterval.Day, "http://this.is.a.callback.url");

            Assert.Equal(HttpMethod.Post, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "sessions/impact?dataSetName=data-set-name&targetColumn=target-column&startDate=2017-12-12T10:11:12.0000000%2B00:00&endDate=2017-12-22T22:23:24.0000000%2B00:00&isEstimate=false&resultInterval=day&eventName=event-name&callbackUrl=http:%2F%2Fthis.is.a.callback.url"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task SetsDataOnRequestWhenGiven()
        {
            var data = new DataSetDetail { Data = new List<Dictionary<string,string>> { new Dictionary<string, string> { { "timestamp", DateTimeOffset.Now.ToString("O") }, { "delta", "0.23" }, { "bravo", "123.23" } } } };

            await target.Sessions.AnalyzeImpact(data, "event-name", "target-column", DateTimeOffset.Parse("2017-12-12 10:11:12 -0:00"), DateTimeOffset.Parse("2017-12-22 22:23:24 -0:00"), ResultInterval.Day, "http://this.is.a.callback.url");

            Assert.Equal(HttpMethod.Post, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "sessions/impact?targetColumn=target-column&startDate=2017-12-12T10:11:12.0000000%2B00:00&endDate=2017-12-22T22:23:24.0000000%2B00:00&isEstimate=false&resultInterval=day&eventName=event-name&callbackUrl=http:%2F%2Fthis.is.a.callback.url"), handler.Request.RequestUri);
            Assert.Equal(JsonConvert.SerializeObject(data), handler.RequestBody);
        }

        [Fact]
        public async Task SerializesCSVToBodyWhenGiven()
        {
            string fileContent = "timestamp,alpha,beta\r\n2017-01-01,10,14\r\n2017-01-02,11,13\r\n2017-01-03,12,12";
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent)))
            {
                using (var input = new StreamReader(stream, Encoding.UTF8))
                {
                    await target.Sessions.AnalyzeImpact(input, "event-name", "beta", DateTimeOffset.Parse("2017-12-12 10:11:12 -0:00"), DateTimeOffset.Parse("2017-12-22 22:23:24 -0:00"), ResultInterval.Day, "http://this.is.a.callback.url"); 
                }

                Assert.Equal(HttpMethod.Post, handler.Request.Method);
                Assert.Equal(new Uri(baseUri, "sessions/impact?targetColumn=beta&startDate=2017-12-12T10:11:12.0000000%2B00:00&endDate=2017-12-22T22:23:24.0000000%2B00:00&isEstimate=false&resultInterval=day&eventName=event-name&callbackUrl=http:%2F%2Fthis.is.a.callback.url"),
                    handler.Request.RequestUri);
                Assert.Equal(fileContent, handler.RequestBody);
            }
        }

        [Fact]
        public async Task ReqiresNotNullDataSet()
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await target.Sessions.AnalyzeImpact((DataSetDetail) null, "event", "", DateTimeOffset.MinValue, DateTimeOffset.MaxValue, ResultInterval.Day));

            Assert.Equal("data", exception.ParamName);
        }
        [Fact]
        public async Task ReqiresNotNullOrEmptyDataSetName()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.Sessions.AnalyzeImpact((string) null, "event",  "", DateTimeOffset.MinValue, DateTimeOffset.MaxValue, ResultInterval.Day));

            Assert.Equal("dataSetName", exception.ParamName);
        }
        [Fact]
        public async Task ReqiresNotNullStreamReader()
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await target.Sessions.AnalyzeImpact((StreamReader) null, "event", "", DateTimeOffset.MinValue, DateTimeOffset.MaxValue, ResultInterval.Day));

            Assert.Equal("input", exception.ParamName);
        }
        [Fact]
        public async Task ReqiresNotNullOrEmptyTargetColumn()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.Sessions.AnalyzeImpact("dataSet", "event", "", DateTimeOffset.MinValue, DateTimeOffset.MaxValue, ResultInterval.Day));

            Assert.Equal("targetColumn", exception.ParamName);
        }
        [Fact]
        public async Task ReqiresNotNullOrEmptyEventName()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.Sessions.AnalyzeImpact("dataSet", "", "targetCol", DateTimeOffset.MinValue, DateTimeOffset.MaxValue, ResultInterval.Day));

            Assert.Equal("eventName", exception.ParamName);
        }
    }
}
