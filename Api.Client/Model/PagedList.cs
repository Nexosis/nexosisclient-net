using System;
using System.Collections.Generic;
using System.Text;

namespace Nexosis.Api.Client.Model
{
    /// <summary>
    /// Utility class to wrap list responses from GET entity requests
    /// </summary>
    /// <typeparam name="T">The type of listed entity to wrap</typeparam>
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        /// <summary>
        /// Initialize the object with an existing collection
        /// </summary>
        /// <param name="collection"></param>
        public PagedList(IEnumerable<T> collection) : base(collection)
        {
        }

        /// <summary>
        /// Initialize with a paged serialized response object 
        /// </summary>
        /// <param name="response">The response directly from the api client request</param>
        public PagedList(IPagedList<T> response)
        {
            PageSize = response.PageSize;
            PageNumber = response.PageNumber;
            TotalPages = response.TotalPages;
            TotalCount = response.TotalCount;
            Links = response.Links;
            if (response.Items != null)
                this.InsertRange(0, response.Items);
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public List<Link> Links { get; set; }
        public List<T> Items { get; set; }
    }

    public interface IPagedList<T>
    {
        int PageSize { get; set; }
        int PageNumber { get; set; }
        int TotalPages { get; set; }
        int TotalCount { get; set; }
        List<Link> Links { get; set; }
        List<T> Items { get; set; }
    }
}
