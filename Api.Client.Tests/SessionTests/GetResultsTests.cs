using System;
using System.Net.Http;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;
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
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}/results?pageSize=50"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task GetResultsByPredictionIntervalUsesPredictionInterval()
        {
            var sessionId = Guid.NewGuid();
            await target.Sessions.GetResults(sessionId, new SessionResultsQuery() {PredictionInterval = "0.5"});

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}/results?predictionInterval=0.5&pageSize=50"), handler.Request.RequestUri);
        }


        [Fact]
        public async Task GetResultsUsesPagingParameters()
        {
            var sessionId = Guid.NewGuid();
            await target.Sessions.GetResults(sessionId, new SessionResultsQuery() {Page = new PagingInfo(1,1)});

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}/results?page=1&pageSize=1"), handler.Request.RequestUri);
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
