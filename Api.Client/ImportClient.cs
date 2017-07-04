using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{
    public class ImportClient : IImportClient
    {
        private readonly ApiConnection apiConnection;

        public ImportClient(ApiConnection apiConnection)
        {
            this.apiConnection = apiConnection;
        }
        
        public async Task<List<ImportDetail>> List()
        {
            return await ListInternal();
        }

        // we are returning just the list object, so we need a wrapper here and will then pull the results off of it.
        private class ImportsList
        {
            [JsonProperty("items", Required = Required.Always)]
            public List<ImportDetail> items { get; set; }
        }

        //we expose these inputs as method parameters, but need a class to post to as the http message body
        private class ImportFromS3Request
        {
            public string DataSetName { get; set; }
            public string Bucket { get; set; }
            public string Path { get; set; }
            public string Region { get; set; }
            public Dictionary<string, ColumnMetadata> Columns { get; set; }
        }

        private async Task<List<ImportDetail>> ListInternal(string dataSetName = null, 
            DateTimeOffset? requestedAfterDate = null,
            DateTimeOffset? requestedBeforeDate = null,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer = null,
            CancellationToken? cancellationToken = null)
        {
            var parameters = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(dataSetName))
            {
                parameters.Add(nameof(dataSetName), dataSetName);
            }
            if (requestedAfterDate != null)
            {
                parameters.Add(nameof(requestedAfterDate), requestedAfterDate.Value.ToString("O"));
            }
            if (requestedBeforeDate != null)
            {
                parameters.Add(nameof(requestedBeforeDate), requestedBeforeDate.Value.ToString("O"));
            }

            var list = await apiConnection.Get<ImportsList>("imports", parameters, httpMessageTransformer,
                cancellationToken ?? CancellationToken.None).ConfigureAwait(false);

            return (list?.items ?? Enumerable.Empty<ImportDetail>()).ToList();
        }

        public async Task<List<ImportDetail>> List(string dataSetName)
        {
            return await ListInternal(dataSetName: dataSetName).ConfigureAwait(false);
        }

        public async Task<List<ImportDetail>> List(string dataSetName, DateTimeOffset requestedAfterDate,
            DateTimeOffset requestedBeforeDate)
        {
            return await ListInternal(dataSetName: dataSetName,
                requestedAfterDate: requestedAfterDate,
                requestedBeforeDate: requestedBeforeDate).ConfigureAwait(false);
        }

        public async Task<List<ImportDetail>> List(string dataSetName, DateTimeOffset requestedAfterDate,
            DateTimeOffset requestedBeforeDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer)
        {
            return await ListInternal(dataSetName: dataSetName,
                requestedAfterDate: requestedAfterDate,
                requestedBeforeDate: requestedBeforeDate,
                httpMessageTransformer: httpMessageTransformer)
                .ConfigureAwait(false);
        }

        public async Task<List<ImportDetail>> List(string dataSetName, DateTimeOffset requestedAfterDate,
            DateTimeOffset requestedBeforeDate,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            return await ListInternal(dataSetName: dataSetName,
                requestedAfterDate: requestedAfterDate,
                requestedBeforeDate: requestedBeforeDate,
                httpMessageTransformer: httpMessageTransformer,
                cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        

        public async Task<ImportDetail> Get(Guid id)
        {
            return await Get(id, null, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<ImportDetail> Get(Guid id, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            return await apiConnection.Get<ImportDetail>($"imports/{id}", null, httpMessageTransformer, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<ImportDetail> ImportFromS3(string dataSetName, string bucket, string path, string region)
        {
            return await ImportFromS3(dataSetName, bucket, path, region, null).ConfigureAwait(false);
        }

        public async Task<ImportDetail> ImportFromS3(string dataSetName, string bucket, string path, string region, Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer,
            CancellationToken cancellationToken)
        {
            return await ImportFromS3(dataSetName, bucket, path, region, null, httpMessageTransformer,
                cancellationToken).ConfigureAwait(false);
        }

        public async Task<ImportDetail> ImportFromS3(string dataSetName, string bucket, string path, string region,
            Dictionary<string, ColumnMetadata> columns)
        {
            return await ImportFromS3(dataSetName, bucket, path, region, columns, null, CancellationToken.None)
                .ConfigureAwait(false);
        }

        public async Task<ImportDetail> ImportFromS3(string dataSetName, string bucket, string path, string region, Dictionary<string, ColumnMetadata> columns,
            Action<HttpRequestMessage, HttpResponseMessage> httpMessageTransformer, CancellationToken cancellationToken)
        {
            var request = new ImportFromS3Request()
            {
                DataSetName = dataSetName,
                Bucket = bucket,
                Path = path,
                Region = region,
                Columns = columns
            };

            var response = await apiConnection.Post<ImportDetail>("imports/s3", null, request, httpMessageTransformer,
                cancellationToken).ConfigureAwait(false);
            return response;
        }

        
    }
}