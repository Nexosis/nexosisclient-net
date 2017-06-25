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
        public Guid SavedSessionId => Guid.Parse("015ce0d8-1443-41aa-a85e-eb2f5536684b");

        public IntegrationTestFixture()
        {
            Client = new NexosisClient("de5831dbb5e349709e05d76cf5bbbc9f", "https://api.dev.nexosisdev.com/api/", new ApiConnection.HttpClientFactory()); 
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