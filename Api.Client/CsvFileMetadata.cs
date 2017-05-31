using System;
using System.Collections.Generic;
using System.Text;

namespace Nexosis.Api.Client
{
    public class CsvFileMetadata
    {
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public double MBSizeOnDisk { get; set; }
        public bool HasHeader { get; set; }
        public char Delimiter { get; set; }
    }
}
