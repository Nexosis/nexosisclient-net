using System;
using System.Collections.Generic;
using System.Linq;
using Nexosis.Api.Client.Model;

namespace Api.Client.Tests
{
    public static class DataSetGenerator
    {
        public static DataSetDetail Run(DateTimeOffset startDate, DateTimeOffset endDate, string targetKey, bool implicitTimestamp = false)
        {
            var tscol = implicitTimestamp ? "timeStamp" : "time";
            var rand = new Random();
            var dates = Enumerable.Range(0, (endDate.Date - startDate.Date).Days).Select(i => startDate.UtcDateTime.Date.AddDays(i));

            var ds = new DataSetDetail
            {
                Data = dates.Select(d => new Dictionary<string, string>
                {
                    { tscol, d.ToString("O") },
                    { targetKey, (rand.NextDouble() * 100).ToString() }
                }).ToList(),
                Columns = new Dictionary<string, ColumnMetadata>
                {
                    { targetKey, new ColumnMetadata { DataType = ColumnType.Numeric, Role = ColumnRole.Target } }
                }
            };
            
            if (!implicitTimestamp)
                ds.Columns.Add(tscol, new ColumnMetadata { DataType = ColumnType.Date, Role = ColumnRole.Timestamp });
            
            return ds;
        }

        public static DataSetDetail Run(int rowCount, int columns, string targetKey)
        {
            var rand = new Random();
            var columnNames = Enumerable.Range(0, columns).Select(i => i.ToString()).ToList();
            if(targetKey != null)
            {
                columnNames.Add(targetKey);
            }

            var rows = Enumerable.Range(0, rowCount).Select(r =>
                columnNames.ToDictionary(k => k, v => (rand.NextDouble() * 100).ToString())).ToList();
            var ds = new DataSetDetail
            {
                Data = rows,
                Columns = columnNames.ToDictionary(k => k, v => new ColumnMetadata { DataType = ColumnType.String, Role = ColumnRole.Feature })
            };

            if(targetKey != null)
            {
                ds.Columns[targetKey] = new ColumnMetadata { DataType = ColumnType.Numeric, Role = ColumnRole.Target };
            }

            return ds;
        }
    }
}
