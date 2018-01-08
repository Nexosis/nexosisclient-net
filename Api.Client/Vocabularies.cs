using System;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{
    public static class Vocabularies
    {
        public static VocabulariesQuery For(Guid sessionId, string columnName, WordType? type = null,
            PagingInfo page = null)
        {
            return new VocabulariesQuery()
            {
                SessionId = sessionId,
                ColumnName = columnName,
                Type = type,
                Page = page
            };
        }
    }
}