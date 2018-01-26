using System;

namespace Nexosis.Api.Client.Model
{
    public class VocabularyWordsQuery
    {
        /// <summary>
        /// The vocabulary id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The type of word (Word or StopWord) to retrieve
        /// </summary>
        public WordType? Type { get; set; }

        /// <summary>
        /// Paging configuration for the response
        /// </summary>
        public PagingInfo Page { get; set; }
    }
}