using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.ModelsTests
{
    public class PostModelPredict : NexosisClient_TestsBase
    {
        public PostModelPredict() : base(new { })
        { }

        [Fact]
        public async Task WillSendValuesForPrediction()
        {
            var modelId = Guid.NewGuid();
            await target.Models.Predict(new ModelPredictionRequest(modelId, new List<Dictionary<string, string>> { new Dictionary<string, string> { ["column"] = "value" } }));

            Assert.Equal(HttpMethod.Post, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"models/{modelId}/predict"), handler.Request.RequestUri);
            Assert.Equal("{\"Data\":[{\"column\":\"value\"}]}", handler.RequestBody);
        }

        [Fact]
        public async Task RequiresNotNullFeatures()
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await target.Models.Predict(new ModelPredictionRequest(Guid.NewGuid(), null)));

            Assert.Equal("Data", exception.ParamName);
        }
    }
}
