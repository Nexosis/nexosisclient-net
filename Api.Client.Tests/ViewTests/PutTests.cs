﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nexosis.Api.Client;
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

        [Fact]
        public async Task CreatesWithCalendarName()
        {
            var actual = await target.Views.Create("TestCalendarView", new ViewInfo()
            {
                DataSetName = "TestDataSet",
                Joins = new JoinMetadata[]
                {
                   new JoinMetadata() {Calendar = new CalendarJoinSource {Name = "Nexosis.Holidays-US", TimeZone = "america/new_york"}}
                }
            });

            Assert.Equal(HttpMethod.Put, handler.Request.Method);
            var expected = "{\"DataSetName\":\"TestDataSet\",\"Columns\":{},\"Joins\":[{\"Calendar\":{\"Name\":\"Nexosis.Holidays-US\",\"TimeZone\":\"america/new_york\"},\"ColumnOptions\":{}}]}";
            Assert.Equal(expected, handler.RequestBody);
        }

        [Fact]
        public async Task CreatesWithCalendarUrl()
        {

            var actual = await target.Views.Create("TestCalendarView", new ViewInfo()
            {
                DataSetName = "TestDataSet",
                Joins = new JoinMetadata[]
                {
                    new JoinMetadata()
                    {
                        Calendar = new CalendarJoinSource {Url = "http://example.com/mycalendar.ical", TimeZone = "america/new_york"}
                    }
                }
            });
            
            Assert.Equal(HttpMethod.Put, handler.Request.Method);
            var expected = "{\"DataSetName\":\"TestDataSet\",\"Columns\":{},\"Joins\":[{\"Calendar\":{\"Url\":\"http://example.com/mycalendar.ical\",\"TimeZone\":\"america/new_york\"},\"ColumnOptions\":{}}]}";
            Assert.Equal(expected, handler.RequestBody);
        }
    }
}
