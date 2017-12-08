using Nexosis.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nexosis.Api.Client;
using Xunit;

namespace Api.Client.Tests
{
#if !SKIP_INTEGRATION // integration tests will charge your account and require an API key in environment variables
    [Collection("Integration")]
    public class ModelIntegrationTests
    {
        private readonly IntegrationTestFixture fixture;

        public ModelIntegrationTests(IntegrationTestFixture fixture)
        {
            this.fixture = fixture;
            var model = fixture.Client.Models.List(new ModelSummaryQuery {Page = new PagingInfo(0, 20)}).GetAwaiter().GetResult();
            if(model == null)
            {
                CreateSession().GetAwaiter().GetResult();
            }
            else
            {
                savedModel = model.Items.FirstOrDefault();
            }
        }

        ModelSummary savedModel;
        private async Task CreateSession()
        {
            var session = await fixture.Client.Sessions.TrainModel(Sessions.TrainModel(fixture.ModelDataSetName, PredictionDomain.Regression, "instances"));
            while (true)
            {
                var status = await fixture.Client.Sessions.GetStatus(session.SessionId);
                if (status.Status == Status.Completed || status.Status == Status.Failed)
                    break;
                Thread.Sleep(5000);
            }
            var modelSession = await fixture.Client.Sessions.Get(session.SessionId);
            savedModel = await fixture.Client.Models.Get(modelSession.ModelId.Value);
        }

        [Fact]
        public async Task ModelStartsNewSession()
        {
            var dataSetName = $"testDataSet-{DateTime.Now:s}";
            var dataSet = DataSetGenerator.Run(90, 10, "instances");
            await fixture.Client.DataSets.Create(DataSet.From(dataSetName, dataSet));

            var actual = await fixture.Client.Sessions.TrainModel(Sessions.TrainModel(dataSetName,
                PredictionDomain.Regression,
                options: new ModelSessionRequest
                {
                    Columns = new Dictionary<string, ColumnMetadata>
                    {
                        ["instances"] = new ColumnMetadata {DataType = ColumnType.Numeric, Role = ColumnRole.Target}
                    }
                }));
            Assert.NotNull(actual.SessionId);
            await fixture.Client.DataSets.Remove(new DataSetRemoveCriteria(dataSetName) {Options = DataSetDeleteOptions.CascadeAll});
        }

        [Fact]
        public async Task GetModelListHasItems()
        {
            var models = await fixture.Client.Models.List(new ModelSummaryQuery() {Page = new PagingInfo(0, 1)});
            Assert.True(models.Items.Count > 0);
        }

        [Fact]
        public async Task ListRespectsPagingInfo()
        {
            var models = await fixture.Client.Models.List(new ModelSummaryQuery {Page = new PagingInfo(1, 2)});
            var actual = models;
            Assert.NotNull(actual);
            Assert.Equal(1, actual.PageNumber);
            Assert.Equal(2, actual.PageSize);
        }

        [Fact]
        public async Task GetModelDetailsHasResults()
        {
            var result = await fixture.Client.Models.Get(savedModel.ModelId);

            Assert.NotNull(result);
            Assert.Equal(savedModel.ModelId, result.ModelId);
        }

        [Fact]
        public async Task PredictModelReturnsResults()
        {
            var record = DataSetGenerator.Run(1, 10, null).Data;

            var result = await fixture.Client.Models.Predict(new ModelPredictionRequest(savedModel.ModelId, record));

            Assert.NotEmpty(result.Data);
        }
    }
#endif
}
