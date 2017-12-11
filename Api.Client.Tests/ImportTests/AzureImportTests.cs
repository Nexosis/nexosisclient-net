using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.ImportTests
{
    public class AzureImportTests : NexosisClient_TestsBase
    {

        public AzureImportTests() : base(new { }) { }

        [Fact]
        public async Task PostsToCorrectEndpoint()
        {
            var response = await target.Imports.ImportFromAzure(new ImportFromAzureRequest()
            {
                DataSetName = "my-data-set",
            });

            Assert.Equal(HttpMethod.Post, handler.Request.Method);
            Assert.Equal(new Uri(@"https://nada.nexosis.com/imports/azure"), this.handler.Request.RequestUri);
        }

        [Fact]
        public async Task PassesCorrectParametersInBody()
        {
            var expectedResponse = new
            {
                ConnectionString = "Foo",
                Container = "Bar",
                Blob = "Baz",
                DataSetName = "my-data-set",
                Columns = new Dictionary<string, ColumnMetadata>()
                {
                    {"foo", new ColumnMetadata() {DataType = ColumnType.Numeric}}
                },
                ContentType = ImportContentType.Json,
            };

            var response =
                await target.Imports.ImportFromAzure(new ImportFromAzureRequest()
                {
                    ConnectionString = "Foo",
                    Container = "Bar",
                    Blob = "Baz",
                    DataSetName = "my-data-set",
                    ContentType = ImportContentType.Json,
                    Columns = new Dictionary<string, ColumnMetadata>()
                    {
                        {"foo", new ColumnMetadata() {DataType = ColumnType.Numeric}}
                    }
                });

            Assert.Equal(JsonConvert.SerializeObject(expectedResponse), this.handler.RequestBody);
        }
    }
}