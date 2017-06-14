using System;
using System.Net.Http;
using System.Threading.Tasks;
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
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.DataSets.Remove(null, DataSetDeleteOptions.None));
            Assert.Equal("dataSetName", exception.ParamName);
        }

        [Fact]
        public async Task GeneratesCascadeValuesFromDeleteOptions()
        {
            await target.DataSets.Remove("sierra", DataSetDeleteOptions.CascadeBoth);

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "data/sierra?cascade=forecast&cascade=sessions"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task DoesNotSetCascadeWhenNoneOptionGiven()
        {
            await target.DataSets.Remove("november", DataSetDeleteOptions.None);

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "data/november"), handler.Request.RequestUri);
        }
    }
}
