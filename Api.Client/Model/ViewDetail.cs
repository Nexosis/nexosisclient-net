using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class ViewDetail : ViewDefinition
    {
        /// <summary>
        /// The data
        /// </summary>
        public List<Dictionary<string, string>> Data { get; set; }
    }
}