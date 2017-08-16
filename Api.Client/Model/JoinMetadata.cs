using System;
using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class JoinMetadata
    {
        // Only one join source should be set on any join
        public DataSetJoinSource DataSet { get; set; }

        //private CalendarJoinSource Calendar { get; set; }

        public Dictionary<string, JoinColumnMetadata> Columns { get; set; } =
            new Dictionary<string, JoinColumnMetadata>(StringComparer.OrdinalIgnoreCase);

        public JoinMetadata[] Joins { get; set; }

    }
}