using System;
using System.IO;
using Nexosis.Api.Client;
using Xunit;

namespace Api.Client.Tests
{
    public class CsvTests
    {
        [Fact]
        public void CsvParseShouldReturnMetadata()
        {
            var filePath = Path.Combine(new DirectoryInfo(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, @"CsvFiles\fulldata.csv");
            var file = new FileInfo(filePath);
            var actual = ApiClient.TestParseCsv(file);
            Assert.NotNull(actual);
            Assert.Equal(5, actual.ColumnCount);
            Assert.Equal(43, actual.RowCount);
        }

        [Fact]
        public void CsvParseShouldntCareIfNoHeader()
        {
            //TODO: we will eventually care if there is a header or not from a column naming standpoint. For now, your first observation just becomes the header row
            var filePath = Path.Combine(new DirectoryInfo(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, @"CsvFiles\noheader.csv");
            var file = new FileInfo(filePath);
            var actual = ApiClient.TestParseCsv(file);
            Assert.NotNull(actual);
            Assert.Equal(5, actual.ColumnCount);
            Assert.Equal(42, actual.RowCount);
        }
    }
}
