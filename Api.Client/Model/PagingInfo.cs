namespace Nexosis.Api.Client.Model
{
    /// <summary>
    /// Used to request certain paging in a response
    /// </summary>
    public class PagingInfo
    {
        
        public static PagingInfo Default = new PagingInfo(null, 50);
        
        public PagingInfo(int? pageNumber = null, int? pageSize = null)
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