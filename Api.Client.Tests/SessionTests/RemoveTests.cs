using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.SessionTests
{
    public class RemoveTests : NexosisClient_TestsBase
    {
        public RemoveTests() : base(new {})
        {
        }


        [Fact]
        public async Task HandlerIncludesOptionalArgsIfTheyAreSet()
        {
            await target.Sessions.Remove(new SessionRemoveCriteria()
            {
                DataSourceName = "data-set-name",
                Type = SessionType.Forecast
            });

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "sessions?dataSourceName=data-set-name&type=Forecast"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task IncludesDatesInUrlWhenGiven()
        {
            var criteria = new SessionRemoveCriteria()
            {
                RequestedAfterDate = DateTimeOffset.Parse("2017-02-02 20:20:12 -0:00"),
                RequestedBeforeDate = DateTimeOffset.Parse("2017-02-22 21:12 -0:00")
            };
            
            await target.Sessions.Remove(criteria);

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "sessions?requestedAfterDate=2017-02-02T20:20:12.0000000%2B00:00&requestedBeforeDate=2017-02-22T21:12:00.0000000%2B00:00"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task IdIsUsedInUrl()
        {
            var sessionId = Guid.NewGuid(); 
            await target.Sessions.Remove(sessionId);

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}"), handler.Request.RequestUri);
        }
    }
}
