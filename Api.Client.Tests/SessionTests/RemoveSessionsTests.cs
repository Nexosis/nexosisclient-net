using System;
using System.Net.Http;
using System.Threading.Tasks;
using Nexosis.Api.Client;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests
{
    public class RemoveSessionsTests : NexosisClient_TestsBase
    {
        public RemoveSessionsTests() : base(new {})
        {
        }

        [Fact]
        public async Task HandlerDoesNotIncludeOptionalArgsIfTheyAreNotSet()
        {
            await target.Sessions.RemoveSessions();

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "sessions"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task HandlerIncludesOptionalArgsIfTheyAreSet()
        {
            await target.Sessions.RemoveSessions("data-set-name", "event-name", SessionType.Forecast);

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "sessions?dataSetName=data-set-name&eventName=event-name&type=Forecast"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task IncludesDatesInUrlWhenGiven()
        {
            await target.Sessions.RemoveSessions(null, null, null, DateTimeOffset.Parse("2017-02-02 20:20:12 -0:00"), DateTimeOffset.Parse("2017-02-22 21:12 -0:00"));

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "sessions?startDate=2017-02-02T20:20:12.0000000%2B00:00&endDate=2017-02-22T21:12:00.0000000%2B00:00"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task PassesTransformFunction()
        {
            bool called = false;
            await target.Sessions.RemoveSessions(null, null, null, DateTimeOffset.Parse("2017-02-02 20:20:12 -0:00"), DateTimeOffset.Parse("2017-02-22 21:12 -0:00"), (request, response) =>
            {
                called = true; 
            });

            Assert.True(called, "Transform function not called.");
        }
    }
}
