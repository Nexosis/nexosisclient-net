using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.Client.Tests.SessionTests
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
            await target.Sessions.GetResults(sessionId);

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}/results"), handler.Request.RequestUri);
        }


        [Fact]
        public async Task GetConfusionMatrixReturnsIt()
        {
            var sessionId = Guid.NewGuid();
            await target.Sessions.GetResultConfusionMatrix(sessionId);

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}/results/confusionmatrix"), handler.Request.RequestUri);
        }

    }
}
