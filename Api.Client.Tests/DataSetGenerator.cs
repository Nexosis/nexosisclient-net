using System;
using System.Collections.Generic;
using System.Linq;
using Nexosis.Api.Client.Model;

namespace Api.Client.Tests
{
    public static class DataSetGenerator
    {
        public static List<DataSetRow> Run(DateTime startDate, DateTime endDate, string targetKey)
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
