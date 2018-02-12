using System;
using System.Net.Http;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.VocabularyTests
{
    public class RemoveTests : NexosisClient_TestsBase
    {
        public RemoveTests() : base(new { })
        { }

        [Fact]
        public async Task IdIsUsedInUrl()
        {
            var vocabularyId = Guid.NewGuid();
            await target.Vocabularies.Remove(new VocabularyRemoveCriteria() { VocabularyId = vocabularyId });

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"vocabulary/{vocabularyId}"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task IncludesCriteriaInUrlWhenGiven()
        {
            var sessionId = Guid.NewGuid();
            await target.Vocabularies.Remove(new VocabularyRemoveCriteria
            {
                DataSourceName = "data-source-name",
                CreatedFromSession = sessionId,
            });

            Assert.Equal(HttpMethod.Delete, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"vocabulary?dataSource=data-source-name&createdFromSession={sessionId}"), handler.Request.RequestUri);
        }
    }
}
