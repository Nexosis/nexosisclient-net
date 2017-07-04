using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.Client.Tests
{
#if !SKIP_INTEGRATION // integration tests will charge your account and require an API key in environment variables
    [Collection("Integration")]
    public class ImportIntegrationTests
    {
        private IntegrationTestFixture fixture;

        public ImportIntegrationTests(IntegrationTestFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task ImportFromS3StartsSession()
        {
            var response = await fixture.Client.Imports.ImportFromS3("s3-import-locationa", "nexosis-sample-data",
                "LocationA.csv", "us-east-1");
            
            Assert.NotEqual(Guid.Empty, response.ImportId);
        }
    }
#endif
}