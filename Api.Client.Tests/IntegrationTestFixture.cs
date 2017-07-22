using System;
using Nexosis.Api.Client;
using Xunit;

namespace Api.Client.Tests
{
    [CollectionDefinition("Integration")]
    public class IntegrationCollection : ICollectionFixture<IntegrationTestFixture> { }

    public class IntegrationTestFixture : IDisposable
    {
        public IntegrationTestFixture()
        {
            string url = "https://api.dev.nexosisdev.com/v1/";
            if ("Test".Equals(Environment.GetEnvironmentVariable("TEST_ENVIRONMENT"), StringComparison.OrdinalIgnoreCase))
            {
                url = "https://api.uat.nexosisdev.com/v1/";
                SavedSessionId = Guid.Parse("015ce681-ca24-449d-a673-699aff25a0cc");
                SavedHourlySessionId = Guid.Parse("015ce681-cccb-4374-ba06-91d699981890");
                ForecastDataSetName = "forecast.015ce681-ca24-449d-a673-699aff25a0cc";
            }
            Client = new NexosisClient(Environment.GetEnvironmentVariable(NexosisClient.NexosisApiKeyEnvironmentVariable), url, new ApiConnection.HttpClientFactory());

        }

        public NexosisClient Client { get; set; }

        // by default these are dev related values.
        public string SavedDataSet { get; set; } = "alpha.persistent";
        public Guid SavedSessionId { get; set; } = Guid.Parse("015ce5b0-8d68-495c-b7e3-1d4293cdeb5a");
        public Guid SavedHourlySessionId { get; set; } = Guid.Parse("015ce58b-e32f-45b7-aba3-030f119392ce");
        public string ForecastDataSetName { get; set; } = "forecast.015ce5b0-8d68-495c-b7e3-1d4293cdeb5a";


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
