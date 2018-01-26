using System;

namespace Nexosis.Api.Client.Model
{
    public class VocabulariesQuery
    {
        /// <summary>
        /// The session id used to generate the vocabulary
        /// </summary>
        public Guid? CreatedFromSession { get; set; }

        /// <summary>
        /// List vocabularies built from data sources matching this string
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// Paging configuration for the response
        /// </summary>
        public PagingInfo Page { get; set; }
    }
}