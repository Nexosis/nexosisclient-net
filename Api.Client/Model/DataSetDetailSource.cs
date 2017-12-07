namespace Nexosis.Api.Client.Model
{
    /// <summary>
    /// Put data into a DataSet from a DataSetDetail
    /// </summary>
    public class DataSetDetailSource : IDataSetSource
    {
        private readonly DataSetDetail data;
        private readonly string name;

        public DataSetDetailSource(string name, DataSetDetail data)
        {
            this.data = data;
            this.name = name;
        }

        /// <summary>
        /// The data
        /// </summary>
        public DataSetDetail Data => data;

        /// <summary>
        /// The DataSet name
        /// </summary>
        public string Name => name;
    }
}