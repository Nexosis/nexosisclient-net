using System;
using System.Net.Http;
using Newtonsoft.Json;
using Nexosis.Api.Client;

namespace Api.Client.Tests
{
    public class NexosisClient_TestsBase
    {
        protected NexosisClient target;
        protected FakeHttpMessageHandler handler;

        protected Uri baseUri = new Uri("https://nada.nexosis.com/");

        public NexosisClient_TestsBase(object data)
        {
            handler = new FakeHttpMessageHandler { ContentResult = new StringContent(JsonConvert.SerializeObject(data)) };
            target = new NexosisClient("abcdefg", baseUri.ToString(), new ApiConnection.HttpClientFactory(handler));
        }
    }
}