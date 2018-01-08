using System;
using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class VocabulariesSummary
    {
        /// <summary>
        /// The session in which the vocabularies were built
        /// </summary>
        public Guid SessionId { get; set; }

        /// <summary>
        /// The Vocabularies
        /// </summary>
        public List<VocabularySummary> Items { get; set; } = new List<VocabularySummary>();
    }
}