using System;

namespace Nexosis.Api.Client.Model
{
    public class VocabularySummary : Resource
    {

        /// <summary>
        /// The session that built this vocabulary
        /// </summary>
        public Guid SessionId { get; set; }

        /// <summary>
        /// The column name in the dataset from which this vocabulary was bui;t
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// The number of words used from the corpus
        /// </summary>
        public int WordCount { get; set; }

        /// <summary>
        /// The number of words from the corpus that were ignored
        /// </summary>
        public int StopWordCount { get; set; }
    }
}