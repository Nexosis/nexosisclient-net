using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.Client.Tests.ImportTests {
    public class GetTests : NexosisClient_TestsBase
    {

        public GetTests() : base(new { }) { }

        [Fact]
        public async Task GetsFromCorrectUrl()
        {
            var id = Guid.NewGuid();
            var result = await target.Imports.Get(id);
            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri($"https://nada.nexosis.com/imports/{id}"), handler.Request.RequestUri);
        }
    }
}