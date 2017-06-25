using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.DataSetTests
{
    public class SaveTests : NexosisClient_TestsBase
    {
        public SaveTests() : base(new { })
        {

        }

        [Fact]
        public async Task RequiresDataSetNameToBeGiven()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.DataSets.Create((string)null, (DataSet)null));

            Assert.Equal(exception.ParamName, "dataSetName");
        }

        [Fact]
        public async Task RequiresDataSetListToBeGiven()
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await target.DataSets.Create("foxtrot", (DataSet)null));

            Assert.Equal(exception.ParamName, "data");
        }

        [Fact]
        public async Task RequiresFileToBeGiven()
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await target.DataSets.Create("whiskey", (StreamReader)null));

            Assert.Equal(exception.ParamName, "input");
        }

        [Fact]
        public async Task WillUploadDataGivenAsFile()
        {
            string fileContent = "timeStamp,alpha,beta\r\n2017-01-01,10,14\r\n2017-01-02,11,13\r\n2017-01-03,12,12";

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent)))
            {
                using (var input = new StreamReader(stream, Encoding.UTF8, false, 1024, leaveOpen: true)) 
                {
                    await target.DataSets.Create("tango", input);
                }

                Assert.Equal(HttpMethod.Put, handler.Request.Method);
                Assert.Equal(new Uri(baseUri, "data/tango"), handler.Request.RequestUri);

                Assert.Equal(fileContent, handler.RequestBody);
            }
        }

        [Fact]
        public async Task WillSaveDataGivenDirectly()
        {
            var data = DataSetGenerator.Run(DateTime.Today.AddDays(-90), DateTime.Today, "something");
            var result = await target.DataSets.Create("yankee", data);

            Assert.Equal(HttpMethod.Put, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "data/yankee"), handler.Request.RequestUri);
            Assert.Equal(JsonConvert.SerializeObject(data), handler.RequestBody);
        }

    }
}
