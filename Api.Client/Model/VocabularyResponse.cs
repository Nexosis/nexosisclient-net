using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class VocabularyResponse : Paged<Word>
    {
        public List<Word> Items { get; set; }
    }
}