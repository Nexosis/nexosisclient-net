namespace Nexosis.Api.Client.Model
{
    /// <summary>
    /// A request to import into a DataSet from a file on AWS S3
    /// </summary>
    public class ImportFromS3Request : ImportRequest
    {
        /// <summary>
        /// The bucket in which the file resides
        /// </summary>
        public string Bucket { get; set; }

        /// <summary>
        /// The path (relative to the bucket) to the file
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The AWS Region in which the bucket resides.  Optional.  US-East-1 will be used by default if not specified.
        /// </summary>
        public string Region { get; set; }
    }
}