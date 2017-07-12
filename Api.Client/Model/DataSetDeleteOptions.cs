using System;

namespace Nexosis.Api.Client.Model
{
    [Flags]
    public enum DataSetDeleteOptions
    {
        /// <summary>No extra options.</summary>
        None = 0,
        /// <summary>Also deletes forecasts for the dataset.</summary>
        CascadeForecast = 1,
        /// <summary>Also deletes sessions associated with the dataset.  If start and/or end date are supplied, sessions requested in that date range are deleted.</summary>
        CascadeSessions = 2,
        /// <summary>Deletes the forcasts and sessions associated with the dataset.</summary>
        CascadeBoth = CascadeForecast | CascadeSessions
    }
}