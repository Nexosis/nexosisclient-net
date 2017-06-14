using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.SessionTests
{
    public class ListTests : NexosisClient_TestsBase
    {
        public ListTests() : 
            base(new
            {
                results = new List<SessionResponse>
                {
                    new SessionResponse { SessionId = Guid.NewGuid(), Type = SessionType.Forecast }
                }
            })
        {
        }

        [Fact]
        public async Task FormatsPropertiesForListSessions()
        {
            var result = await target.Sessions.List("alpha", "zulu", DateTimeOffset.Parse("2017-01-01"), DateTimeOffset.Parse("2017-01-11"));

            Assert.NotNull(result);
            Assert.Equal(handler.Request.RequestUri, new Uri(baseUri, $"sessions?dataSetName=alpha&eventName=zulu&startDate={DateTimeOffset.Parse("2017-01-01"):O}&endDate={DateTimeOffset.Parse("2017-01-11"):O}"));
        }

        [Fact]
        public async Task ExcludesPropertiesWhenNoneGiven()
        {
            var result = await target.Sessions.List();

            Assert.NotNull(result);
            Assert.Equal(handler.Request.RequestUri, new Uri(baseUri, "sessions"));
        }

        [Fact]
        public async Task PassesTransformFunction()
        {
            bool called = false;
            await target.Sessions.List("beta", "charlie", DateTimeOffset.Parse("2017-02-02 20:20:12 -0:00"), DateTimeOffset.Parse("2017-02-22 21:12 -0:00"), (request, response) =>
            {
                called = true; 
            });

            Assert.True(called, "Transform function not called.");
        }
    }
}
