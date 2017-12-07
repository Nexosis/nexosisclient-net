namespace Nexosis.Api.Client.Model
{
    /// <summary>
    /// Used to request certain paging in a response
    /// </summary>
    public class PagingInfo
    {
        public PagingInfo(int? pageNumber, int? pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        /// <summary>
        /// The page number to be retrieved
        /// </summary>
        public int? PageNumber { get; set; }

        /// <summary>
        /// The size of the page to be returned
        /// </summary>
        public int? PageSize { get; set; }
    }
}