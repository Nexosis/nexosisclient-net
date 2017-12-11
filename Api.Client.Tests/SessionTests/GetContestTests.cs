using System;
using System.Net.Http;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.SessionTests
{
    public class GetContestTests : NexosisClient_TestsBase
    {
        public GetContestTests() : base(new { }) { }

        [Fact]
        public async Task GetContestUsesCorrectUrl()
        {
            var sessionId = Guid.NewGuid();
            await target.Sessions.Contest.GetContest(sessionId);

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}/contest"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task GetChampionUsesCorrectUrl()
        {
            var sessionId = Guid.NewGuid();
            await target.Sessions.Contest.GetChampion(sessionId,
                new ChampionQueryOptions() {PredictionInterval = "0.5"});

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}/contest/champion?predictionInterval=0.5"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task GetSelectionUsesCorrectUrl()
        {
            var sessionId = Guid.NewGuid();
            await target.Sessions.Contest.GetSelection(sessionId);

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}/contest/selection"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task ListContestantsUsesCorrectUrl()
        {
            var sessionId = Guid.NewGuid();
            await target.Sessions.Contest.ListContestants(sessionId);

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}/contest/contestants"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task GetContestantUsesCorrectUrl()
        {
            var sessionId = Guid.NewGuid();
            var contestantId = "foo";
            await target.Sessions.Contest.GetContestant(sessionId, contestantId, new ChampionQueryOptions() {PredictionInterval = "0.5"});

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}/contest/contestants/{contestantId}?predictionInterval=0.5"), handler.Request.RequestUri);
        }
    }
}