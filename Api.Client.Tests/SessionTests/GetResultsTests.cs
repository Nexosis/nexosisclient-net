using System;
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

        [Fact]
        public async Task GetClassScoresReturnsThen()
        {
            var sessionId = Guid.NewGuid();
            await target.Sessions.GetResultClassScores(sessionId);

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}/results/classscores"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task GetAnomalyScoresReturnsThen()
        {
            var sessionId = Guid.NewGuid();
            await target.Sessions.GetResultAnomalyScores(sessionId);

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}/results/anomalyscores"), handler.Request.RequestUri);
        }
    }
}
