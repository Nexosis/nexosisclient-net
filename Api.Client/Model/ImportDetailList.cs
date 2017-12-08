using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class ImportDetailList : Paged<ImportDetail>
    {
        /// <summary>
        /// The list of Imports
        /// </summary>
        public List<ImportDetail> Items { get; set; }
    }
}