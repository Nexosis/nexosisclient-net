using System.IO;

namespace Nexosis.Api.Client.Model
{
    /// <summary>
    /// Put data into a DataSet from a stream of csv data
    /// </summary>
    public class DataSetStreamSource : IDataSetSource
    {
        private readonly StreamReader streamReader;
        private readonly string name;

        public DataSetStreamSource(string name, StreamReader streamReader)
        {
            this.streamReader = streamReader;
            this.name = name;
        }

        /// <summary>
        /// The data
        /// </summary>
        public StreamReader Data => streamReader;

        /// <summary>
        /// The DataSet name
        /// </summary>
        public string Name => name;
    }
}