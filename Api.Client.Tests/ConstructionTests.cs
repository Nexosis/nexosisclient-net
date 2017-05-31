using System;
using Nexosis.Api.Client;
using Xunit;

namespace Api.Client.Tests
{
    public class ConstructionTests
    {
        [Fact]
        public void CreatingClientWithNoKeyShouldThrow()
        {
            var exception = Assert.Throws<InvalidOperationException>(
                () => new Nexosis.Api.Client.ApiClient(null));
        }

        [Fact]
        public void CreatingWithKeyInConfigShouldUseConfig()
        {
            var target = new ApiClient();
            Assert.NotNull(target);
        }

        [Fact]
        public void CreationSetsHostValue()
        {
            var target = new ApiClient();
            Assert.NotNull(target.BaseUrl);
        }

    }
}
