using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Nexosis.Api.Client;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.DataSetTests
{
    public class GetTests : NexosisClient_TestsBase
    {
        public GetTests() : base(new DataSetDetail { Data = new List<Dictionary<string, string>>(), Columns = new Dictionary<string, ColumnMetadata>()})
        {
        }

        [Fact]
        public async Task LoadsByName()
        {
            await target.DataSets.Get("test");

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"data/test?page=0&pageSize={NexosisClient.MaxPageSize}"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task RequiresDataSetNameIsNotNullOrEmpty()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.DataSets.Get(""));
             
            Assert.Equal("dataSetName", exception.ParamName);
        }


        [Fact]
        public async Task IncludesAllParametersWhenGiven()
        {
            await target.DataSets.Get("test", 10, 10, DateTimeOffset.Parse("2017-01-01 0:00 -0:00"), DateTimeOffset.Parse("2017-01-31 0:00 -0:00"), new []{ "test1", "test2" });

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "data/test?page=10&pageSize=10&startDate=2017-01-01T00:00:00.0000000%2B00:00&endDate=2017-01-31T00:00:00.0000000%2B00:00&include=test1&include=test2"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task WillSaveDataSetToGivenFile()
        {
            var fileContent = "timestamp,lima\r\n2017-01-01,123\r\n2017-01-2,444\r\n2017-01-03,123";
            handler.ContentResult = new StringContent(fileContent, Encoding.UTF8);

            using (var stream = new MemoryStream())
            {
                using (var output = new StreamWriter(stream, Encoding.UTF8, 1024, leaveOpen: true))
                {
                    await target.DataSets.Get("kilo", output);
                }

                Assert.Equal(HttpMethod.Get, handler.Request.Method);
                Assert.Equal(new Uri(baseUri, "data/kilo"), handler.Request.RequestUri);

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
