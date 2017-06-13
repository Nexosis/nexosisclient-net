using System.Linq;
using Nexosis.Api.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests
{
    public class SessionIntegrationTests
    {
        private readonly string productFilePath = Path.Combine(new DirectoryInfo(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, @"CsvFiles\producttest.csv");
        private NexosisClient target;

        public SessionIntegrationTests()
        {
            target = new NexosisClient("249c5810fd58403c9e2bfeae423a72fd");
        }

        [Fact]
        public async Task GetBalanceWillGiveItBack()
        {
            var actual = await target.GetAccountBalance();
            Assert.NotNull(actual.Balance);
            Assert.Equal(0, actual.Cost.Amount);
            Assert.Equal("USD", actual.Balance.Currency);
        }

        [Fact]
        public async Task CreateForecastWithCsvStartsNewSession()
        {  
            using (var file = File.OpenText(productFilePath))
            {
                var actual = await target.Sessions.CreateForecastSession(file, "sales", DateTimeOffset.Parse("2017-03-25 -0:00"), DateTimeOffset.Parse("2017-04-25 -0:00"));
                Assert.NotNull(actual.SessionId);
            }
        }

        [Fact]
        public async Task CreateForecastWithDataDirectlyStartsNewSession()
        {
            var dataSet = GenerateDataSet(DateTime.Parse("2016-08-01"), DateTime.Parse("2017-03-26"), "instances");
            var actual = await target.Sessions.CreateForecastSession(dataSet, "instances", DateTimeOffset.Parse("2017-03-26"), DateTimeOffset.Parse("2017-04-25") );
            Assert.NotNull(actual.SessionId);
        }

        // TODO: uncomment when data set save is implemented 
        /* [Fact]
        public async Task ForcastFromSavedDataSetStartsNewSession()
        {
            var dataSetName = $"testDataSet-{DateTime.Now:s}";
            var dataSet = GenerateDataSet(DateTime.Parse("2016-08-01"), DateTime.Parse("2017-03-26"), "instances");
            await target.DataSets.CreateDataSet(dataSetName, dataSet);

            var actual = await target.Sessions.CreateForecastSession(dataSetName, "instances", DateTimeOffset.Parse("2017-03-26"), DateTimeOffset.Parse("2017-04-25") );
            Assert.NotNull(actual.SessionId);
        } */

        [Fact]
        public async Task StartImpactWithCsvStartsNewSession()
        {  
            using (var file = File.OpenText(productFilePath))
            {
                var actual = await target.Sessions.CreateImpactSession(file, "super-duper-sale", "sales", DateTimeOffset.Parse("2016-11-25 -0:00"), DateTimeOffset.Parse("2016-12-25 -0:00"));
                Assert.NotNull(actual.SessionId);
            }
        }

        [Fact]
        public async Task StartImpactWithDataDirectlyStartsNewSession()
        {
            var dataSet = GenerateDataSet(DateTime.Parse("2016-08-01"), DateTime.Parse("2017-03-26"), "instances");
            var actual = await target.Sessions.CreateImpactSession(dataSet, $"charlie-delta-{DateTime.UtcNow:s}", "instances", DateTimeOffset.Parse("2016-11-26"), DateTimeOffset.Parse("2016-12-25") );
            Assert.NotNull(actual.SessionId);
        }

        // TODO: uncomment when data set save is implemented 
        /* [Fact]
        public async Task StartImpactFromSavedDataSetStartsNewSession()
        {
            var dataSetName = $"testDataSet-{DateTime.UtcNow:s}";
            var dataSet = GenerateDataSet(DateTime.Parse("2016-08-01"), DateTime.Parse("2017-03-26"), "instances");
            await target.DataSets.CreateDataSet(dataSetName, dataSet);

            var actual = await target.Sessions.CreateForecastSession(dataSetName, "instances", DateTimeOffset.Parse("2017-03-26"), DateTimeOffset.Parse("2017-04-25") );
            Assert.NotNull(actual.SessionId);
        } */

        [Fact]
        public async Task GetSessionListHasItems()
        {
            var sessions = await target.Sessions.ListSessions();
            Assert.True(sessions.Count > 0);
        }

        [Fact]
        public async Task GetSessionResultsHasResults()
        {
            var sessions = await target.Sessions.ListSessions();
            var session = sessions.FirstOrDefault(s => s.Status == SessionStatus.Completed);
            var results = await target.Sessions.GetSessionResults(session.SessionId);

            Assert.NotNull(results);
            Assert.True(results.Data.Count > 0);
        }

        [Fact]
        public async Task GetSessionResultsWillWriteFile()
        {
            var sessions = await target.Sessions.ListSessions();
            var session = sessions.FirstOrDefault(s => s.Status == SessionStatus.Completed);

            var filename = Path.Combine(AppContext.BaseDirectory, $"test-ouput-{DateTime.UtcNow:yyyyMMddhhmmss}.csv");
            try
            {
                using (var output = new StreamWriter(File.OpenWrite(filename)))
                {
                    await target.Sessions.GetSessionResults(session.SessionId, output);
                }

                var results = File.ReadAllText(filename);

                Assert.True(results.Length > 0);
                Assert.StartsWith("timestamp,", results);
            }
            finally
            {
                if (File.Exists(filename))
                    File.Delete(filename);
            }
        }

        [Fact]
        public async Task DeletingSessionThenQueryingReturns404()
        {
            var dataSet = GenerateDataSet(DateTime.Parse("2016-08-01"), DateTime.Parse("2017-03-26"), "instances");
            var actual = await target.Sessions.CreateImpactSession(dataSet, $"charlie-delta-{DateTime.UtcNow:s}", "instances", DateTimeOffset.Parse("2016-11-26"), DateTimeOffset.Parse("2016-12-25") );

            await target.Sessions.RemoveSession(actual.SessionId);
            var exception = await Assert.ThrowsAsync<NexosisClientException>(async () => await target.Sessions.GetSession(actual.SessionId));

            Assert.Equal(exception.StatusCode, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CheckingSessionStatusReturnsExpcetedValue()
        {
            var dataSet = GenerateDataSet(DateTime.Parse("2016-08-01"), DateTime.Parse("2017-03-26"), "instances");
            var actual = await target.Sessions.EstimateImpactSession(dataSet, $"charlie-delta-{DateTime.UtcNow:s}", "instances", DateTimeOffset.Parse("2016-11-26"), DateTimeOffset.Parse("2016-12-25") );

            var status = await target.Sessions.GetSessionStatus(actual.SessionId);

            Assert.Equal(actual.Status, status.Status);
        }

        [Fact]
        public async Task CanRemoveMultipleSessions()
        {
            var dataSet = GenerateDataSet(DateTime.Parse("2016-08-01"), DateTime.Parse("2017-03-26"), "instances");
            var first = await target.Sessions.CreateImpactSession(dataSet, "juliet-juliet-echo-1", "instances", DateTimeOffset.Parse("2016-11-26"), DateTimeOffset.Parse("2016-12-25") );
            var second = await target.Sessions.CreateImpactSession(dataSet, "juliet-juliet-echo-2", "instances", DateTimeOffset.Parse("2016-11-26"), DateTimeOffset.Parse("2016-12-25") );

            await target.Sessions.RemoveSessions(null, "juliet-juliet-echo-", SessionType.Impact);

            var exceptionTheFirst = await Assert.ThrowsAsync<NexosisClientException>(async () => await target.Sessions.GetSession(first.SessionId));
            var exceptionTheSecond = await Assert.ThrowsAsync<NexosisClientException>(async () => await target.Sessions.GetSession(second.SessionId));

            Assert.Equal(exceptionTheFirst.StatusCode, HttpStatusCode.NotFound);
            Assert.Equal(exceptionTheSecond.StatusCode, HttpStatusCode.NotFound);
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
