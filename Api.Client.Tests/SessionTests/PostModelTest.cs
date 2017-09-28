using Nexosis.Api.Client.Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.Client.Tests.SessionTests
{
    public class PostModelTest : NexosisClient_TestsBase
    {
        public PostModelTest(): base(new { })
        { }

        [Fact]
        public async Task SetsDataSetNameWhenGiven()
        {
            await target.Sessions.TrainModel("data-source-name", "target-column", PredictionDomain.Regression, "http://this.is.a.callback.url");

            Assert.Equal(HttpMethod.Post, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "sessions/model"), handler.Request.RequestUri);
            var expected = "{\"DataSourceName\":\"data-source-name\",\"Columns\":{\"target-column\":{\"Role\":\"Target\"}},\"PredictionDomain\":\"regression\",\"CallbackUrl\":\"http://this.is.a.callback.url\",\"IsEstimate\":false}";
            Assert.Equal(expected, handler.RequestBody);
        }

        [Fact]
        public async Task RequiresNotNullDetail()
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await target.Sessions.TrainModel((ModelSessionDetail)null));

            Assert.Equal("data", exception.ParamName);
        }

        [Fact]
        public async Task RequiresNotNullOrEmptyDataSourceName()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.Sessions.TrainModel((string)null, "target-column", PredictionDomain.Regression));

            Assert.Equal("dataSourceName", exception.ParamName);
        }

        [Fact]
        public async Task RequiredNotNullOrEmptyTargetColumn()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.Sessions.TrainModel("data-source-name", null, PredictionDomain.Regression));

            Assert.Equal("targetColumn", exception.ParamName);
        }
    }
}
