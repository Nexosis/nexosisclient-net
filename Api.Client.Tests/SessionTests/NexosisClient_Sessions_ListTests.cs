using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nexosis.Api.Client;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests
{
    public class NexosisClient_Sessions_ListTests
    {
        private NexosisClient target;
        private FakeHttpMessageHandler handler;

        public NexosisClient_Sessions_ListTests()
        {
            var data = new
            {
                results = new List<SessionResponse>
                {
                    new SessionResponse { SessionId = Guid.NewGuid(), Type = SessionType.Forecast }
                }
            };

            handler = new FakeHttpMessageHandler { ContentResult = new StringContent(JsonConvert.SerializeObject(data)) };
            target = new NexosisClient("abcdefg", "https://nada.nexosis.com/not-here", new ApiConnection.HttpClientFactory(handler));
        }

        [Fact]
        public async Task FormatsPropertiesForListSessions()
        {
            var result = await target.Sessions.ListSessions("alpha", "zulu", DateTimeOffset.Parse("2017-01-01"), DateTimeOffset.Parse("2017-01-11"));

            Assert.NotNull(result);
            Assert.Equal(handler.Request.RequestUri, new Uri($"https://nada.nexosis.com/not-here/sessions?dataSetName=alpha&eventName=zulu&startDate={DateTimeOffset.Parse("2017-01-01"):O}&endDate={DateTimeOffset.Parse("2017-01-11"):O}"));
        }

        [Fact]
        public async Task ExcludesPropertiesWhenNoneGiven()
        {
            var result = await target.Sessions.ListSessions();

            Assert.NotNull(result);
            Assert.Equal(handler.Request.RequestUri, new Uri("https://nada.nexosis.com/not-here/sessions"));
        }
    }
}
