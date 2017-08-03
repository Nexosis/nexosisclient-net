namespace Nexosis.Api.Client.Model
{
    public class ColumnMetadata
    {
        public ColumnType? DataType { get; set; }
        public ColumnRole? Role { get; set; }
        
        public ImputationStrategy? Imputation { get; set; }
        public AggregationStrategy? Aggregation { get; set; }
    }

    public enum ColumnType
    {
        String,
        Numeric,
        Logical,
        Date,
        NumericMeasure
    }

    public enum ColumnRole
    {
        None,
        Timestamp,
        Target,
        Feature
    }

    public enum ImputationStrategy
    {
        Zeroes,
        Mean,
        Median,
        Mode
    }

    public enum AggregationStrategy
    {
        Sum,
        Mean,
        Median,
        Mode,
    }
}