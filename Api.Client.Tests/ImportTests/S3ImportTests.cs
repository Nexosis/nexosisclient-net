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
            var response = await target.Imports.ImportFromS3("my-data-set", "nexosis-sample-data", "LocationA.csv", "us-east-1");
            Assert.Equal(HttpMethod.Post, handler.Request.Method);
            Assert.Equal(new Uri(@"https://nada.nexosis.com/imports/s3"), this.handler.Request.RequestUri);
        }

        [Fact]
        public async Task PassesCorrectParametersInBody()
        {
            var expectedResponse = new
            {
                DataSetName = "my-data-set",
                Bucket = "nexosis-sample-data",
                Path = "LocationA.csv",
                Region = "us-east-1",
                Columns = new Dictionary<string, ColumnMetadata>() {{"foo", new ColumnMetadata() {DataType = ColumnType.Numeric}}}
            };
            
            var response =
                await target.Imports.ImportFromS3("my-data-set", "nexosis-sample-data", "LocationA.csv", "us-east-1", new Dictionary<string, ColumnMetadata>() {{"foo", new ColumnMetadata(){DataType = ColumnType.Numeric}} });
            
            Assert.Equal(JsonConvert.SerializeObject(expectedResponse), this.handler.RequestBody);
        }
    }
}