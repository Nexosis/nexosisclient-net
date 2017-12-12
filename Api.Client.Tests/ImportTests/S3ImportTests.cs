using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.ImportTests
{
    public class S3ImportTests : NexosisClient_TestsBase
    {
 
        public S3ImportTests() : base(new { })
        {
            
        }

        [Fact]
        public async Task PostsToCorrectEndpoint()
        {
            var response = await target.Imports.ImportFromS3(new ImportFromS3Request
            {
                DataSetName = "my-data-set",
                Bucket = "nexosis-sample-data",
                Path = "LocationA.csv",
                Region = "us-east-1"
            });
            
            Assert.Equal(HttpMethod.Post, handler.Request.Method);
            Assert.Equal(new Uri(@"https://nada.nexosis.com/imports/s3"), this.handler.Request.RequestUri);
        }

        [Fact]
        public async Task PassesCorrectParametersInBody()
        {
            var expectedResponse = new
            {
                Bucket = "nexosis-sample-data",
                Path = "LocationA.csv",
                Region = "us-east-1",
                AccessKeyId = "my-access-key",
                SecretAccessKey = "my-secret-key",
                DataSetName = "my-data-set",
                Columns = new Dictionary<string, ColumnMetadata>() {{"foo", new ColumnMetadata() {DataType = ColumnType.Numeric}}},
                ContentType = ImportContentType.Json,
            };

            var response =
                await target.Imports.ImportFromS3(new ImportFromS3Request
                {
                    DataSetName = "my-data-set",
                    Bucket = "nexosis-sample-data",
                    Path = "LocationA.csv",
                    Region = "us-east-1",
                    ContentType = ImportContentType.Json,
                    AccessKeyId = "my-access-key",
                    SecretAccessKey = "my-secret-key",
                    Columns = new Dictionary<string, ColumnMetadata>()
                    {
                        {"foo", new ColumnMetadata() {DataType = ColumnType.Numeric}}
                    }
                });
            
            Assert.Equal(JsonConvert.SerializeObject(expectedResponse), this.handler.RequestBody);
        }
    }
}