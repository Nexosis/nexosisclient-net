using System;
using Nexosis.Api.Client;
using Xunit;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;

namespace Api.Client.Tests
{
    [CollectionDefinition("Integration")]
    public class IntegrationCollection : ICollectionFixture<IntegrationTestFixture> { }

    public class IntegrationTestFixture : IDisposable
    {
        public IntegrationTestFixture()
        {
            string baseUrl = "https://ml.nexosis.com/v1";
            if ("Test".Equals(Environment.GetEnvironmentVariable("TEST_ENVIRONMENT"), StringComparison.OrdinalIgnoreCase))
            {
                baseUrl = Environment.GetEnvironmentVariable(NexosisClient.NexosisApiUriEnvironmentVariable);

                SavedSessionId = Guid.Parse("015ce681-ca24-449d-a673-699aff25a0cc");
                SavedHourlySessionId = Guid.Parse("015ce681-cccb-4374-ba06-91d699981890");
                ForecastDataSetName = "forecast.015ce681-ca24-449d-a673-699aff25a0cc";
            }
            Client = new NexosisClient(Environment.GetEnvironmentVariable(NexosisClient.NexosisApiKeyEnvironmentVariable)
                , baseUrl
                , new ApiConnection.HttpClientFactory());

            var dataSetTask = Client.DataSets.Create(ForecastDataSetName,
                DataSetGenerator.Run(DateTime.Parse("2016-01-01"), DateTime.Parse("2017-03-26"), "instances"));
            dataSetTask.GetAwaiter().GetResult();
        }

        

        public NexosisClient Client { get; set; }

        // by default these are dev related values.
        public string SavedDataSet { get; set; } = "alpha.persistent";
        public Guid SavedSessionId { get; set; }
        public Guid SavedHourlySessionId { get; set; }
        public string ForecastDataSetName { get; set; } = "forecast.015ce5b0-8d68-495c-b7e3-1d4293cdeb5a";

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    var task = Client.DataSets.Remove(ForecastDataSetName, Nexosis.Api.Client.Model.DataSetDeleteOptions.CascadeAll);
                    task.GetAwaiter().GetResult();
                }
                catch { //already removed...
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
