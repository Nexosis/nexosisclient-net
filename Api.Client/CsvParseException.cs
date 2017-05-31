using System;
using System.Collections.Generic;
using System.Text;

namespace Nexosis.Api.Client
{
    public class CsvParseException : Exception
    {
        public CsvParseException(string message) : base(message) { }
        public List<Exception> ParseExceptions { get; set; }
    }
}
