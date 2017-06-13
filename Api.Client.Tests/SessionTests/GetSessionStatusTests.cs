using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nexosis.Api.Client;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests
{
    public class GetSessionStatusTests : NexosisClient_TestsBase
    {
        public GetSessionStatusTests() : base(new { })
        {
            handler.ResponseHeaders.Add("Nexosis-Session-Status", new[] { "Started" }); 
        }

        [Fact]
        public async Task StatusHeaderIsAssignedToResult()
        {
            var sessionId = Guid.NewGuid();
            var result = await target.Sessions.GetSessionStatus(sessionId);

            Assert.Equal(sessionId, result.SessionId);
            Assert.Equal(SessionStatus.Started, result.Status);
        }

        [Fact]
        public async Task HttpTransformerIsWrappedAndCalled()
        {
            bool called = false;
            var result = await target.Sessions.GetSessionStatus(Guid.NewGuid(), (request, response) => { called = true; });

            Assert.True(called, "Http transform function not called");
        }
    }
}
