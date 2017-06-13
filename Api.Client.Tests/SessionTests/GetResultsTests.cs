using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.Client.Tests
{
    public class GetResultsTests : NexosisClient_TestsBase
    {
        public GetResultsTests() : base(new { session = new { } })
        {
        }

        [Fact]
        public async Task GetResultsReturnsThem()
        {
            var sessionId = Guid.NewGuid();
            await target.Sessions.GetSessionResults(sessionId);

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}/results"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task GetResultToFileThrowsWithNullWriter()
        {
            var sessionId = Guid.NewGuid();
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await target.Sessions.GetSessionResults(sessionId, (StreamWriter)null));

            Assert.Equal("output", exception.ParamName);
        }

        [Fact]
        public async Task PassesTransformFunction()
        {
            bool called = false;
            await target.Sessions.GetSessionResults(Guid.NewGuid(), (request, repsonse) => { called = true; });

            Assert.True(called, "Transform function not called.");
        }
    }
}
