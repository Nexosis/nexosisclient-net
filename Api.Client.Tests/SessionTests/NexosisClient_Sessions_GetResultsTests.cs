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
    public class NexosisClient_Sessions_GetResultsTests
    {
        private NexosisClient target;
        private FakeHttpMessageHandler handler;

        public NexosisClient_Sessions_GetResultsTests()
        {
            var data = new { };

            handler = new FakeHttpMessageHandler { ContentResult = new StringContent(JsonConvert.SerializeObject(data)) };
            target = new NexosisClient("abcdefg", "https://nada.nexosis.com/not-here", new ApiConnection.HttpClientFactory(handler));
        }

        [Fact]
        public async Task CallingItWillDoSomething()
        {
            
        }
    }
}
