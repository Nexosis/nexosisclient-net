using System;
using System.Net.Http;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.ViewTests
{
    public class DeleteTests : NexosisClient_TestsBase
    {
        public DeleteTests() : base(new { })
        {
        }

        [Fact]
        public async Task RemoveRequiresDataSetName()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.Views.Remove(new ViewDeleteCriteria(null)));
            Assert.Equal("Name", exception.ParamName);

            exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.Views.Remove(null));
            Assert.Equal("Name", exception.ParamName);
        }

        [Fact]
        public async Task GeneratesCascadeValuesFromDeleteOptions()
        {
            await target.Views.Remove(new ViewDeleteCriteria("sierra") { Cascade = ViewCascadeOptions.CascadeAll });

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "views/sierra?cascade=session&cascade=model&cascade=vocabulary"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task DoesNotSetCascadeWhenNoOptionGiven()
        {
            await target.Views.Remove(new ViewDeleteCriteria("november"));

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "views/november"), handler.Request.RequestUri);
        }
    }
}
