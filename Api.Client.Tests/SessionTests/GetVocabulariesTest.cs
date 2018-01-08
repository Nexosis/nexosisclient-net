using System;
using System.Net.Http;
using System.Threading.Tasks;
using Nexosis.Api.Client;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.SessionTests
{
    public class GetVocabulariesTest : NexosisClient_TestsBase
    {
        public GetVocabulariesTest() : base(new { }) { }

        [Fact]
        public async Task ListVocabulariesUsesCorrectUrl()
        {
            var sessionId = Guid.NewGuid();
            await target.Sessions.Vocabularies.List(sessionId);

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}/results/vocabularies"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task GetVocabularyWordsWithDefaultParmetersUsesCorrectUrl()
        {
            var sessionId = Guid.NewGuid();
            await target.Sessions.Vocabularies.Get(Vocabularies.For(sessionId, "mycolumn"));

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}/results/vocabularies/mycolumn?pageSize=50"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task GetVocabularyWordsEscapesColumnName()
        {
            var sessionId = Guid.NewGuid();
            await target.Sessions.Vocabularies.Get(Vocabularies.For(sessionId, "my column"));

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}/results/vocabularies/my%20column?pageSize=50").AbsoluteUri,
                handler.Request.RequestUri.AbsoluteUri);
        }

        [Fact]
        public async Task GetVocabularyWordsWithPagingUsesPagingParameters()
        {
            var sessionId = Guid.NewGuid();
            await target.Sessions.Vocabularies.Get(Vocabularies.For(sessionId, "mycolumn", page: new PagingInfo(10,5)));

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}/results/vocabularies/mycolumn?page=10&pageSize=5"),
                handler.Request.RequestUri);
        }

        [Fact]
        public async Task GetVocabularyWordsWithTypeUsesCorrectUrl()
        {
            var sessionId = Guid.NewGuid();
            await target.Sessions.Vocabularies.Get(Vocabularies.For(sessionId, "mycolumn", type: WordType.Word));

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"sessions/{sessionId}/results/vocabularies/mycolumn?type=Word&pageSize=50"),
                handler.Request.RequestUri);
        }

    }
}