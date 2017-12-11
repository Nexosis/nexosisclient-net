namespace Nexosis.Api.Client.Model
{
    public class ModelSessionRequest : SessionRequest
    {
        /// <summary>
        /// The <see cref="PredictionDomain"/> of the Model to be built
        /// </summary>
        public PredictionDomain PredictionDomain { get; set; }

    }
}