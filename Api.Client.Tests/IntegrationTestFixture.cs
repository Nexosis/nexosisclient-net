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
        public Guid SavedSessionId => Guid.Parse("015cac09-6ff6-4f41-967f-4def3269c763");

        public IntegrationTestFixture()
        {
            Client = new NexosisClient(Environment.GetEnvironmentVariable(NexosisClient.NexosisApiKeyEnvironmentVariable), "https://api.dev.nexosisdev.com/api/", new ApiConnection.HttpClientFactory()); 
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                var dataSets = Client.DataSets.List().GetAwaiter().GetResult();
                if (dataSets != null)
                {
                    foreach (var dataSet in dataSets)
                    {
                        if (string.Equals(dataSet.DataSetName, SavedDataSet)) continue;
                        Client.DataSets.Remove(dataSet.DataSetName, DataSetDeleteOptions.CascadeBoth).GetAwaiter().GetResult();
                    }
                }

                var sessions = Client.Sessions.List().GetAwaiter().GetResult();
                if (sessions != null)
                {
                    foreach (var session in sessions)
                    {
                        if (session.SessionId.Equals(SavedSessionId)) continue;
                        Client.Sessions.Remove(session.SessionId).GetAwaiter().GetResult();
                    }
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