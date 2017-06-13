using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nexosis.Api.Client;
using Xunit;

namespace Api.Client.Tests
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        public FakeHttpMessageHandler()
        {
            ResponseHeaders = new Dictionary<string, IEnumerable<string>>();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Request = request;

            if (request.Content != null)
                RequestBody = await request.Content.ReadAsStringAsync();

            var response = new HttpResponseMessage { StatusCode = ReturnStatus, Content = ContentResult };
            foreach (var item in ResponseHeaders)
            {
                response.Headers.Add(item.Key, item.Value);
            }            
            return await Task.FromResult(response);
        }


        public HttpRequestMessage Request { get; set; }
        public string RequestBody { get; set; }
        public HttpStatusCode ReturnStatus => HttpStatusCode.OK;
        public HttpContent ContentResult { get; set; }
        public IDictionary<string, IEnumerable<string>> ResponseHeaders { get; }
    }


    public class NexosisClientTests
    {
        [Fact]
        public void GetsKeyFromEnvironment()
        {
            Environment.SetEnvironmentVariable(NexosisClient.NexosisApiKeyEnvironmentVariable, "abcdefg");
            var target = new NexosisClient();
            Assert.Equal("abcdefg", target.ApiKey);
        }

        [Fact]
        public void CanGiveKeyWhenConstructing()
        {
            var target = new NexosisClient("asdfasdfasdf");
            Assert.Equal("asdfasdfasdf", target.ApiKey);
        }

        [Fact]
        public async Task AddsApiKeyHeaderToRequest()
        {
            var handler = new FakeHttpMessageHandler { ContentResult = new StringContent(JsonConvert.SerializeObject(new {})) };
            var target = new NexosisClient("abcdefg", "https://nada.nexosis.com/not-here", new ApiConnection.HttpClientFactory(handler));

            await target.GetAccountBalance();

            Assert.True(handler.Request.Headers.Contains("api-key"));
            Assert.Equal("abcdefg", handler.Request.Headers.GetValues("api-key").First());
        }

        [Fact]
        public async Task AddsUserAgentToRequest()
        {
            var handler = new FakeHttpMessageHandler { ContentResult = new StringContent(JsonConvert.SerializeObject(new {})) };
            var target = new NexosisClient("abcdefg", "https://nada.nexosis.com/not-here", new ApiConnection.HttpClientFactory(handler));

            await target.GetAccountBalance();

            Assert.True(handler.Request.Headers.Contains("User-Agent"));
            Assert.Equal(NexosisClient.ClientVersion, handler.Request.Headers.GetValues("User-Agent").First());
        }

        [Fact]
        public async Task ProcessesCostAndBalance()
        {
            var handler = new FakeHttpMessageHandler { ContentResult = new StringContent("{ }") };
            handler.ResponseHeaders.Add("nexosis-request-cost", new [] { "123.12 USD" });
            handler.ResponseHeaders.Add("nexosis-account-balance",new [] { "999.99 USD" });

            var target = new NexosisClient("abcdefg", "https://nada.nexosis.com/not-here", new ApiConnection.HttpClientFactory(handler));

            var result = await target.GetAccountBalance();

            Assert.Equal(123.12m, result.Cost.Amount);
            Assert.Equal("USD", result.Cost.Currency);
            Assert.Equal(999.99m, result.Balance.Amount);
            Assert.Equal("USD", result.Balance.Currency);
        }

        [Fact]
        public async Task CanHandleErrorResponse()
        {
            
        }

        [Fact]
        public async Task WhenHandlingErrorAndThatErrorsWillStillThrowWithSomethingUseful()
        {
            
        }
    }
}
