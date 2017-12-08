using System;
using System.Globalization;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{
    public static class Sessions
    {
        public static ModelSessionRequest TrainModel(string dataSourceName, PredictionDomain domain,
            string targetColumn = null, ModelSessionRequest options = null)
        {
            var request = options ?? new ModelSessionRequest();
            request.DataSourceName = dataSourceName;
            if (targetColumn != null)
            {
                request.TargetColumn = targetColumn;
            }

            request.PredictionDomain = domain;
            return request;
        }

        public static ForecastSessionRequest Forecast(string dataSourceName, DateTimeOffset startDate, DateTimeOffset endDate, ResultInterval resultInterval, string targetColumn = null, ForecastSessionRequest options = null)
        {
            var request = options ?? new ForecastSessionRequest();
            request.DataSourceName = dataSourceName;
            if (targetColumn != null)
            {
                request.TargetColumn = targetColumn;                
            }
            request.StartDate = startDate;
            request.EndDate = endDate;
            request.ResultInterval = resultInterval;
            return request;
        }

        public static ImpactSessionRequest Impact(string dataSourceName, DateTimeOffset startDate,
            DateTimeOffset endDate, ResultInterval resultInterval, string eventName, string targetColumn = null,
            ImpactSessionRequest options = null)
        {
            var request = options ?? new ImpactSessionRequest();
            request.DataSourceName = dataSourceName;
            if (targetColumn != null)
            {
                request.TargetColumn = targetColumn;
            }
            request.EventName = eventName;
            request.StartDate = startDate;
            request.EndDate = endDate;
            request.ResultInterval = resultInterval;
            return request;
        }

        public static SessionQuery Where(string dataSourceName = null, DateTimeOffset? requestedAfterDate = null, DateTimeOffset? requestedBeforeDate = null, SessionQuery query = null)
        {
            var theQuery = query ?? new SessionQuery() {DataSourceName = dataSourceName};
            theQuery.DataSourceName = dataSourceName;
            if (requestedAfterDate.HasValue)
            {
                theQuery.RequestedAfterDate = requestedAfterDate;
            }
            if (requestedBeforeDate.HasValue)
            {
                theQuery.RequestedBeforeDate = requestedBeforeDate;
            }
            
            return theQuery;
        }
    }
}