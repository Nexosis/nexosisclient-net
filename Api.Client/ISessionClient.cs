using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client
{
    public interface ISessionClient
    {

        Action<HttpRequestMessage, HttpResponseMessage> HttpMessageTransformer { get; set; }

        /// <summary>
        /// Forecast from data already saved to the API.
        /// </summary>
        /// <param name="request">The parameters for the Forecast session.  Create with <see cref="Sessions.Forecast"/>.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/forecast</remarks>
        Task<SessionResponse> CreateForecast(ForecastSessionRequest request, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// Analyze impact for an event with data already saved to the API.
        /// </summary>
        /// <param name="request">The <see cref="ImpactSessionRequest"/> describing the parameters of the Impact session. Create with <see cref="Sessions.Impact"/>.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the sesssion.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/impact</remarks>
        Task<SessionResponse> AnalyzeImpact(ImpactSessionRequest request, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Train a model from data already saved to the API.
        /// </summary>
        /// <param name="request">The <see cref="ModelSessionRequest"/> with the parameters required for training the model. Create with <see cref="Sessions.TrainModel"/>.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><see cref="SessionResponse"/> providing information about the session.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is recived from server, or errors in parsing the response.</exception>
        /// <remarks>POST to https://ml.nexosis.com/api/sessions/model</remarks>
        Task<SessionResponse> TrainModel(ModelSessionRequest request, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// List sessions that have been run. This will show the information about them such as the id, status, and the analysis date range. 
        /// </summary>
        /// <param name="query">The <see cref="SessionQuery"/> with the criteria for which sessions to return</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The list of <see cref="SessionResponse"/> objects.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions</remarks>
        Task<SessionResponseList> List(SessionQuery query = null, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// Remove sessions that have been run. All parameters are optional and will be used to limit the sessions removed.
        /// </summary>
        /// <param name="criteria">The criteria to be used to determine which sessions are removed</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/sessions</remarks>
        Task Remove(SessionRemoveCriteria criteria, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// Get a specific session by id.
        /// </summary>
        /// <param name="id">The identifier of the session.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="SessionResponse" /> populated with the sessions data.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions/{id}</remarks>
        Task<SessionResponse> Get(Guid id, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// Lookup the status of the session. 
        /// </summary>
        /// <param name="id">The identifier of the session.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="SessionResultStatus"/> with the status set.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>HEAD of https://ml.nexosis.com/api/sessions/{id}</remarks>
        Task<SessionResultStatus> GetStatus(Guid id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Remove the session. 
        /// </summary>
        /// <param name="id">The identifier of the session to remove.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>DELETE to https://ml.nexosis.com/api/sessions/{id}</remarks>
        Task Remove(Guid id, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// Get the results of the session.
        /// </summary>
        /// <param name="id">The identifier of the session.</param>
        /// <param name="query">Query Parameters for the results to be returned</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="SessionResult"/> which contains the results of the run.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        /// <remarks>GET of https://ml.nexosis.com/api/sessions/{id}/results</remarks>
        Task<SessionResult> GetResults(Guid id, SessionResultsQuery query = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the confusion matrix for the classification model generated by a model-building session
        /// </summary>
        /// <remarks>
        /// GET of https://ml.nexosis.com/api/sessions/{id}/results/confusionmatrix
        /// #### Gets the confusion matrix for the model generated by a model-building session
        /// A confusion matrix describes the performance of the classification model generated by this session by 
        /// showing how each record in the test set was classified by the model. The rows in the confusion matrix
        /// are actual classes from the test set, and the columns are classes predicted by the model for those rows.
        /// Each cell in the matrix contains the count of records in the test set with a particular actual value and 
        /// predicted value. The headers for both rows and columns of the matrix can be found in the `classes`
        /// property of the response.
        /// </remarks>
        /// <param name="id">The identifier of the session.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="ConfusionMatrixResult"/> which contains the results of the run.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        Task<ConfusionMatrixResult> GetResultConfusionMatrix(Guid id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the scores of the entire dataset generated by a particular completed anomalies session
        /// </summary>
        /// <remarks>
        /// GET of https://ml.nexosis.com/api/sessions/{id}/results/anomalyscores
        /// #### Gets the scores of the entire dataset generated by a particular completed anomalies session
        /// Anomaly detection scores are generated for every row in the data source used by the session. The target value in each row is negative if the row was identified as 
        /// an outlier, and positive if the row was identified as "normal". The magnitude of the value provides a relative indicator of "how anomalous" or "how normal" the row is. 
        /// </remarks>
        /// <param name="id">The identifier of the session.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="SessionResult"/> which contains the results of the run.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        Task<SessionResult> GetResultAnomalyScores(Guid id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the class scores for each result of a particular completed classification model session
        /// </summary>
        /// <remarks>
        /// GET of https://ml.nexosis.com/api/sessions/{id}/results/classscores
        /// #### Gets the class scores for each result of a particular completed classification model session
        /// Whereas classification session results indicate the class chosen for each row in the test set, this endpoint returns the scores for each possible class
        /// for ech row in the test set. Higher scores indicate that the model is more confident that the row fits into the specified class, but the scores are not
        /// strict probabilities, and they are not comparable across sessions or data sources.
        /// </remarks>
        /// <param name="id">The identifier of the session.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="SessionResult"/> which contains the results of the run.</returns>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the response.</exception>
        Task<SessionResult> GetResultClassScores(Guid id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// A client for getting information about how Nexosis determined the algorithm to use for a session.
        /// <remarks>Only available to customers on our paid tiers</remarks>
        /// </summary>
        IContestClient Contest { get; }
    }
}