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
            Assert.Equal(new Uri(baseUri, "views"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task WillIncludePartialNameParameterWhenNotNull()
        {
            var result = await target.Views.List(new ViewQuery {PartialName = "partialSomething"});

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "views?PartialName=partialSomething"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task WillIncludeDataSetNameParameterWhenNotNull()
        {
            var result = await target.Views.List(new ViewQuery {DataSetName = "something"});

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "views?DataSetName=something"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task WillIncludePagingParameters()
        {
            var result = await target.Views.List(new ViewQuery {Page = 1, PageSize = 10});

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "views?Page=1&PageSize=10"), handler.Request.RequestUri);
        }
    }
}
