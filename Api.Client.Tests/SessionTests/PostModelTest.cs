using System;
using System.Net.Http;
using System.Threading.Tasks;
using Nexosis.Api.Client;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.SessionTests
{
    public class PostModelTest : NexosisClient_TestsBase
    {
        public PostModelTest() : base(new { })
        { }

        [Fact]
        public async Task SetsDataSourceNameWhenGiven()
        {
            var request = Sessions.TrainModel("data-source-name", PredictionDomain.Regression, "target-column");

            request.CallbackUrl = "http://this.is.a.callback.url";

            await target.Sessions.TrainModel(request);

            Assert.Equal(HttpMethod.Post, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "sessions/model"), handler.Request.RequestUri);
            var expected = "{\"PredictionDomain\":\"regression\",\"DataSourceName\":\"data-source-name\",\"TargetColumn\":\"target-column\",\"Columns\":{},\"CallbackUrl\":\"http://this.is.a.callback.url\"}";
            Assert.Equal(expected, handler.RequestBody);
        }


        [Fact]
        public async Task RequiresNotNullOrEmptyDataSourceName()
        {
            var request = Sessions.TrainModel(null, PredictionDomain.Regression, "target-column");

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.Sessions.TrainModel(request));

            Assert.Equal("dataSourceName", exception.ParamName);
        }

        [Fact]
        public async Task IncludesBalanceOptionWhenSpecified()
        {
            var request = Sessions.TrainModel(
                "data-source-name",
                PredictionDomain.Classification,
                "target-column",
                new ClassificationModelSessionRequest { Balance = true });

            await target.Sessions.TrainModel(request);

            Assert.Equal(HttpMethod.Post, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "sessions/model"), handler.Request.RequestUri);
            var expected = "{\"PredictionDomain\":\"classification\",\"ExtraParameters\":{\"balance\":\"True\"},\"DataSourceName\":\"data-source-name\",\"TargetColumn\":\"target-column\",\"Columns\":{}}";
            Assert.Equal(expected, handler.RequestBody);
        }

        [Fact]
        public async Task IncludesContainsAnomaliesOptionWhenSpecified()
        {
            var request = Sessions.TrainModel(
                "data-source-name",
                PredictionDomain.Anomalies,
                "target-column",
                new AnomaliesModelSessionRequest { ContainsAnomalies = true });

            await target.Sessions.TrainModel(request);

            Assert.Equal(HttpMethod.Post, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "sessions/model"), handler.Request.RequestUri);
            var expected = "{\"PredictionDomain\":\"anomalies\",\"ExtraParameters\":{\"containsAnomalies\":\"True\"},\"DataSourceName\":\"data-source-name\",\"TargetColumn\":\"target-column\",\"Columns\":{}}";
            Assert.Equal(expected, handler.RequestBody);
        }
    }
}
