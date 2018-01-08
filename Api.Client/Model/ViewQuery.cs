using Nexosis.Api.Client.Utility;

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
        /// Paging info for the response
        /// </summary>
        public PagingInfo Page { get; set; }
    }
}