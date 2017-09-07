using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Nexosis.Api.Client;
using Nexosis.Api.Client.Model;
using Xunit;
using System.Threading;

namespace Api.Client.Tests
{
#if !SKIP_INTEGRATION // integration tests will charge your account and require an API key in environment variables
    [Collection("Integration")]
    public class SessionIntegrationTests
    {
        private readonly IntegrationTestFixture fixture;
        private readonly string sensorFilePath = Path.Combine(new DirectoryInfo(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, @"CsvFiles\sensor2.csv");

        public SessionIntegrationTests(IntegrationTestFixture fixture)
        {
            this.fixture = fixture;
            var sessions = fixture.Client.Sessions.List(0, 20).GetAwaiter().GetResult();
            var completedSession = sessions.FirstOrDefault(s => s.Status == Status.Completed);
            if (completedSession == null)
                CreateSession().GetAwaiter().GetResult();
            else
                savedSession = completedSession;
        }

        SessionResponse savedSession;
        private async Task CreateSession()
        {
            var session = await fixture.Client.Sessions.CreateForecast(fixture.ForecastDataSetName, "instances", DateTime.Parse("2016-05-01"), DateTime.Parse("2016-05-30"), ResultInterval.Day);
            while (true)
            {
                var status = await fixture.Client.Sessions.GetStatus(session.SessionId);
                if (status.Status == Status.Completed || status.Status == Status.Failed)
                    break;
                Thread.Sleep(5000);
            }
            savedSession = await fixture.Client.Sessions.Get(session.SessionId);
        }

        [Fact]
        public async Task GetBalanceWillGiveItBack()
        {
            var actual = await fixture.Client.GetAccountBalance();
            Assert.NotNull(actual.Balance);
            Assert.Equal(0, actual.Cost.Amount);
            Assert.Equal("USD", actual.Balance.Currency);
        }

        [Fact]
        public async Task ForecastStartsNewSession()
        {
            var dataSetName = $"testDataSet-{DateTime.Now:s}";
            var dataSet = DataSetGenerator.Run(DateTime.Parse("2016-08-01"), DateTime.Parse("2017-03-26"), "instances");
            await fixture.Client.DataSets.Create(dataSetName, dataSet);

            var sessionRequest = new SessionDetail()
            {
                DataSetName = dataSetName,
                Columns = new Dictionary<string, ColumnMetadata>()
                {
                    ["instances"] =
                    new ColumnMetadata() { DataType = ColumnType.NumericMeasure, Role = ColumnRole.Target }
                }
            };

            var actual = await fixture.Client.Sessions.CreateForecast(sessionRequest, DateTimeOffset.Parse("2017-03-26"), DateTimeOffset.Parse("2017-04-25"), ResultInterval.Day);
            Assert.NotNull(actual.SessionId);
            await fixture.Client.DataSets.Remove(dataSetName, DataSetDeleteOptions.CascadeAll);
        }

        [Fact]
        public async Task StartingSessionWithMeasureDatatypeTargetStartsSession()
        {
            var dataSetName = $"testDataSet-{DateTime.Now:s}";
            var dataSet = DataSetGenerator.Run(DateTime.Parse("2016-08-01"), DateTime.Parse("2017-03-26"), "instances");

            await fixture.Client.DataSets.Create(dataSetName, dataSet);



            var actual = await fixture.Client.Sessions.CreateForecast(dataSetName, "instances",
                DateTimeOffset.Parse("2017-03-26"), DateTimeOffset.Parse("2017-04-25"), ResultInterval.Day);
            Assert.NotNull(actual.SessionId);
            await fixture.Client.DataSets.Remove(dataSetName, DataSetDeleteOptions.CascadeAll);
        }

        [Fact]
        public async Task GivesBackForecastMatchingRequestedInterval()
        {
            var sessionData = new SessionDetail { DataSetName = "HourlyNetTest" };
            sessionData.Columns.Add("value", new ColumnMetadata { DataType = ColumnType.Numeric, Role = ColumnRole.Target });
            await fixture.Client.DataSets.Create(sessionData.DataSetName, new StreamReader(File.OpenRead("csvfiles/sensor2.csv")));
            var session = await fixture.Client.Sessions.CreateForecast(sessionData, DateTime.Parse("2017-01-08"), DateTime.Parse("2017-01-09"), ResultInterval.Hour);
            while (true)
            {
                var status = await fixture.Client.Sessions.GetStatus(session.SessionId);
                if (status.Status == Status.Completed)
                    break;
                Thread.Sleep(5000);
            }

            var results = await fixture.Client.Sessions.GetResults(session.SessionId);

            Assert.NotNull(results);
            var date1 = DateTimeOffset.Parse(results.Data[0]["timeStamp"]);
            var date2 = DateTimeOffset.Parse(results.Data[1]["timeStamp"]);
            Assert.Equal(1, (date2 - date1).Hours);
            await fixture.Client.DataSets.Remove(sessionData.DataSetName, DataSetDeleteOptions.CascadeAll);
        }

        [Fact]
        public async Task StartImpactStartsNewSession()
        {
            var dataSetName = $"testDataSet-{DateTime.UtcNow:s}";
            var dataSet = DataSetGenerator.Run(DateTime.Parse("2016-08-01"), DateTime.Parse("2017-03-26"), "instances");
            await fixture.Client.DataSets.Create(dataSetName, dataSet);

            var actual = await fixture.Client.Sessions.CreateForecast(dataSetName, "instances", DateTimeOffset.Parse("2017-03-26"), DateTimeOffset.Parse("2017-04-25"), ResultInterval.Day);
            Assert.NotNull(actual.SessionId);
            await fixture.Client.DataSets.Remove(dataSetName, DataSetDeleteOptions.CascadeAll);
        }

        [Fact]
        public async Task GetSessionListHasItems()
        {
            var sessions = await fixture.Client.Sessions.List(0, 1);
            Assert.True(sessions.Count > 0);
        }

        [Fact]
        public async Task GetSessionResultsHasResults()
        {
            var results = await fixture.Client.Sessions.GetResults(savedSession.SessionId);

            Assert.NotNull(results);
            Assert.Equal(savedSession.SessionId, results.SessionId);
            Assert.Equal(Status.Completed, results.Status);
        }

        [Fact]
        public async Task GetSessionResultsHasLinks()
        {
            var result = await fixture.Client.Sessions.GetResults(savedSession.SessionId);
            Assert.NotNull(result);
            Assert.Equal(2, result.Links.Count);
            Assert.Equal(new[] { "results", "data" }, result.Links.Select(l => l.Rel));
            Assert.Equal($"{fixture.Client.ConfiguredUrl}sessions/{savedSession.SessionId}/results", result.Links[0].Href);
            Assert.Equal($"{fixture.Client.ConfiguredUrl}data/{savedSession.DataSetName}", result.Links[1].Href);
        }

        [Fact]
        public async Task GetSessionResultsWillWriteFile()
        {
            var dataSets = await fixture.Client.DataSets.List(fixture.ForecastDataSetName);
            var dataSet = dataSets.First();
            var filename = Path.Combine(AppContext.BaseDirectory, $"test-ouput-{DateTime.UtcNow:yyyyMMddhhmmss}.csv");
            try
            {
                using (var output = new StreamWriter(File.OpenWrite(filename)))
                {
                    await fixture.Client.Sessions.GetResults(savedSession.SessionId, output);
                }

                var results = File.ReadAllText(filename);

                Assert.True(results.Length > 0);

                Assert.StartsWith(dataSet.Columns.First().Key, results);
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
            var dataSetName = $"testDataSet-{DateTime.Now:s}";
            var dataSet = DataSetGenerator.Run(DateTime.Parse("2016-08-01"), DateTime.Parse("2017-03-26"), "instances");

            await fixture.Client.DataSets.Create(dataSetName, dataSet);

            var actual = await fixture.Client.Sessions.AnalyzeImpact(dataSetName, $"charlie-delta-{DateTime.UtcNow:s}", "instances", DateTimeOffset.Parse("2016-11-26"), DateTimeOffset.Parse("2016-12-25"), ResultInterval.Day);

            await fixture.Client.Sessions.Remove(actual.SessionId);
            var exception = await Assert.ThrowsAsync<NexosisClientException>(async () => await fixture.Client.Sessions.Get(actual.SessionId));

            Assert.Equal(exception.StatusCode, HttpStatusCode.NotFound);
            await fixture.Client.DataSets.Remove(dataSetName, DataSetDeleteOptions.CascadeAll);
        }

        [Fact]
        public async Task CheckingSessionStatusReturnsExpectedValue()
        {
            var dataSetName = $"testDataSet-{DateTime.Now:s}";
            var dataSet = DataSetGenerator.Run(DateTime.Parse("2016-08-01"), DateTime.Parse("2017-03-26"), "instances");

            await fixture.Client.DataSets.Create(dataSetName, dataSet);

            var actual = await fixture.Client.Sessions.EstimateImpact(dataSetName, $"charlie-delta-{DateTime.UtcNow:s}", "instances", DateTimeOffset.Parse("2016-11-26"), DateTimeOffset.Parse("2016-12-25"), ResultInterval.Day);

            var status = await fixture.Client.Sessions.GetStatus(actual.SessionId);

            Assert.Equal(actual.Status, status.Status);
            await fixture.Client.DataSets.Remove(dataSetName, DataSetDeleteOptions.CascadeAll);
        }

        [Fact]
        public async Task CanRemoveMultipleSessions()
        {
            var dataSetName = $"testDataSet-{DateTime.Now:s}";
            var dataSet = DataSetGenerator.Run(DateTime.Parse("2016-08-01"), DateTime.Parse("2017-03-26"), "instances");

            await fixture.Client.DataSets.Create(dataSetName, dataSet);

            var first = await fixture.Client.Sessions.AnalyzeImpact(dataSetName, "juliet-juliet-echo-1", "instances", DateTimeOffset.Parse("2016-11-26"), DateTimeOffset.Parse("2016-12-25"), ResultInterval.Day);
            var second = await fixture.Client.Sessions.AnalyzeImpact(dataSetName, "juliet-juliet-echo-2", "instances", DateTimeOffset.Parse("2016-11-26"), DateTimeOffset.Parse("2016-12-25"), ResultInterval.Day);

            await fixture.Client.Sessions.Remove(null, "juliet-juliet-echo-", SessionType.Impact);

            var exceptionTheFirst = await Assert.ThrowsAsync<NexosisClientException>(async () => await fixture.Client.Sessions.Get(first.SessionId));
            var exceptionTheSecond = await Assert.ThrowsAsync<NexosisClientException>(async () => await fixture.Client.Sessions.Get(second.SessionId));

            Assert.Equal(exceptionTheFirst.StatusCode, HttpStatusCode.NotFound);
            Assert.Equal(exceptionTheSecond.StatusCode, HttpStatusCode.NotFound);
            await fixture.Client.DataSets.Remove(dataSetName, DataSetDeleteOptions.CascadeAll);
        }
    }
#endif
}
