using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{
    public class DataSetClient : IDataSetClient
    {
        private readonly ApiConnection apiConnection;

        public DataSetClient(ApiConnection apiConnection)
        {
            this.apiConnection = apiConnection;
        }

        public async Task<DataSetSummary> CreateDataSet(string dataSetName, IEnumerable<DataSetRow> data)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetSummary> CreateDataSet(string dataSetName, IEnumerable<DataSetRow> data, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetSummary> CreateDataSet(string dataSetName, IEnumerable<DataSetRow> data, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DataSetSummary>> ListDataSets()
        {
            throw new NotImplementedException();
        }

        public async Task<List<DataSetSummary>> ListDataSets(string nameFilter)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DataSetSummary>> ListDataSets(string nameFilter, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DataSetSummary>> ListDataSets(string nameFilter, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSet(string dataSetName)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSet(string dataSetName, int pageNumber, int pageSize, IEnumerable<string> includeColumns)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSet(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate,
            IEnumerable<string> includeColumns)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSet(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate,
            IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSet(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate,
            IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSet(string dataSetName, TextWriter output)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSet(string dataSetName, TextWriter output, int pageNumber, int pageSize, IEnumerable<string> includeColumns)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSet(string dataSetName, TextWriter output, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSet(string dataSetName, TextWriter output, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSet(string dataSetName, TextWriter output, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveDataSet(string dataSetName, DataSetDeleteOptions options)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveDataSet(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, DataSetDeleteOptions options)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveDataSet(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, DataSetDeleteOptions options,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveDataSet(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, DataSetDeleteOptions options,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSetForecast(string dataSetName)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSetForecast(string dataSetName, int pageNumber, int pageSize, IEnumerable<string> includeColumns)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSetForecast(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, int pageNumber,
            int pageSize, IEnumerable<string> includeColumns)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSetForecast(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSetForecast(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSetForecast(string dataSetName, TextWriter output)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSetForecast(string dataSetName, TextWriter output, int pageNumber, int pageSize,
            IEnumerable<string> includeColumns)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSetForecast(string dataSetName, TextWriter output, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSetForecast(string dataSetName, TextWriter output, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task GetDataSetForecast(string dataSetName, TextWriter output, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveDataSetForecast(string dataSetName)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveDataSetForecast(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveDataSetForecast(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ForecastModel>> ListDataSetForecastModels(string dataSetName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ForecastModel>> ListDataSetForecastModels(string dataSetName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ForecastModel>> ListDataSetForecastModels(string dataSetName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ForecastModel> GetDataSetForecastModel(string dataSetName, string targetColumn)
        {
            throw new NotImplementedException();
        }

        public async Task<ForecastModel> GetDataSetForecastModel(string dataSetName, string targetColumn, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<ForecastModel> GetDataSetForecastModel(string dataSetName, string targetColumn, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSetForecastModelForecast(string dataSetName, string targetColumn, string algorithmKey)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetData> GetDataSetForecastModelForecast(string dataSetName, string targetColumn, string algorithmKey,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
