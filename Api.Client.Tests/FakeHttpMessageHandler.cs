using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Client.Tests
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        public FakeHttpMessageHandler()
        {
            ReturnStatus = HttpStatusCode.OK;
            ResponseHeaders = new Dictionary<string, IEnumerable<string>>();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Request = request;

            if (request.Content != null)
                RequestBody = await request.Content.ReadAsStringAsync();

            var response = new HttpResponseMessage { StatusCode = ReturnStatus, Content = ContentResult };
            foreach (var item in ResponseHeaders)
                response.Headers.Add((string) item.Key, (IEnumerable<string>) item.Value);
            return await Task.FromResult(response);
        }

        public HttpRequestMessage Request { get; set; }
        public string RequestBody { get; set; }
        public HttpStatusCode ReturnStatus { get; set; }
        public HttpContent ContentResult { get; set; }
        public IDictionary<string, IEnumerable<string>> ResponseHeaders { get; }
    }
}