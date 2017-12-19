using System.Runtime.Serialization;

namespace Nexosis.Api.Client.Model
{
    public enum ImportType
    {
        [EnumMember(Value = "s3")] S3 = 0,
        [EnumMember(Value = "url")] Url = 1,
        [EnumMember(Value = "azure")] Azure = 2,
    }
}