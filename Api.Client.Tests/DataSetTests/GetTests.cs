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
            await target.DataSets.Get(DataSet.Get("test"));

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"data/test?pageSize=50"), handler.Request.RequestUri);
        }

        

        [Fact]
        public async Task RequiresDataSetNameIsNotNullOrEmpty()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.DataSets.Get(DataSet.Get("")));
             
            Assert.Equal("Name", exception.ParamName);
        }


        [Fact]
        public async Task IncludesAllParametersWhenGiven()
        {
            await target.DataSets.Get(DataSet.Get("test", new DataSetDataQuery()
            {
                Page = new PagingInfo(10, 10),
                StartDate = DateTimeOffset.Parse("2017-01-01 0:00 -0:00"),
                EndDate = DateTimeOffset.Parse("2017-01-31 0:00 -0:00"),
                IncludedColumns = new[] {"test1", "test2"}
            }));

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "data/test?startDate=2017-01-01T00:00:00.0000000%2B00:00&endDate=2017-01-31T00:00:00.0000000%2B00:00&include=test1&include=test2&page=10&pageSize=10"), handler.Request.RequestUri);
        }
    }
}
