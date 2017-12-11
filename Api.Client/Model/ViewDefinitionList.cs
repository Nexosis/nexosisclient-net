using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class ViewDefinitionList : Paged<ViewDefinition>
    {
        /// <summary>
        /// The views
        /// </summary>
        public List<ViewDefinition> Items { get; set; }
    }
}