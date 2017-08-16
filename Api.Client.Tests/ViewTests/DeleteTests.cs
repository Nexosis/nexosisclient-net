using System;
using System.Net.Http;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.ViewTests
{
    public class DeleteTests : NexosisClient_TestsBase
    {
        public DeleteTests() : base(new {})
        {
        }

        [Fact]
        public async Task RemoveRequiresDataSetName()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.Views.Remove(null, null));
            Assert.Equal("viewName", exception.ParamName);
        }

        [Fact]
        public async Task GeneratesCascadeValuesFromDeleteOptions()
        {
            await target.Views.Remove("sierra", new ViewDeleteOptions() {Cascade = ViewCascadeOptions.CascadeSessions});

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "views/sierra?cascade=sessions"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task DoesNotSetCascadeWhenNoneOptionGiven()
        {
            await target.Views.Remove("november");

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "views/november"), handler.Request.RequestUri);
        }
    }
}
