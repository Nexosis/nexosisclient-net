namespace Nexosis.Api.Client.Model
{
    public class ColumnMetadata
    {
        public ColumnType DataType { get; set; }
        public ColumnRole Role { get; set; }
    }

    public enum ColumnType
    {
        String,
        Numeric,
        Logical,
        Date,
    }
    
    public enum ColumnRole
    {
        None,
        Timestamp,
        Target,
        Feature
    }
}