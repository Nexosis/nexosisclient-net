using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Nexosis.Api.Client;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests
{
    [Collection("Integration")]
    public class DataSetIntegrationTests
    {
        private readonly IntegrationTestFixture fixture;

        public DataSetIntegrationTests(IntegrationTestFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task CanSaveDataSet()
        {
            var data = DataSetGenerator.Run(DateTime.Parse("2017-01-01"), DateTime.Parse("2017-03-31"), "foxtrot");

            var result = await fixture.Client.DataSets.Create("whiskey", data);

            Assert.Equal("whiskey", result.DataSetName);
        }

        [Fact]
        public async Task GettingDataSetGivesBackLinks()
        {
            var result = await fixture.Client.DataSets.Get("whiskey");

            Assert.Equal(3, result.Links.Count);
            Assert.Equal(new [] { "forecast", "model", "sessions"}, result.Links.Select(l => l.Rel));
            Assert.Equal("https://api.dev.nexosisdev.com/api/data/whiskey/forecast", result.Links[0].Href);
            Assert.Equal("https://api.dev.nexosisdev.com/api/data/whiskey/forecast/model", result.Links[1].Href);
            Assert.Equal("https://api.dev.nexosisdev.com/api/sessions?dataSetName=whiskey", result.Links[2].Href);
        }

        [Fact]
        public async Task CanGetDataSetThatHasBeenSaved()
        {
            var data = DataSetGenerator.Run(DateTime.Parse("2017-01-01"), DateTime.Parse("2017-03-31"), "hotel");

            await fixture.Client.DataSets.Create("india", data);

            var result = await fixture.Client.DataSets.Get("india");

            Assert.Equal(DateTime.Parse("2017-01-01").ToLocalTime(), result.Data.First().Timestamp);
            Assert.True(result.Data.First().Values.ContainsKey("hotel"));
        }

        [Fact]
        public async Task CanPutMoreDataToSameDataSet()
        {
            var data = DataSetGenerator.Run(DateTime.Parse("2017-01-01"), DateTime.Parse("2017-01-31"), "hotel");
            await fixture.Client.DataSets.Create("charlie", data);

            var moreData = DataSetGenerator.Run(DateTime.Parse("2017-02-01"), DateTime.Parse("2017-03-01"), "hotel");
            await fixture.Client.DataSets.Create("charlie", moreData);

            var result = await fixture.Client.DataSets.Get("charlie");

            var orderedData = result.Data.OrderBy(d => d.Timestamp);
            Assert.Equal(DateTime.Parse("2017-01-01").ToLocalTime(), orderedData.First().Timestamp);
            Assert.Equal(DateTime.Parse("2017-02-28").ToLocalTime(), orderedData.Last().Timestamp);
        }

        [Fact]
        public async Task ListsDataSets()
        {
            var list = await fixture.Client.DataSets.List();

            Assert.True(list.Count > 0);
        }

        [Fact]
        public async Task CanRemoveDataSet()
        {
            var id = Guid.NewGuid().ToString("N");

            var data = DataSetGenerator.Run(DateTime.Parse("2017-01-01"), DateTime.Parse("2017-03-31"), "hotel");

            await fixture.Client.DataSets.Create(id, data);
            await fixture.Client.DataSets.Remove(id, DataSetDeleteOptions.None);

            var exception = await Assert.ThrowsAsync<NexosisClientException>(async () => await fixture.Client.DataSets.Get(id));

            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task QueryingForecastWithPredictionRunsCanGetForecastData()
        {
            var result = await fixture.Client.DataSets.GetForecast(fixture.SavedDataSet);

            Assert.Equal(30, result.Data.Count);
        }

        [Fact]
        public async Task QueryingForecastCanGetListOfAlgorithmsUsed()
        {
            var result = await fixture.Client.DataSets.ListForecastModels(fixture.SavedDataSet);

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task QueryingForecastCanGetListSingleModel()
        {
            var result = await fixture.Client.DataSets.GetForecastModel(fixture.SavedDataSet, "sales");

            Assert.Equal("Seasonal Median, Weekly", result.ChampionContest.Champion.Algorithm.Name);
        }

        [Fact(Skip = "Only run if changing the API key used.")]
        public async Task PopulateDataForTesting()
        {
            // loads a data set and creates a forecast so we can query it when running the tests
            string dataSet = fixture.SavedDataSet;

            using (var file = File.OpenText("..\\..\\..\\CsvFiles\\producttest.csv"))
            {
                await fixture.Client.DataSets.Create(dataSet, file);
            }
            await fixture.Client.Sessions.CreateForecast(dataSet, "sales", DateTimeOffset.Parse("2017-03-25 0:00:00 -0:00"), DateTimeOffset.Parse("2017-04-24 0:00:00 -0:00"));

            var names = String.Join(", ", fixture.Client.DataSets.List(dataSet).GetAwaiter().GetResult().Select(ds => ds.DataSetName));
            Console.WriteLine(names);
        }


    }
}
