using System;
using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class VocabularyResponse : Paged<Word>
    {
        public Guid Id { get; set; }
        
        public List<Word> Items { get; set; }
    }
}