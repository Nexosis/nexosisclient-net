using System;

namespace Nexosis.Api.Client.Model
{
    public class VocabulariesQuery
    {
        /// <summary>
        /// The session id used to generate the vocabulary
        /// </summary>
        public Guid SessionId { get; set; }

        /// <summary>
        /// The Text column in the Data Source from which the vocabulary was generated
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// The type of word to retrieve from the vocabulary
        /// </summary>
        public WordType? Type { get; set; }

        /// <summary>
        /// Paging configuration for the response
        /// </summary>
        public PagingInfo Page { get; set; }
    }
}