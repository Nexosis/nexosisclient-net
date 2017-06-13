using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public Task<DataSetSummary> CreateDataSet(string dataSetName, IEnumerable<DataSetRow> data, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetSummary> CreateDataSet(string dataSetName, IEnumerable<DataSetRow> data, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<DataSetSummary>> ListDataSets()
        {
            return ListDataSets(null);
        }

        public Task<List<DataSetSummary>> ListDataSets(string partialName)
        {
            return ListDataSets(partialName, null);
        }

        public Task<List<DataSetSummary>> ListDataSets(string partialName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return ListDataSets(partialName, httpMessageTransformer, CancellationToken.None);
        }

        private class DataSetListResponse
        {
            public List<DataSetSummary> DataSets { get; set; }
        }
        public async Task<List<DataSetSummary>> ListDataSets(string partialName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Dictionary<string, string> parameters = null;
            if (!String.IsNullOrEmpty(partialName))
            {
                parameters = new Dictionary<string, string> { { "partialName", partialName } };
            }
            var result = await apiConnection.Get<DataSetListResponse>("data", parameters, httpMessageTransformer, cancellationToken);
            return result?.DataSets;
        }

        public Task<DataSetData> GetDataSet(string dataSetName)
        {
            return GetDataSet(dataSetName, 0, 100, null);
        }

        public async Task<DataSetData> GetDataSet(string dataSetName, int pageNumber, int pageSize, IEnumerable<string> includeColumns)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));

            var parameters = new Dictionary<string, string>
            {
                { "page", pageNumber.ToString() },
                { "pageSize", pageSize.ToString() },
            };

            return await apiConnection.Get<DataSetData>($"data/{dataSetName}", parameters, null, CancellationToken.None).ConfigureAwait(false);
        }

        public Task<DataSetData> GetDataSet(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate,
            IEnumerable<string> includeColumns)
        {
            return GetDataSet(dataSetName, pageNumber, pageSize, startDate, endDate, includeColumns, null);
        }

        public Task<DataSetData> GetDataSet(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate,
            IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return GetDataSet(dataSetName, pageNumber, pageSize, startDate, endDate, includeColumns, httpMessageTransformer, CancellationToken.None);
        }

        public Task<DataSetData> GetDataSet(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate, DateTimeOffset endDate,
            IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));

            var parameters = new Dictionary<string, string>
            {
                { "page", pageNumber.ToString() },
                { "pageSize", pageSize.ToString() },
                { "startDate", startDate.ToString("O") },
                { "endDate", endDate.ToString("O") },
            };
            var includes = includeColumns.Select(ic => new KeyValuePair<string, string>("include", ic));

            return apiConnection.Get<DataSetData>($"data/{dataSetName}", parameters.Union(includes).ToList(), httpMessageTransformer, cancellationToken);
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
