using System;

namespace Nexosis.Api.Client.Model
{
    public class SessionResultStatus 
    {
        public Guid SessionId { get; set; }
        public SessionStatus Status { get; set; }
    }

}