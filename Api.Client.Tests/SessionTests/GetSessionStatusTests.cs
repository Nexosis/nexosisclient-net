﻿using System;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.SessionTests
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
            var result = await target.Sessions.GetStatus(sessionId);

            Assert.Equal(sessionId, result.SessionId);
            Assert.Equal(Status.Started, result.Status);
        }

        [Fact]
        public async Task HttpTransformerIsWrappedAndCalled()
        {
            bool called = false;
            target.Sessions.HttpMessageTransformer = (request, response) => { called = true; };
            var result = await target.Sessions.GetStatus(Guid.NewGuid());

            Assert.True(called, "Http transform function not called");
        }
    }
}
