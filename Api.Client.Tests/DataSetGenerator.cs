using System;
using System.Collections.Generic;
using System.Linq;
using Nexosis.Api.Client.Model;

namespace Api.Client.Tests
{
    public static class DataSetGenerator
    {
        public static DataSet Run(DateTimeOffset startDate, DateTimeOffset endDate, string targetKey, bool implicitTimestamp = false)
        {
            var tscol = implicitTimestamp ? "timeStamp" : "time";
            var rand = new Random();
            var dates = Enumerable.Range(0, (endDate.Date - startDate.Date).Days).Select(i => startDate.UtcDateTime.Date.AddDays(i));

            var ds = new DataSet
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
        
        
    }
}
