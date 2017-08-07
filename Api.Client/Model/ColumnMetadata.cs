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
        // Order of values is relevant for priority of recommended type
        String = 0,
        Numeric = 1,
        Logical = 2,
        Date = 3,
        NumericMeasure = -1
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