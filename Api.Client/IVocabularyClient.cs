using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{
    public interface IVocabularyClient
    {
        Action<HttpRequestMessage, HttpResponseMessage> HttpMessageTransformer { get; set; }

        Task<VocabulariesSummary> List(VocabulariesQuery query = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<VocabularyResponse> Get(VocabularyWordsQuery query, CancellationToken cancellationToken = default(CancellationToken));

        Task Remove(VocabularyRemoveCriteria criteria, CancellationToken cancellationToken = default(CancellationToken));
    }
}