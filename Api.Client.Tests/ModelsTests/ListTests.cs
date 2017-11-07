using Nexosis.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.Client.Tests.ModelsTests
{
    public class ListTests : NexosisClient_TestsBase
    {
        public ListTests() : base(new {
            items = new List<ModelSummary>
            {
                new ModelSummary { ModelId = Guid.NewGuid(), PredictionDomain = PredictionDomain.Regression }
            }
        })
        { }

        [Fact]
        public async Task WillNotIncludeFilterParametersWhenNull()
        {
            var result = await target.Models.List();

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "models?page=0&pageSize=50"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task FormatsPropertiesWhenProvided()
        {
            var result = await target.Models.List("data-source-name", DateTimeOffset.Parse("2017-01-01 0:00 -0:00"), DateTimeOffset.Parse("2017-01-11 0:00 -0:00"));

            Assert.NotNull(result);
            Assert.Equal(new Uri(baseUri, "models?dataSourceName=data-source-name&createdAfterDate=2017-01-01T00:00:00.0000000%2B00:00&createdBeforeDate=2017-01-11T00:00:00.0000000%2B00:00&page=0&pageSize=50"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task SetPageSizeIncludesParam()
        {
            var result = await target.Models.List(0, 20);
            Assert.Equal(handler.Request.RequestUri.Query, "?page=0&pageSize=20");
        }
    }
}
