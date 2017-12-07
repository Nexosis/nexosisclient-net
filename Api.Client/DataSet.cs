using System.IO;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{
    /// <summary>
    /// Contains convenience methods for working with Data Sets
    /// </summary>
    public static class DataSet
    {
        /// <summary>
        /// Create a Data Source from a <see cref="DataSetDetail"/>
        /// </summary>
        /// <param name="name">The name of the DataSet to be created</param>
        /// <param name="data">The data</param>
        /// <returns>An IDataSetSource to be used for creating the data set</returns>
        public static IDataSetSource From(string name, DataSetDetail data)
        {
            return new DataSetDetailSource(name, data);
        }

        /// <summary>
        /// Create a Data Source from a Csv stream
        /// </summary>
        /// <param name="name">The name of the DataSet to be created</param>
        /// <param name="reader">A <see cref="System.IO.StreamReader"/> with csv data</param>
        /// <returns>An IDataSetSource to be used for creating the data set</returns>
        public static IDataSetSource From(string name, StreamReader reader)
        {
            return new DataSetStreamSource(name, reader);
        }

        /// <summary>
        /// Create a <see cref="DataSetDataQuery"/> with criteria for getting data from a DataSet
        /// </summary>
        /// <param name="name">The DataSet from which the data should be retrieved </param>
        /// <param name="query">Additional query criteria</param>
        /// <returns>A <see cref="DataSetDataQuery"/> defining the query critera </returns>
        public static DataSetDataQuery Get(string name, DataSetDataQuery query = null)
        {
            var queryObject = query ?? new DataSetDataQuery(name);
            queryObject.Name = name;
            return queryObject;
        }

    }
}