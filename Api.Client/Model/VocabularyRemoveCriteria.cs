using System;

namespace Nexosis.Api.Client.Model
{
    public class VocabularyRemoveCriteria
    {

        /// <summary>
        /// The ID of the vocabulary to be removed
        /// </summary>
        public Guid? VocabularyId { get; set; }

        /// <summary>
        /// All vocabularies built from data sources with names containing this string should be removed
        /// </summary>
        public string DataSourceName { get; set; }

        /// <summary>
        /// Vocabularies created by this session should be removed
        /// </summary>
        public Guid? CreatedFromSession { get; set; }
    }
}