namespace Nexosis.Api.Client.Model
{
    public class ViewQuery
    {
        /// <summary>
        /// Limits results to only those view definitions with names containing the specified value
        /// </summary>
        public string PartialName { get; set; }

        /// <summary>
        /// Limits results to only those view definitions that reference the specified dataset
        /// </summary>
        public string DataSetName { get; set; }

        /// <summary>
        /// Zero-based page number of view definitions to retrieve
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// Count of view definitions to retrieve in each page (max 1000)
        /// </summary>
        public int? PageSize { get; set; }
    }
}