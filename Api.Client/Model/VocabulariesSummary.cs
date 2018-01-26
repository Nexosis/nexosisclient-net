using System;
using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class VocabulariesSummary : Paged<VocabularySummary>
    {

        /// <summary>
        /// The Vocabularies
        /// </summary>
        public List<VocabularySummary> Items { get; set; } = new List<VocabularySummary>();
    }
}