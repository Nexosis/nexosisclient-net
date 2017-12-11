namespace Nexosis.Api.Client.Model
{
    public class ConfusionMatrixResult : SessionResult
    {
        /// <summary>
        /// For classification sessions, the confusion matrix for the model
        /// </summary>
        public int[][] ConfusionMatrix { get; set; }
    }
}