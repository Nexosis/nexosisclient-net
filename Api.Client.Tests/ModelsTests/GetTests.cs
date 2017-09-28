using Nexosis.Api.Client.Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.Client.Tests.ModelsTests
{
    public class GetTests : NexosisClient_TestsBase
    {
        public GetTests() : base(new ModelSummary())
        { }

        [Fact]
        public async Task LoadsById()
        {
            var id = Guid.NewGuid();

            await target.Models.Get(id);

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"model/{id}"), handler.Request.RequestUri);
        }

    }
}
