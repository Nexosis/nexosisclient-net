using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests
{
    public class CreateForecastTests : NexosisClient_TestsBase
    {
        public CreateForecastTests() : base(new { })
        {
        }

        [Fact]
        public async Task SetsDataSetNameWhenGiven()
        {
            await target.Sessions.CreateForecastSession("data-set-name", "target-column", DateTimeOffset.Parse("2017-12-12 10:11:12 -0:00"), DateTimeOffset.Parse("2017-12-22 22:23:24 -0:00"), "http://this.is.a.callback.url");

            Assert.Equal(HttpMethod.Post, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "sessions/forecast?dataSetName=data-set-name&targetColumn=target-column&startDate=2017-12-12T10:11:12.0000000%2B00:00&endDate=2017-12-22T22:23:24.0000000%2B00:00&isEstimate=false&callbackUrl=http:%2F%2Fthis.is.a.callback.url"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task SetsDataOnRequestWhenGiven()
        {
            var data = new List<DataSetRow> { new DataSetRow { Timestamp = DateTime.Today, Values = new Dictionary<string, double> { { "delta", 0.23 }, { "bravo", 123.23 } } } };

            await target.Sessions.CreateForecastSession(data, "target-column", DateTimeOffset.Parse("2017-12-12 10:11:12 -0:00"), DateTimeOffset.Parse("2017-12-22 22:23:24 -0:00"), "http://this.is.a.callback.url");

            Assert.Equal(HttpMethod.Post, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "sessions/forecast?targetColumn=target-column&startDate=2017-12-12T10:11:12.0000000%2B00:00&endDate=2017-12-22T22:23:24.0000000%2B00:00&isEstimate=false&callbackUrl=http:%2F%2Fthis.is.a.callback.url"), handler.Request.RequestUri);
            Assert.Equal(JsonConvert.SerializeObject(new { data }), handler.RequestBody);
        }

        [Fact]
        public async Task SerializesCSVToBodyWhenGiven()
        {
            var filename = Path.Combine(AppContext.BaseDirectory, $"test-forecast-{DateTime.UtcNow:yyyyMMddhhmmss}.csv");
            try
            {
                using (var file = File.AppendText(filename))
                {
                    file.WriteLine("timestamp,alpha,beta");
                    file.WriteLine("2017-01-01,10,14");
                    file.WriteLine("2017-01-02,11,13");
                    file.WriteLine("2017-01-03,12,12");
                    file.Flush();
                }
                using (var input = File.OpenText(filename))
                {
                    await target.Sessions.CreateForecastSession(input, "beta", DateTimeOffset.Parse("2017-12-12 10:11:12 -0:00"), DateTimeOffset.Parse("2017-12-22 22:23:24 -0:00"), "http://this.is.a.callback.url"); 
                }

                Assert.Equal(HttpMethod.Post, handler.Request.Method);
                Assert.Equal(new Uri(baseUri, "sessions/forecast?targetColumn=beta&startDate=2017-12-12T10:11:12.0000000%2B00:00&endDate=2017-12-22T22:23:24.0000000%2B00:00&isEstimate=false&callbackUrl=http:%2F%2Fthis.is.a.callback.url"),
                    handler.Request.RequestUri);
                Assert.Equal(File.ReadAllText(filename), handler.RequestBody);
            }
            finally
            {
                if (File.Exists(filename))
                    File.Delete(filename);
            }
        }

        [Fact]
        public async Task ReqiresNotNullDataSet()
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await target.Sessions.CreateForecastSession((IEnumerable<DataSetRow>) null, "", DateTimeOffset.MinValue, DateTimeOffset.MaxValue));

            Assert.Equal("data", exception.ParamName);
        }
        [Fact]
        public async Task ReqiresNotNullOrEmptyDataSetName()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.Sessions.CreateForecastSession((string) null, "", DateTimeOffset.MinValue, DateTimeOffset.MaxValue));

            Assert.Equal("dataSetName", exception.ParamName);
        }
        [Fact]
        public async Task ReqiresNotNullStreamReader()
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await target.Sessions.CreateForecastSession((StreamReader) null, "", DateTimeOffset.MinValue, DateTimeOffset.MaxValue));

            Assert.Equal("input", exception.ParamName);
        }
        [Fact]
        public async Task ReqiresNotNullOrEmptyTargetColumn()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.Sessions.CreateForecastSession("dataSet", "", DateTimeOffset.MinValue, DateTimeOffset.MaxValue));

            Assert.Equal("targetColumn", exception.ParamName);
        }
    }
}
