using System;
using System.Net.Http;
using System.Threading.Tasks;
using Nexosis.Api.Client;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.VocabularyTests
{
    public class GetVocabulariesTest : NexosisClient_TestsBase
    {
        public GetVocabulariesTest() : base(new { }) { }

        [Fact]
        public async Task ListVocabulariesUsesCorrectUrl()
        {
            await target.Vocabularies.List();

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"vocabulary?pageSize=50"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task ListVocabulariesIncludesSessionId()
        {
            var sessionId = Guid.NewGuid();
            await target.Vocabularies.List(new VocabulariesQuery()
            {
                CreatedFromSession = sessionId,
                DataSource = "foo",
                Page = new PagingInfo(1,1)
            });

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"vocabulary?dataSource=foo&createdFromSession={sessionId}&page=1&pageSize=1"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task GetVocabularyWordsWithDefaultParmetersUsesCorrectUrl()
        {
            var vocabularyId = Guid.NewGuid();
            await target.Vocabularies.Get(Vocabularies.For(vocabularyId));

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"vocabulary/{vocabularyId}?pageSize=50"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task GetVocabularyWordsIncludesQueryParameters()
        {
            var vocabularyId = Guid.NewGuid();
            await target.Vocabularies.Get(Vocabularies.For(vocabularyId, page: new PagingInfo(1,1), type: WordType.StopWord));

            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, $"vocabulary/{vocabularyId}?type=StopWord&page=1&pageSize=1"), handler.Request.RequestUri);
        }



    }
}