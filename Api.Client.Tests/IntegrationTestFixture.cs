using System;
using Nexosis.Api.Client;
using Nexosis.Api.Client.Model;

namespace Api.Client.Tests
{
    public class IntegrationTestFixture : IDisposable
    {
        public NexosisClient Client { get; set; }

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
                        Client.DataSets.Remove(dataSet.DataSetName, DataSetDeleteOptions.CascadeBoth).GetAwaiter()
                            .GetResult();
                    }
                }

                var sessions = Client.Sessions.List().GetAwaiter().GetResult();
                if (sessions != null)
                {
                    foreach (var session in sessions)
                    {
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