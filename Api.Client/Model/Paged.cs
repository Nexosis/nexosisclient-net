namespace Nexosis.Api.Client.Model
{
    public abstract class Paged<T> : Resource
    {

        /// <summary>
        /// The current page number
        /// </summary>
        public int PageNumber { get; set; }
        
        /// <summary>
        /// The total number of pages that could be returned
        /// </summary>
        public int TotalPages { get; set; }
        
        /// <summary>
        /// The number of records in the page
        /// </summary>
        public int PageSize { get; set; }
        
        /// <summary>
        /// The total count of records that could be returned
        /// </summary>
        public int TotalCount { get; set; }

        //next()
        //first()
        //last()
        //prev()
    }
}