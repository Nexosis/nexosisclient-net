using System;
using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class DataSetData : DataSet
    {
        /// <summary> Hypermedia links to related content </summary>
        public List<Link> Links { get; set; }
    }
}