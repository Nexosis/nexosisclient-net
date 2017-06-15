using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.DataSetTests
{
    public class GetForecastTests : NexosisClient_TestsBase
    {
        public GetForecastTests() : base(new { data = new DataSetRow[] { } })
        {
        }

        [Fact]
        public async Task ThrowsExceptionWithoutDataSetName()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.DataSets.GetForecast(null));

            Assert.Equal("dataSetName", exception.ParamName);
        }

        [Fact]
        public async Task QueriesForDataSetData()
        {
            await target.DataSets.GetForecast("hotel", DateTimeOffset.Parse("2017-01-01 00:00:00 -0:00"), DateTimeOffset.Parse("2017-05-01 00:00:00 -0:00"), 1, 50, new [] { "india", "juliet" });

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "data/hotel/forecast?page=1&pageSize=50&startDate=2017-01-01T00:00:00.0000000%2B00:00&endDate=2017-05-01T00:00:00.0000000%2B00:00&include=india&include=juliet"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task WillSaveForcecastDataToFile()
        {
            var fileContent = "timestamp,lima\r\n2017-01-01,123\r\n2017-01-2,444\r\n2017-01-03,123";
            handler.ContentResult = new StringContent(fileContent, Encoding.UTF8);

            using (var stream = new MemoryStream())
            {
                using (var output = new StreamWriter(stream, Encoding.UTF8, 1024, leaveOpen: true))
                {
                    await target.DataSets.GetForecast("kilo", output);
                }

                Assert.Equal(HttpMethod.Get, handler.Request.Method);
                Assert.Equal(new Uri(baseUri, "data/kilo/forecast?page=0&pageSize=100"), handler.Request.RequestUri);

                stream.Position = 0;
                using (var data = new StreamReader(stream, Encoding.UTF8))
                {
                    var result = data.ReadToEnd();
                    Assert.Equal(fileContent, result.Substring(0, result.Length - 1)); 
                }
            }
        }
    }
}
