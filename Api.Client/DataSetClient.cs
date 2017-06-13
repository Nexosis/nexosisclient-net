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

        public Task<DataSetSummary> CreateDataSet(string dataSetName, IEnumerable<DataSetRow> data)
        {
            return CreateDataSet(dataSetName, data, null);
        }

        public Task<DataSetSummary> CreateDataSet(string dataSetName, IEnumerable<DataSetRow> data, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return CreateDataSet(dataSetName, data, httpMessageTransformer, CancellationToken.None);
        }

        public async Task<DataSetSummary> CreateDataSet(string dataSetName, IEnumerable<DataSetRow> data, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            return await apiConnection.Put<DataSetSummary>($"data/{dataSetName}", null, new { data }, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }

        public Task<DataSetSummary> CreateDataSet(string dataSetName, StreamReader input)
        {
            return CreateDataSet(dataSetName, input, null);
        }

        public Task<DataSetSummary> CreateDataSet(string dataSetName, StreamReader input, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return CreateDataSet(dataSetName, input, httpMessageTransformer, CancellationToken.None);
        }

        public async Task<DataSetSummary> CreateDataSet(string dataSetName, StreamReader input, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            return await apiConnection.Put<DataSetSummary>($"data/{dataSetName}", null, input, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
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
            return GetDataSet(dataSetName, 0, 100, new string[]{});
        }

        public async Task<DataSetData> GetDataSet(string dataSetName, int pageNumber, int pageSize, IEnumerable<string> includeColumns)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));
            Argument.IsNotNull(includeColumns, nameof(includeColumns));

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
            var parameters = ProcessDataSetGetParameters(dataSetName, pageNumber, pageSize, startDate, endDate, includeColumns);

            return apiConnection.Get<DataSetData>($"data/{dataSetName}", parameters, httpMessageTransformer, cancellationToken);
        }

        public async Task GetDataSet(string dataSetName, StreamWriter output)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));
            Argument.IsNotNull(output, nameof(output));

            await apiConnection.Get($"data/{dataSetName}", null, null, CancellationToken.None, output, "text/csv").ConfigureAwait(false);
        }

        public async Task GetDataSet(string dataSetName, StreamWriter output, int pageNumber, int pageSize, IEnumerable<string> includeColumns)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));
            Argument.IsNotNull(includeColumns, nameof(includeColumns));
            Argument.IsNotNull(output, nameof(output));

            var parameters = new Dictionary<string, string>
            {
                { "page", pageNumber.ToString() },
                { "pageSize", pageSize.ToString() },
            };
            var includes = includeColumns.Select(ic => new KeyValuePair<string, string>("include", ic));

            await apiConnection.Get($"data/{dataSetName}", parameters.Union(includes), null, CancellationToken.None, output, "text/csv").ConfigureAwait(false);
        }

        public async Task GetDataSet(string dataSetName, StreamWriter output, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns)
        {
            await GetDataSet(dataSetName, output, pageNumber, pageSize, startDate, endDate, includeColumns, null).ConfigureAwait(false);
        }

        public async Task GetDataSet(string dataSetName, StreamWriter output, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            await GetDataSet(dataSetName, output, pageNumber, pageSize, startDate, endDate, includeColumns, httpMessageTransformer, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task GetDataSet(string dataSetName, StreamWriter output, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            Argument.IsNotNull(output, nameof(output));

            var parameters = ProcessDataSetGetParameters(dataSetName, pageNumber, pageSize, startDate, endDate, includeColumns);

            await apiConnection.Get($"data/{dataSetName}", parameters, httpMessageTransformer, cancellationToken, output, "text/csv").ConfigureAwait(false);
        }

        public async Task RemoveDataSet(string dataSetName, DataSetDeleteOptions options)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));

            var parameters = new List<KeyValuePair<string, string>>();
            if ((options & DataSetDeleteOptions.CascadeForecast) != 0) parameters.Add(new KeyValuePair<string, string>("cascade", "forecast"));
            if ((options & DataSetDeleteOptions.CascadeSessions) != 0) parameters.Add(new KeyValuePair<string, string>("cascade", "sessions"));

            await apiConnection.Delete($"data/{dataSetName}", parameters, null, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task RemoveDataSet(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, DataSetDeleteOptions options)
        {
            await RemoveDataSet(dataSetName, startDate, endDate, options, null).ConfigureAwait(false);
        }

        public async Task RemoveDataSet(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, DataSetDeleteOptions options,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            await RemoveDataSet(dataSetName, startDate, endDate, options, httpMessageTransformer, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task RemoveDataSet(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, DataSetDeleteOptions options,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));

            var parameters = new Dictionary<string, string>()
            {
                { "startDate", startDate.ToString("O") },
                { "endDate", endDate.ToString("O") },
            }.ToList();
            if ((options & DataSetDeleteOptions.CascadeForecast) != 0) parameters.Add(new KeyValuePair<string, string>("cascade", "forecast"));
            if ((options & DataSetDeleteOptions.CascadeSessions) != 0) parameters.Add(new KeyValuePair<string, string>("cascade", "sessions"));

            await apiConnection.Delete($"data/{dataSetName}", parameters, httpMessageTransformer, cancellationToken) .ConfigureAwait(false);
        }

        public Task<DataSetData> GetDataSetForecast(string dataSetName)
        {
            return GetDataSetForecast(dataSetName, 0, 100, new string[] { });
        }

        public async Task<DataSetData> GetDataSetForecast(string dataSetName, int pageNumber, int pageSize, IEnumerable<string> includeColumns)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));
            Argument.IsNotNull(includeColumns, nameof(includeColumns));

            var parameters = new Dictionary<string, string>
            {
                { "page", pageNumber.ToString() },
                { "pageSize", pageSize.ToString() }, 
            };
            var includes = includeColumns.Select(ic => new KeyValuePair<string, string>("include", ic));
            
            return await apiConnection.Get<DataSetData>($"data/{dataSetName}/forecast", parameters.Union(includes), null, CancellationToken.None).ConfigureAwait(false);
        }

        public Task<DataSetData> GetDataSetForecast(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate, int pageNumber,
            int pageSize, IEnumerable<string> includeColumns)
        {
            return GetDataSetForecast(dataSetName, pageNumber, pageSize, startDate, endDate, includeColumns, null);
        }

        public Task<DataSetData> GetDataSetForecast(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return GetDataSetForecast(dataSetName, pageNumber, pageSize, startDate, endDate, includeColumns, httpMessageTransformer, CancellationToken.None);
        }

        public async Task<DataSetData> GetDataSetForecast(string dataSetName, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            var parameters = ProcessDataSetGetParameters(dataSetName, pageNumber, pageSize, startDate, endDate, includeColumns);
            
            return await apiConnection.Get<DataSetData>($"data/{dataSetName}/forecast", parameters, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }

        public Task GetDataSetForecast(string dataSetName, StreamWriter output)
        {
            return GetDataSetForecast(dataSetName, output, 0, 100, new string[] {});
        }

        public async Task GetDataSetForecast(string dataSetName, StreamWriter output, int pageNumber, int pageSize, IEnumerable<string> includeColumns)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));
            Argument.IsNotNull(includeColumns, nameof(includeColumns));

            var parameters = new Dictionary<string, string>
            {
                { "page", pageNumber.ToString() },
                { "pageSize", pageSize.ToString() },
            };
            var includes = includeColumns.Select(ic => new KeyValuePair<string, string>("include", ic));

            await apiConnection.Get($"data/{dataSetName}/forecast", parameters.Union(includes), null, CancellationToken.None, output, "text/csv").ConfigureAwait(false);
        }

        public Task GetDataSetForecast(string dataSetName, StreamWriter output, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns)
        {
            return GetDataSetForecast(dataSetName, output, pageNumber, pageSize, startDate, endDate, includeColumns, null);
        }

        public Task GetDataSetForecast(string dataSetName, StreamWriter output, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return GetDataSetForecast(dataSetName, output, pageNumber, pageSize, startDate, endDate, includeColumns, httpMessageTransformer, CancellationToken.None);
        }

        public async Task GetDataSetForecast(string dataSetName, StreamWriter output, int pageNumber, int pageSize, DateTimeOffset startDate,
            DateTimeOffset endDate, IEnumerable<string> includeColumns, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            Argument.IsNotNull(output, nameof(output));

            var parameters = ProcessDataSetGetParameters(dataSetName, pageNumber, pageSize, startDate, endDate, includeColumns);

            await apiConnection.Get($"data/{dataSetName}/forecast", parameters, httpMessageTransformer, cancellationToken, output, "text/csv").ConfigureAwait(false);
        }

        private static IEnumerable<KeyValuePair<string, string>> ProcessDataSetGetParameters(string dataSetName, int pageNumber, int pageSize,
            DateTimeOffset startDate, DateTimeOffset endDate, IEnumerable<string> includeColumns)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));
            Argument.IsNotNull(includeColumns, nameof(includeColumns));

            var parameters = new Dictionary<string, string>
            {
                { "page", pageNumber.ToString() },
                { "pageSize", pageSize.ToString() },
                { "startDate", startDate.ToString("O") },
                { "endDate", endDate.ToString("O") },
            };
            var includes = includeColumns.Select(ic => new KeyValuePair<string, string>("include", ic));
            return parameters.Union(includes);
        }

        public async Task RemoveDataSetForecast(string dataSetName)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));
            await apiConnection.Delete($"data/{dataSetName}", null, null, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task RemoveDataSetForecast(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));
            var parameters = new Dictionary<string, string>
            {
                { "startDate", startDate.ToString("O") },
                { "endDate", endDate.ToString("O") },
            };
            await apiConnection.Delete($"data/{dataSetName}", parameters, null, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task RemoveDataSetForecast(string dataSetName, DateTimeOffset startDate, DateTimeOffset endDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));
            var parameters = new Dictionary<string, string>
            {
                { "startDate", startDate.ToString("O") },
                { "endDate", endDate.ToString("O") },
            };
            await apiConnection.Delete($"data/{dataSetName}", parameters, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }

        public Task<List<ForecastModel>> ListDataSetForecastModels(string dataSetName)
        {
            return ListDataSetForecastModels(dataSetName, null);
        }

        public Task<List<ForecastModel>> ListDataSetForecastModels(string dataSetName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return ListDataSetForecastModels(dataSetName, httpMessageTransformer, CancellationToken.None);
        }

        private class ForecastModelList
        {
            public List<ForecastModel> Items { get; set; }
        }
        public async Task<List<ForecastModel>> ListDataSetForecastModels(string dataSetName, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));

            var result = await apiConnection
                .Get<ForecastModelList>($"data/{dataSetName}/forecast/model", null, httpMessageTransformer, cancellationToken).ConfigureAwait(false);

            return result?.Items;
        }

        public Task<ForecastModel> GetDataSetForecastModel(string dataSetName, string targetColumn)
        {
            return GetDataSetForecastModel(dataSetName, targetColumn, null);
        }

        public Task<ForecastModel> GetDataSetForecastModel(string dataSetName, string targetColumn, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return GetDataSetForecastModel(dataSetName, targetColumn, httpMessageTransformer, CancellationToken.None);
        }

        public async Task<ForecastModel> GetDataSetForecastModel(string dataSetName, string targetColumn, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            Argument.IsNotNullOrEmpty(dataSetName, nameof(dataSetName));
            Argument.IsNotNullOrEmpty(targetColumn, nameof(targetColumn));

            return await apiConnection.Get<ForecastModel>($"data/{dataSetName}/forecast/model/{targetColumn}", null, httpMessageTransformer, cancellationToken).ConfigureAwait(false);
        }

    }
}
