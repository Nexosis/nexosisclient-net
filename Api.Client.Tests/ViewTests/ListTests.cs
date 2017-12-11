using System;
using System.Net.Http;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.ViewTests
{
    public class ListTests : NexosisClient_TestsBase
    {
        public ListTests() : base(new {})
        {
        }

        [Fact]
        public async Task WillNotIncludeFilterParameterWhenNull()
        {
            var result = await target.Views.List();

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "views?pageSize=50"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task WillIncludePartialNameParameterWhenNotNull()
        {
            var result = await target.Views.List(new ViewQuery {PartialName = "partialSomething"});

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "views?partialName=partialSomething&pageSize=50"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task WillIncludeDataSetNameParameterWhenNotNull()
        {
            var result = await target.Views.List(new ViewQuery {DataSetName = "something"});

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "views?dataSetName=something&pageSize=50"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task WillIncludePagingParameters()
        {
            var result = await target.Views.List(new ViewQuery {Page = new PagingInfo(1, 10)});

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "views?page=1&pageSize=10"), handler.Request.RequestUri);
        }

    }
}
