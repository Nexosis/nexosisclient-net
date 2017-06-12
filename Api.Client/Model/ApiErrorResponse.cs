using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string ErrorType { get; set; }
        public Dictionary<string, object> ErrorDetails { get; set; }
    }
}
