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
        public Guid SavedSessionId => Guid.Parse("015cacc9-19bc-4e27-a2a5-6cd83c26ce92");

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