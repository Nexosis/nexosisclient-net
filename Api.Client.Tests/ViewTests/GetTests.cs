using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Nexosis.Api.Client;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.ViewTests
{
    public class GetTests : NexosisClient_TestsBase
    {
        public GetTests() : base(new ViewDetail() { Data = new List<Dictionary<string, string>>(), Columns = new Dictionary<string, ColumnMetadata>()})
        {
        }

        [Fact]
        public async Task LoadsByName()
        {
            await target.Views.Get("test");

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"views/test"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task RequiresDataSetNameIsNotNullOrEmpty()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.Views.Get(""));
             
            Assert.Equal("viewName", exception.ParamName);
        }


        [Fact]
        public async Task IncludesAllParametersWhenGiven()
        {
            //10, 10, DateTimeOffset.Parse("2017-01-01 0:00 -0:00"), DateTimeOffset.Parse("2017-01-31 0:00 -0:00"), new []{ "test1", "test2" });
            await target.Views.Get("test",
                new GetViewOptions()
                {
                    Page = 10,
                    PageSize = 10,
                    StartDate = new DateTimeOffset(2017, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    EndDate = new DateTimeOffset(2017, 1, 31, 0, 0, 0, TimeSpan.Zero),
                    Include = new[]{"test1", "test2"}
                });
                

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "views/test?StartDate=2017-01-01T00:00:00.0000000%2B00:00&EndDate=2017-01-31T00:00:00.0000000%2B00:00&Page=10&PageSize=10&include=test1&include=test2"), handler.Request.RequestUri);
        }

    }
}
