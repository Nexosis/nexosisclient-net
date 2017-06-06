using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Nexosis.Api.Client;
using Nexosis.Api.Client.Model;

namespace Api.Client.Tests
{
    public class ClientIntegrationTests
    {
        private readonly string productFilePath = Path.Combine(new DirectoryInfo(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, @"CsvFiles\producttest.csv");

        [Fact]
        public async Task SubmitCsvStartsNewSession()
        {  
            var session = new SessionRequest { DataSetName = "FullTest", StartDate = DateTime.Parse("2017-03-26"), EndDate = DateTime.Parse("2017-04-25") };
            var target = new ApiClient();
            var actual = await target.ForecastFromCsvAsync(session, new FileInfo(productFilePath), "sales");
            Assert.NotNull(actual.Session.SessionId);
        }

        [Fact]
        public async Task SubmitDataDirectlyStartsNewSession()
        {
            var dataSet = GenerateDataSet(DateTime.Parse("2016-08-01"), DateTime.Parse("2017-03-26"), "instances");
            var session = new SessionRequest { DataSetName = "Something", StartDate = DateTime.Parse("2017-03-26"), EndDate = DateTime.Parse("2017-04-25") };
            var target = new ApiClient();
            var actual = await target.ForecastFromDataAsync(session, dataSet, "instances");
            Assert.NotNull(actual.Session.SessionId);
        }

        [Fact]
        public async Task SessionShouldAllowUserToGetResults()
        {
            var target = new ApiClient();
            var sessions = await target.GetSessions();
            var testSession = sessions.First(srd => srd.Status == SessionStatus.Completed);
            var actual = await target.GetSessionResultsAsync(testSession.SessionId.Value);
            Assert.True(actual.Count >= 0);
        }

        [Fact]
        public async Task SessionsListCanBeQueried()
        {
            var target = new ApiClient();
            var sessions = await target.GetSessions();
            Assert.True(sessions.Count > 0, "no sessions found");
        }

        [Fact]
        public async Task CostAndBalanceAreAvailableAfterRequest()
        {
            var dataSet = GenerateDataSet(DateTime.Parse("2016-08-01"), DateTime.Parse("2016-12-31"), "instances");
            var session = new SessionRequest { DataSetName = "Something", StartDate = DateTime.Parse("2017-01-01"), EndDate = DateTime.Parse("2017-01-31") };
            var target = new ApiClient();
            var result = await target.ForecastFromDataAsync(session, dataSet, "instances");

            // cannot test specific value for balance as it will change between runs right now.
            Assert.False(String.IsNullOrEmpty(result.Balance));
            Assert.NotEqual("NA", result.Balance);
            Assert.Equal("3.0 USD", result.Cost); 
        }

        [Fact]
        public async Task DataSetCanBeSubmitted()
        {
            var target = new ApiClient();
            var key = $"foo-{DateTime.UtcNow:s}";
            await target.SaveDataSetAsync(key, GenerateDataSet(DateTime.Parse("2016-01-01"), DateTime.Parse("2017-01-01"), "foos"));

            // TODO: what to assert?
        }

        [Fact]
        public async Task CanRunPredictionsOnDataSet()
        {
            var target = new ApiClient();

            var key = $"foo-{DateTime.UtcNow:s}";
            await target.SaveDataSetAsync(key, GenerateDataSet(DateTime.Parse("2016-01-01"), DateTime.Parse("2017-01-01"), "foos"));
           
            var session = new SessionRequest { DataSetName = key, StartDate = DateTime.Parse("2017-03-26"), EndDate = DateTime.Parse("2017-04-25")  };
            var sessonResponse = await target.ForecastFromSavedDataSetAsync(session, "foos");

            Assert.NotNull(sessonResponse);
            Assert.Equal(key, sessonResponse.Session.DataSetName);
            Assert.Equal("foos", sessonResponse.Session.TargetColumn);
        }

        [Fact]
        public async Task CanListAllDataSets()
        {
            var target = new ApiClient();

            var datasets = await target.ListDataSetsAsync(null);
            
            Assert.NotNull(datasets);
            Assert.True(datasets.Count > 0, "No datasets found");
        }

        [Fact]
        public async Task CanFilterDataSetList()
        {
            var target = new ApiClient();

            var datasets = await target.ListDataSetsAsync("foo-");
            
            Assert.NotNull(datasets);
            Assert.True(datasets.Count > 0, "No datasets found");
        }

        [Fact]
        public async Task WillGiveStatusBackForRunningJob()
        {
            var target = new ApiClient();
            var session = new SessionRequest { DataSetName = Guid.NewGuid().ToString(), StartDate = DateTime.Parse("2017-03-26"), EndDate = DateTime.Parse("2017-04-25") };
            var actual = await target.ForecastFromCsvAsync(session, new FileInfo(productFilePath), "sales");

            Assert.NotNull(actual.Session.SessionId);

            var status = await target.GetSessionStatusAsync(actual.Session.SessionId.Value);

            Assert.NotNull(status);
            Assert.True((int)status.Status <= (int)SessionStatus.Started);
            Assert.Equal(actual.Session.SessionId, status.SessionId);
        }

        [Fact]
        public async Task WillRunImpactSessionOnEvent()
        {
            var target = new ApiClient();
            var session = new SessionRequest { DataSetName = Guid.NewGuid().ToString(), StartDate = DateTime.Parse("2015-10-01"), EndDate = DateTime.Parse("2016-01-01") };
            var result = await target.EventImpactFromCsvAsync(session, new FileInfo(productFilePath), "super duper sale", "sales");

            Assert.NotNull(result);

            var status = await target.GetSessionStatusAsync(result.Session.SessionId.Value);

            Assert.NotNull(status);
            Assert.True((int)status.Status <= (int)SessionStatus.Started);
            Assert.Equal(result.Session.SessionId, status.SessionId);
        }

        private List<DataSetRow> GenerateDataSet(DateTime startDate, DateTime endDate, string targetKey)
        {
            var rand = new Random();
            var dates = Enumerable.Range(0, (endDate.Date - startDate.Date).Days).Select(i => startDate.Date.AddDays(i));

            return dates.Select(d => new DataSetRow
            {
                Timestamp = d,
                Values = new Dictionary<string, double> { { targetKey, rand.NextDouble() * 100 } }
            }).ToList();
        }

    }
}
