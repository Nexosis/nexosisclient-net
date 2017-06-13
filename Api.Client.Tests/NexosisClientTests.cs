using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nexosis.Api.Client;
using Xunit;

namespace Api.Client.Tests
{
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
            var data = new ErrorResponse
            {
                StatusCode = 500,
                ErrorType = "SomethingWentWrong",
                Message = "An error occurred",
                ErrorDetails = new Dictionary<string, object> { { "error", "details" } }
            };
            var handler = new FakeHttpMessageHandler { ReturnStatus = HttpStatusCode.InternalServerError,  ContentResult = new StringContent(JsonConvert.SerializeObject(data)), };

            var target = new NexosisClient("abcdefg", "https://nada.nexosis.com/not-here", new ApiConnection.HttpClientFactory(handler));

            var exception = await Assert.ThrowsAsync<NexosisClientException>(async () => await target.GetAccountBalance());

            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
            Assert.NotNull(exception.ErrorResponse);
            Assert.Equal(data.ErrorType, exception.ErrorResponse.ErrorType);
            Assert.Equal(data.Message, exception.ErrorResponse.Message);
            Assert.Equal(data.ErrorDetails, exception.ErrorResponse.ErrorDetails);
        }
    }
}
