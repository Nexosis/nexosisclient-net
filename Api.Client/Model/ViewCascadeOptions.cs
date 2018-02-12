using System;

namespace Nexosis.Api.Client.Model
{
    [Flags]
    public enum ViewCascadeOptions
    {
        /// <summary>
        /// No extra options.
        /// </summary>
        None = 0,

        /// <summary>
        /// Also deletes sessions associated with the view.  If start and/or end date are supplied, sessions requested in that date range are deleted.
        /// </summary>
        CascadeSessions = 1,

        /// <summary>
        /// Also deletes any models which have been created based on this view.
        /// </summary>
        CascadeModels = 2,

        /// <summary>
        /// Also deletes any vocabularies which have been created based on this view.
        /// </summary>
        CascadeVocabularies = 4,

        /// <summary>
        /// Cascade deletes to sessions, models, and vocabularies associated with the dataset.
        /// </summary>
        CascadeAll = CascadeSessions | CascadeModels | CascadeVocabularies
    }
}