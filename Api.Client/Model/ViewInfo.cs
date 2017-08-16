using System;
using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class ViewInfo
    {

        public string DataSetName { get; set; }

        public Dictionary<string, ColumnMetadata> ColumnOptions { get; set; } =
            new Dictionary<string, ColumnMetadata>(StringComparer.OrdinalIgnoreCase);

        public JoinMetadata[] Joins { get; set; }
    }
}