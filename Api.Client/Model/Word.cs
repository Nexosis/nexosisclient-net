namespace Nexosis.Api.Client.Model
{
    public class Word
    {
        /// <summary>
        /// The text of the word
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// The type of word
        /// </summary>
        public WordType Type { get; set; }
        
        /// <summary>
        /// The words relative importance as a feature
        /// </summary>
        public int? Rank { get; set; }
    }
}