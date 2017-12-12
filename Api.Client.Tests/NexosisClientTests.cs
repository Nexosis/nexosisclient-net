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
        public void AddsTrailingSlashWhenNeeded()
        {
            var target = new NexosisClient("alpha-bravo-delta-charlie", "https://should.have.a.slash", new ApiConnection.HttpClientFactory());

            Assert.Equal("https://should.have.a.slash/", target.ConfiguredUrl);
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
            var handler = new FakeHttpMessageHandler { ContentResult = new StringContent(JsonConvert.SerializeObject(new { })) };
            var target = new NexosisClient("abcdefg", "https://nada.nexosis.com/not-here", new ApiConnection.HttpClientFactory(() => handler));

            await target.GetAccountBalance();

            Assert.True(handler.Request.Headers.Contains("api-key"));
            Assert.Equal("abcdefg", handler.Request.Headers.GetValues("api-key").First());
        }

        [Fact]
        public async Task AddsUserAgentToRequest()
        {
            var handler = new FakeHttpMessageHandler { ContentResult = new StringContent(JsonConvert.SerializeObject(new { })) };
            var target = new NexosisClient("abcdefg", "https://nada.nexosis.com/not-here", new ApiConnection.HttpClientFactory(() => handler));

            await target.GetAccountBalance();

            Assert.True(handler.Request.Headers.Contains("User-Agent"));
            Assert.Equal(NexosisClient.ClientVersion, handler.Request.Headers.GetValues("User-Agent").First());
        }

        [Fact]
        public async Task ProcessesQuotas()
        {
            var handler = new FakeHttpMessageHandler { ContentResult = new StringContent("{ }") };
            handler.ResponseHeaders.Add("nexosis-account-datasetcount-allotted", new[] { "100" });
            handler.ResponseHeaders.Add("nexosis-account-datasetcount-current", new[] {"99"});
            handler.ResponseHeaders.Add("nexosis-account-predictioncount-allotted", new[] {"50"});
            handler.ResponseHeaders.Add("nexosis-account-predictioncount-current", new[] {"49"});
            handler.ResponseHeaders.Add("nexosis-account-sessioncount-allotted", new[] {"1000"});
            handler.ResponseHeaders.Add("nexosis-account-sessioncount-current", new[] {"999"});
            

            var target = new NexosisClient("abcdefg", "https://nada.nexosis.com/not-here", new ApiConnection.HttpClientFactory(() => handler));

            var result = await target.GetAccountBalance();

            Assert.Equal(100, result.DataSetCount.Allotted);
            Assert.Equal(99, result.DataSetCount.Current);

            Assert.Equal(50, result.PredictionCount.Allotted);
            Assert.Equal(49, result.PredictionCount.Current);

            Assert.Equal(1000, result.SessionCount.Allotted);
            Assert.Equal(999, result.SessionCount.Current);
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
            var handler = new FakeHttpMessageHandler { ReturnStatus = HttpStatusCode.InternalServerError, ContentResult = new StringContent(JsonConvert.SerializeObject(data)), };

            var target = new NexosisClient("abcdefg", "https://nada.nexosis.com/not-here", new ApiConnection.HttpClientFactory(() => handler));

            var exception = await Assert.ThrowsAsync<NexosisClientException>(async () => await target.GetAccountBalance());

            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
            Assert.NotNull(exception.ErrorResponse);
            Assert.Equal(data.ErrorType, exception.ErrorResponse.ErrorType);
            Assert.Equal(data.Message, exception.ErrorResponse.Message);
            Assert.Equal(data.ErrorDetails, exception.ErrorResponse.ErrorDetails);
        }
    }
}
