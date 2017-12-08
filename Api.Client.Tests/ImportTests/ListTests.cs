using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.ImportTests
{
    public class ListTests : NexosisClient_TestsBase
    {

        public ListTests() : base(new {Items = new object[] {}})
        {
            
        }

        [Fact]
        public async Task GetsFromCorrectUrl()
        {
            var result = await target.Imports.List();
            Assert.Equal(HttpMethod.Get, handler.Request.Method);
            Assert.Equal(new Uri(@"https://nada.nexosis.com/imports"), handler.Request.RequestUri);
        }

        [Fact]
        public async Task IncludesParametersCorrectly()
        {
            var result = await target.Imports.List(new ImportDetailQuery()
            {
                DataSetName = "foo",
                RequestedAfterDate = DateTimeOffset.Parse("2017-01-01 0:00 -0:00"),
                RequestedBeforeDate = DateTimeOffset.Parse("2017-01-02 0:00 -0:00"),
            });

            Assert.Equal(new Uri(@"https://nada.nexosis.com/imports?dataSetName=foo&requestedAfterDate=2017-01-01T00:00:00.0000000%2B00:00&requestedBeforeDate=2017-01-02T00:00:00.0000000%2B00:00"), handler.Request.RequestUri);
        }
    }
}