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
                items = new List<SessionResponse>
                {
                    new SessionResponse { SessionId = Guid.NewGuid(), Type = SessionType.Forecast }
                }
            })
        {
        }

        [Fact]
        public async Task FormatsPropertiesForListSessions()
        {
            var result = await target.Sessions.List("alpha", "zulu", DateTimeOffset.Parse("2017-01-01 0:00 -0:00"), DateTimeOffset.Parse("2017-01-11 0:00 -0:00"));

            Assert.NotNull(result);
            Assert.Equal(new Uri(baseUri, "sessions?dataSetName=alpha&eventName=zulu&requestedAfterDate=2017-01-01T00:00:00.0000000%2B00:00&requestedBeforeDate=2017-01-11T00:00:00.0000000%2B00:00"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task ExcludesPropertiesWhenNoneGiven()
        {
            var result = await target.Sessions.List();

            Assert.NotNull(result);
            Assert.Equal(handler.Request.RequestUri, new Uri(baseUri, "sessions"));
        }

    }
}
