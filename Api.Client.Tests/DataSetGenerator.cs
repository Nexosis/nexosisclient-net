using System;
using System.Collections.Generic;
using System.Linq;
using Nexosis.Api.Client.Model;

namespace Api.Client.Tests
{
    public static class DataSetGenerator
    {
        public static DataSet Run(DateTimeOffset startDate, DateTimeOffset endDate, string targetKey)
        {
            var rand = new Random();
            var dates = Enumerable.Range(0, (endDate.Date - startDate.Date).Days).Select(i => startDate.Date.AddDays(i));

            return new DataSet
            {
                Data = dates.Select(d => new Dictionary<string, string>
                {
                    { "time", d.ToString("O") },
                    { targetKey, (rand.NextDouble() * 100).ToString() }
                }).ToList(),
                Columns = new Dictionary<string, ColumnMetadata>
                {
                    { "time", new ColumnMetadata { DataType = ColumnType.Date, Role = ColumnRole.Timestamp } },
                    { targetKey, new ColumnMetadata { DataType = ColumnType.Numeric, Role = ColumnRole.Target } }
                }
            };
        }
    }
}
