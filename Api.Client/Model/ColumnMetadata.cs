using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

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
        NumericMeasure = -1,
        Text = 4
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ColumnRole
    {
        [EnumMember(Value = "None")]
        None,

        [EnumMember(Value = "Timestamp")]
        Timestamp,

        [EnumMember(Value = "Target")]
        Target,

        [EnumMember(Value = "Feature")]
        Feature,

        [EnumMember(Value = "Key")] 
        Key
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
        Min,
        Max,
    }
}