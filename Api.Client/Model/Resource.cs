using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public abstract class Resource
    {
        /// <summary>
        /// Links to related resources
        /// </summary>
        public List<Link> Links { get; set; }
    }
}