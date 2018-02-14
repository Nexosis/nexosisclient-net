﻿using System;

namespace Nexosis.Api.Client.Model
{
    [Flags]
    public enum DataSetDeleteOptions
    {
        /// <summary>
        /// No extra options.
        /// </summary>
        None = 0,
        /// <summary>
        /// Also deletes sessions associated with the dataset.  If start and/or end date are supplied, sessions requested in that date range are deleted.
        /// </summary>
        CascadeSessions = 2,
        /// <summary>
        /// Also deletes any views which have been created based on this dataset.
        /// </summary>
        CascadeViews = 4,

        /// <summary>
        /// Also deletes any models which have been created based on this dataset.
        /// </summary>
        CascadeModels = 8,

        /// <summary>
        /// Also deletes any vocabularies which have been created based on this dataset.
        /// </summary>
        CascadeVocabularies = 16,

        /// <summary>
        /// Deletes the forcasts and sessions associated with the dataset.
        /// </summary>
        [Obsolete("Now poorly named, but preserved for backward compatibility. Use CascadeAll or build your own composite.")]
        CascadeBoth = CascadeSessions,
        /// <summary>
        /// Cascade deletes to forecasts, sessions, views, models, and vocabularies associated with the dataset.
        /// </summary>
        CascadeAll = CascadeSessions | CascadeViews | CascadeModels | CascadeVocabularies
    }
}