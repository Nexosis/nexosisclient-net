using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.ImportTests
{
    public class UrlImportTests : NexosisClient_TestsBase
    {

        public UrlImportTests() : base(new { }) { }

        [Fact]
        public async Task PostsToCorrectEndpoint()
        {
            var response = await target.Imports.ImportFromUrl(new ImportFromUrlRequest()
            {
                DataSetName = "my-data-set",
                Url = "foo"
            });

            Assert.Equal(HttpMethod.Post, handler.Request.Method);
            Assert.Equal(new Uri(@"https://nada.nexosis.com/imports/url"), this.handler.Request.RequestUri);
        }

        [Fact]
        public async Task PassesCorrectParametersInBody()
        {
            var expectedResponse = new
            {
                Url="foo",
                Auth = new
                {
                    Basic = new
                    {
                        UserId = "user",
                        Password = "password"
                    }
                },
                DataSetName = "my-data-set",
                Columns = new Dictionary<string, ColumnMetadata>()
                {
                    {"foo", new ColumnMetadata() {DataType = ColumnType.Numeric}}
                },
                ContentType = ImportContentType.Json,
            };

            var response =
                await target.Imports.ImportFromUrl(new ImportFromUrlRequest()
                {
                    Url = "foo",
                    Auth = new ImportFromUrlAuthentication()
                    {
                        Basic = new BasicAuthentication()
                        {
                            UserId = "user",
                            Password = "password"
                        }
                    },
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