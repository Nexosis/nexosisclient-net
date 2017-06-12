using System;
using System.Collections.Generic;
using System.Net;

namespace Nexosis.Api.Client
{
    public class NexosisClientException : Exception
    {
        public NexosisClientException(string message) : base(message) { }

        public NexosisClientException(string message, Exception inner) : base(message, inner) { }

        public NexosisClientException(string message, ErrorResponse response) : base(message)
        {
            StatusCode = (HttpStatusCode)response.StatusCode;
            ErrorResponse = response;
        }
        public NexosisClientException(string message, HttpStatusCode status) : base(message)
        {
            StatusCode = status;
        }

        public HttpStatusCode StatusCode { get; set; }
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class ErrorResponse
    {
        public ErrorResponse()
        {
            ErrorDetails = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string ErrorType { get; set; }
        public Dictionary<string, object> ErrorDetails { get; set; }
    }
}

