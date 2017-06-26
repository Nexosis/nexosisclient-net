using System;
using System.Collections.Generic;
using Nexosis.Api.Client;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests
{
    [CollectionDefinition("Integration")]
    public class IntegrationCollection : ICollectionFixture<IntegrationTestFixture> { }

    public class IntegrationTestFixture : IDisposable
    {
        public NexosisClient Client { get; set; }

        public string SavedDataSet =>  "alpha.persistent";
        public Guid SavedSessionId => Guid.Parse("015ce5b0-8d68-495c-b7e3-1d4293cdeb5a");
        public string ForecastDataSetName => "forecast.015ce5b0-8d68-495c-b7e3-1d4293cdeb5a";

        public IntegrationTestFixture()
        {
            Client = new NexosisClient(Environment.GetEnvironmentVariable(NexosisClient.NexosisApiKeyEnvironmentVariable), "https://api.dev.nexosisdev.com/v1/", new ApiConnection.HttpClientFactory()); 
        }

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
