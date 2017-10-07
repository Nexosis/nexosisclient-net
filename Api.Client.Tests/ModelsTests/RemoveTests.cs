using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.Client.Tests.ModelsTests
{
    public class RemoveTests : NexosisClient_TestsBase
    {
        public RemoveTests() : base(new { })
        { }

        [Fact]
        public async Task IdIsUsedInUrl()
        {
            var modelId = Guid.NewGuid();
            await target.Models.Remove(modelId);

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"models/{modelId}"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task IncludesDatesInUrlWhenGiven()
        {
            await target.Models.Remove("data-source-name", DateTimeOffset.Parse("2017-02-02 20:20:12 -0:00"), DateTimeOffset.Parse("2017-02-22 21:12 -0:00"));

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "models?createdAfterDate=2017-02-02T20:20:12.0000000%2B00:00&createdBeforeDate=2017-02-22T21:12:00.0000000%2B00:00&dataSourceName=data-source-name"), handler.Request.RequestUri);
        }
    }
}
