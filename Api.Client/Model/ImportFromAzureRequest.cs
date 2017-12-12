namespace Nexosis.Api.Client.Model
{
    /// <summary>
    /// The parameters needed to import data into the Nexosis API from Azure blob storage
    /// </summary>
    public class ImportFromAzureRequest : ImportRequest
    {

        /// <summary>
        /// The Azure Storage connection string to use when connecting to Azure
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Name of the Azure blob container from which to import
        /// </summary>
        public string Container { get; set; }

        /// <summary>
        /// Name of the Azure blob (i.e. file) containing data to import
        /// </summary>
        /// <remarks>The Nexosis API can import a single file at a time.  The file can be in either csv or json format, and optionally with gzip compression.</remarks>
        public string Blob { get; set; }

    }
}