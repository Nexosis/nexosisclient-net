using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class SessionResponseList : Paged<SessionResponse>
    {
        /// <summary>
        /// The sessions
        /// </summary>
        public List<SessionResponse> Items { get; set; }
    }
}