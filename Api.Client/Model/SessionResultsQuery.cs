namespace Nexosis.Api.Client.Model
{
    public class SessionResultsQuery
    {
        /// <summary>
        /// Optional. The results returned will be from the given prediction interval.
        /// </summary>
        public string PredictionInterval { get; set; }

        /// <summary>
        /// Paging info for the response
        /// </summary>
        public PagingInfo Page { get; set; }
    }
}