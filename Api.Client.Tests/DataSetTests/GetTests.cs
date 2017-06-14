using System;
using System.Net.Http;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.DataSetTests
{
    public class GetTests : NexosisClient_TestsBase
    {
        public GetTests() : base(new { data = new DataSetRow[] {} })
        {
        }

        [Fact]
        public async Task LoadsByName()
        {
            var result = await target.DataSets.Get("test");

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "data/test?page=0&pageSize=100"), handler.Request.RequestUri);
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
            var result = await target.DataSets.Get("test", 10, 10, DateTimeOffset.Parse("2017-01-01"), DateTimeOffset.Parse("2017-01-31"), new []{ "test1", "test2" });

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "data/test?page=10&pageSize=10&startDate=2017-01-01T00:00:00.0000000-05:00&endDate=2017-01-31T00:00:00.0000000-05:00&include=test1&include=test2"), handler.Request.RequestUri);
        }

    }
}
