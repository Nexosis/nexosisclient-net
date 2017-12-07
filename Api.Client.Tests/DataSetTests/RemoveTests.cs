using System;
using System.Net.Http;
using System.Threading.Tasks;
using Nexosis.Api.Client;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.DataSetTests
{
    public class RemoveTests : NexosisClient_TestsBase
    {
        public RemoveTests() : base(new {})
        {
        }

        [Fact]
        public async Task RemoveRequiresDataSetName()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.DataSets.Remove(new DataSetRemoveCriteria(null) {Options = DataSetDeleteOptions.None}));
            Assert.Equal("Name", exception.ParamName);
        }

        [Fact]
        public async Task GeneratesCascadeValuesFromDeleteOptions()
        {
            await target.DataSets.Remove(new DataSetRemoveCriteria("sierra") { Options = DataSetDeleteOptions.CascadeBoth});

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "data/sierra?cascade=forecast&cascade=session"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task DoesNotSetCascadeWhenNoneOptionGiven()
        {
            await target.DataSets.Remove(new DataSetRemoveCriteria("november"){Options = DataSetDeleteOptions.None});

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "data/november"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task RemoveForDateRangeQueriesThatRange()
        {
            await target.DataSets.Remove(new DataSetRemoveCriteria("oscar") {
                StartDate = DateTimeOffset.Parse("2015-10-12 22:23:24 -4:00"),
                EndDate = DateTimeOffset.Parse("2015-10-31 19:47:00 -4:00"), 
                Options = DataSetDeleteOptions.None
                
            });

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "data/oscar?startDate=2015-10-12T22:23:24.0000000-04:00&endDate=2015-10-31T19:47:00.0000000-04:00"), handler.Request.RequestUri);
        }
    }
}
