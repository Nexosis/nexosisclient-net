using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.Client.Tests
{
    public class RemoveSessionTests : NexosisClient_TestsBase
    {
        public RemoveSessionTests() : base(new { })
        {
        }

        [Fact]
        public async Task IdIsUsedInUrl()
        {
            var sessionId = Guid.NewGuid(); 
            await target.Sessions.RemoveSession(sessionId);

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task PassesTransformFunction()
        {
            bool called = false;
            await target.Sessions.RemoveSession(Guid.NewGuid(), (request, repsonse) => { called = true; });

            Assert.True(called, "Transform function not called.");
        }

    }
}
