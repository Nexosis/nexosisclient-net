using Nexosis.Api.Client;
using System;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests
{
    [CollectionDefinition("Integration")]
    public class IntegrationCollection : ICollectionFixture<IntegrationTestFixture> { }

    public class IntegrationTestFixture : IDisposable
    {
        public IntegrationTestFixture()
        {
            string baseUrl = "https://ml.nexosis.com/v1";
            baseUrl = Environment.GetEnvironmentVariable(NexosisClient.NexosisApiUriEnvironmentVariable);
            ForecastDataSetName = "forecast.015ce681-ca24-449d-a673-699aff25a0cc";
            Client = new NexosisClient(Environment.GetEnvironmentVariable(NexosisClient.NexosisApiKeyEnvironmentVariable)
                , baseUrl
                , new ApiConnection.HttpClientFactory());

            var dataSetTask = Client.DataSets.Create(DataSet.From(ForecastDataSetName,
                DataSetGenerator.Run(DateTime.Parse("2016-01-01"), DateTime.Parse("2017-03-26"), "instances")));
            dataSetTask.GetAwaiter().GetResult();

            var keyedDatasetTask = Client.DataSets.Create(DataSet.From(ModelDataSetName,
                DataSetGenerator.Run(90, 10, "instances")));
            keyedDatasetTask.GetAwaiter().GetResult();
        }



        public NexosisClient Client { get; set; }

        // by default these are dev related values.
        public string SavedDataSet { get; set; } = "alpha.persistent";
        public Guid SavedSessionId { get; set; }
        public Guid SavedHourlySessionId { get; set; }
        public string ForecastDataSetName { get; set; } = "forecast.015ce5b0-8d68-495c-b7e3-1d4293cdeb5a";
        public string ModelDataSetName { get; set; } = "model.5AF982B2-C259-44C4-B635-9B095FE1B494";

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    var task = Client.DataSets.Remove(new DataSetRemoveCriteria(ForecastDataSetName) {Options =  Nexosis.Api.Client.Model.DataSetDeleteOptions.CascadeAll});
                    task.GetAwaiter().GetResult();

                    var removeModelDataset = Client.DataSets.Remove(new DataSetRemoveCriteria(ModelDataSetName){Options = Nexosis.Api.Client.Model.DataSetDeleteOptions.CascadeAll});
                    removeModelDataset.GetAwaiter().GetResult();
                }
                catch
                { //already removed...
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
