using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.Client.Tests.DataSetTests
{
    public class ListTests : NexosisClient_TestsBase
    {
        public ListTests() : base(new {})
        {
        }

        [Fact]
        public async Task WillNotIncludeFilterParameterWhenNull()
        {
            var result = await target.DataSets.ListDataSets();

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "data"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task WillIncludeFilterParameterWhenNotNull()
        {
            var result = await target.DataSets.ListDataSets("partialSomething");

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "data?partialName=partialSomething"), handler.Request.RequestUri);
        }
    }
}
