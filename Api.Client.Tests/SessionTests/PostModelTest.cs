using Nexosis.Api.Client.Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Nexosis.Api.Client;
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
            var request = Sessions.TrainModel("data-source-name", PredictionDomain.Regression,
                "target-column");

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

        
    }
}
