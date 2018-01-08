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
        
        Task<VocabulariesSummary> List(Guid sessionId, CancellationToken cancellationToken = default(CancellationToken));

        Task<VocabularyResponse> Get(VocabulariesQuery query, CancellationToken cancellationToken = default(CancellationToken));
    }
}