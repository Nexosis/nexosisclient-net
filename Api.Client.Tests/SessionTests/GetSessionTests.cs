using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.Client.Tests.SessionTests
{
    public class GetSessionTests : NexosisClient_TestsBase
    {
        public GetSessionTests() : base(new {})
        {
        }

        [Fact]
        public async Task PutsSessionIdInUri()
        {
            var sessionId = Guid.NewGuid();
            await target.Sessions.Get(sessionId);

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task PassesTransformFunction()
        {
            bool called = false;
            await target.Sessions.Get(Guid.NewGuid(), (request, response) => { called = true; }); 

            Assert.True(called, "Transform function not called.");
        }
    }
}
