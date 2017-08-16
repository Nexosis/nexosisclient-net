using System;

namespace Nexosis.Api.Client.Model
{
    [Flags]
    public enum ViewCascadeOptions
    {
        /// <summary>No extra options.</summary>
        None = 0,

        /// <summary>Also deletes sessions associated with the dataset.  If start and/or end date are supplied, sessions requested in that date range are deleted.</summary>
        CascadeSessions = 1,

    }
}