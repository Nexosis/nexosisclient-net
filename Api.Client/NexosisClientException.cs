using System;
using System.Collections.Generic;
using System.Net;

namespace Nexosis.Api.Client
{
    public class NexosisClientException : Exception
    {
        public NexosisClientException(string message, HttpStatusCode status) : base(message)
        {
            StatusCode = status;
            ErrorDetails = new Dictionary<string, object>();
        }

        public NexosisClientException(string message, HttpStatusCode status, IDictionary<string, object> details) : base(message)
        {
            StatusCode = status;
            ErrorDetails = details;
        }

        public HttpStatusCode StatusCode { get; set; }
        public IDictionary<string, object> ErrorDetails { get; set; }
    }

    public class ErrorResponse
    {
        public ErrorResponse()
        {
            ErrorDetails = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        public int StatusCode { get; set; }
        public string ErrorType { get; set; }
        public Dictionary<string, object> ErrorDetails { get; set; }
    }
}

