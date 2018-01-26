using System;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{
    public static class Vocabularies
    {
        public static VocabularyWordsQuery For(Guid id, WordType? type = null, PagingInfo page = null)
        {
            return new VocabularyWordsQuery()
            {
                Id = id,
                Type = type,
                Page = page
            };
        }
               
    }
}