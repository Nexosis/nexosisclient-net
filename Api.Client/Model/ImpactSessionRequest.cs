namespace Nexosis.Api.Client.Model
{
    public class ImpactSessionRequest : TimeSeriesSessionRequest
    {
        /// <summary>
        /// The name of the event whose impact we are trying to determine
        /// </summary>
        public string EventName { get; set; }

    }
}