using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.ViewTests
{
    public class PutTests : NexosisClient_TestsBase
    {
        public PutTests() : base(new { })
        {

        }

        [Fact]
        public async Task RequiresViewNameToBeGiven()
        {
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await target.Views.Create((string)null, (ViewInfo)null));

            Assert.Equal(exception.ParamName, "viewName");
        }

        [Fact]
        public async Task RequiresViewDefinitionToBeGiven()
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await target.Views.Create("myview", (ViewInfo)null));

            Assert.Equal(exception.ParamName, "view");
        }
        

        [Fact]
        public async Task WillSaveViewGiven()
        {
            var view = new ViewInfo()
            {
                DataSetName = "foo",
                Columns = new Dictionary<string, ColumnMetadata>()
                {
                    ["timestamp"] = new ColumnMetadata() {DataType = ColumnType.Date, Role = ColumnRole.Timestamp}
                },
                Joins = new[]
                {
                    new JoinMetadata()
                    {
                        DataSet = new DataSetJoinSource() {Name = "bar"},
                        ColumnOptions = new Dictionary<string, JoinColumnMetadata>()
                        {
                            ["timeStamp"] = new JoinColumnMetadata() {JoinInterval = ResultInterval.Hour}
                        }
                    }
                }
            };
            var result = await target.Views.Create("myview", view);

            Assert.Equal(HttpMethod.Put, handler.Request.Method);
            Assert.Equal(new Uri(baseUri, "views/myview"), handler.Request.RequestUri);
            Assert.Equal(JsonConvert.SerializeObject(view), handler.RequestBody);
        }

    }
}
