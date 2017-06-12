﻿using System.Linq;
using Nexosis.Api.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Api.Client.Tests
{
    public class ClientIntegrationTests
    {
        private readonly string productFilePath = Path.Combine(new DirectoryInfo(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, @"CsvFiles\producttest.csv");

        [Fact]
        public async Task SubmitCsvStartsNewSession()
        {  
            using (var file = File.OpenText(productFilePath))
            {
                var actual = await target.Sessions.CreateForecastSession(file, "sales", DateTimeOffset.Parse("2017-03-25 -0:00"), DateTimeOffset.Parse("2017-04-25 -0:00"));
                Assert.NotNull(actual.SessionId);
            }
        }

        [Fact]
        public async Task SubmitDataDirectlyStartsNewSession()
        {
            var dataSet = GenerateDataSet(DateTime.Parse("2016-08-01"), DateTime.Parse("2017-03-26"), "instances");
            var actual = await target.Sessions.CreateForecastSession(dataSet, "instances", DateTimeOffset.Parse("2017-03-26"), DateTimeOffset.Parse("2017-04-25") );
            var actual = await target.CreateForecastSession(dataSet, "instances", DateTimeOffset.Parse("2017-03-26"), DateTimeOffset.Parse("2017-04-25") );
            Assert.NotNull(actual.SessionId);
        }

        [Fact]
        public async Task SessionShouldAllowUserToGetResults()
        {
            var target = new ApiClient();
            var sessions = await target.GetSessions();
            var testSession = sessions.First(srd => srd.Status == SessionResponseDtoStatus.Completed);
            var actual = await target.GetSessionResultsAsync(testSession.SessionId);
            Assert.True(actual.Data.Count >= 0);
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
            var session = new SessionData { DataSetName = "Something", StartDate = DateTime.Parse("2017-01-01"), EndDate = DateTime.Parse("2017-01-31") };
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
           
            var session = new SessionData { DataSetName = key, StartDate = DateTime.Parse("2017-03-26"), EndDate = DateTime.Parse("2017-04-25")  };
            var sessonResponse = await target.ForecastFromSavedDataSetAsync(session, "foos");

            Assert.NotNull(sessonResponse);
            Assert.Equal(key, sessonResponse.DataSetName);
            Assert.Equal("foos", sessonResponse.TargetColumn);
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
            var session = new SessionData { DataSetName = Guid.NewGuid().ToString(), StartDate = DateTime.Parse("2017-03-26"), EndDate = DateTime.Parse("2017-04-25") };
            var actual = await target.ForecastFromCsvAsync(session, new FileInfo(productFilePath), "sales");

            Assert.NotNull(actual.SessionId);

            var status = await target.GetSessionStatusAsync(actual.SessionId);

            Assert.NotNull(status);
            Assert.True((int)status.Status <= (int)SessionResponseDtoStatus.Started);
            Assert.Equal(actual.SessionId, status.SessionId);
        }

        [Fact]
        public async Task WillRunImpactSessionOnEvent()
        {
            var target = new ApiClient();
            var session = new SessionData { DataSetName = Guid.NewGuid().ToString(), StartDate = DateTime.Parse("2015-10-01"), EndDate = DateTime.Parse("2016-01-01") };
            var result = await target.EventImpactFromCsvAsync(session, new FileInfo(productFilePath), "super duper sale", "sales");

            Assert.NotNull(result);

            var status = await target.GetSessionStatusAsync(result.SessionId);

            Assert.NotNull(status);
            Assert.True((int)status.Status <= (int)SessionResponseDtoStatus.Started);
            Assert.Equal(result.SessionId, status.SessionId);
        }

        private DataSet GenerateDataSet(DateTime startDate, DateTime endDate, string targetKey)
        {
            var rand = new Random();
            var dates = Enumerable.Range(0, (endDate.Date - startDate.Date).Days).Select(i => startDate.Date.AddDays(i));

            return new DataSet
            {
                Data = dates.Select(d => new DataSetRow
                {
                    Timestamp = d,
                    Values = new Dictionary<string, double> { { targetKey, rand.NextDouble() * 100 } }
                }).ToList()
            };
        }

    }
}
