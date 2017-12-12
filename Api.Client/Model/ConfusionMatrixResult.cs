namespace Nexosis.Api.Client.Model
{
    public class ConfusionMatrixResult : SessionResult
    {
        /// <summary>
        /// For classification sessions, the confusion matrix for the model
        /// </summary>
        public int[][] ConfusionMatrix { get; set; }

        /// <summary>
        /// For classification sessions, the list of classes that were included in the model
        /// </summary>
        public string[] Classes { get; set; }
    }
}