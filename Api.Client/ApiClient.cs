using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using CsvHelper;

namespace Nexosis.Api.Client
{
    /// <summary>
    /// Simplified client for making calls to the Nexosis API
    /// </summary>
    /// <remarks>Wraps generated client</remarks>
    public partial class ApiClient
    {
        private static readonly IConfigurationRoot Configuration;
        static ApiClient()
        {
            var settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            var builder = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory);
            if (File.Exists(settingsPath))
            {
                builder.AddJsonFile("appsettings.json");
            }
            Configuration = builder.Build();
        }

#if DEBUG
        private readonly string host = "https://api.dev.nexosisdev.com/api";
#else
        private readonly string host = "https://ml.nexosis.com/api";
#endif
        private readonly string _apiKey;

        /// <summary>
        /// Create a new instance of the Api Client depending on configuration for the api key
        /// </summary>
        public ApiClient() : this(Configuration["NexosisApiKey"]) { }

        /// <summary>
        /// Create a new instance of the Api Client with the given api key
        /// </summary>
        /// <param name="nexosisApiKey">Subscription key obtained from the Nexosis developer portal</param>
        public ApiClient(string nexosisApiKey)
        {
            _apiKey = nexosisApiKey;
            if (string.IsNullOrEmpty(_apiKey))
                throw new InvalidOperationException("No api key was found. Please set your api key in the NexosisApiKey keyed app settings parameter or construct the client using an api key.");

            BaseUrl = host;
        }

        /// <summary>
        /// Forecast data from the contents of a CSV file.
        /// </summary>
        /// <param name="session">A <c>SessionData</c> object describing the prediction session to run.</param>
        /// <param name="fileName">The path to the local file containing the data you wish to submit.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data for prediction.</param>
        /// <returns>Session</returns>
        public Task<Session> ForecastFromCsvAsync(SessionData session, string fileName, string targetColumn)
        {
            return ForecastFromCsvAsync(session, new FileInfo(fileName), targetColumn);
        }

        /// <summary>
        /// Forecast from the contents of a CSV file.
        /// </summary>
        /// <param name="session">A <c>SessionData</c> object describing the prediction session to run.</param>
        /// <param name="file">A reference to a local file which contains the data you wish to submit.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data for prediction.</param>
        /// <returns>Session</returns>
        public async Task<Session> ForecastFromCsvAsync(SessionData session, FileInfo file, string targetColumn)
        {
            if (file.Exists)
            {
                TestParseCsv(file);
                var contents = file.OpenText().ReadToEnd();
                return new Session(await this.SessionsForecastPostDataAsync(contents, session.DataSetName, targetColumn, session.StartDate?.ToString(), session.EndDate.ToString()));
            }
            else
            {
                throw new FileNotFoundException("The data file submitted could not be found. Please check the path and try again.");
            }
        }

        /// <summary>
        /// Forecast from data posted in the request.
        /// </summary>
        /// <param name="session">A <c>SessionData</c> object describing the prediction session to run.</param>
        /// <param name="data">A <see cref="DataSet">DataSet</see> containing the data used for the forecast.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <returns>Session</returns>
        public async Task<Session> ForecastFromDataAsync(SessionData session, DataSet data, string targetColumn)
        {
            return new Session(await SessionsCreateForecastSessionAsync(session.DataSetName, targetColumn, session.StartDate?.ToString(), session.EndDate.ToString(), data.ToDto()));
        }

        /// <summary>
        /// Forecast from data already saved to the API.
        /// </summary>
        /// <param name="session">A <c>SessionData</c> object describing the prediction session to run.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <returns>Session</returns>
        public async Task<Session> ForecastFromSavedDataSetAsync(SessionData session, string targetColumn)
        {
            return new Session(await SessionsCreateForecastSessionAsync(session.DataSetName, targetColumn, session.StartDate?.ToString(), session.EndDate.ToString(), null));
        }

        /// <summary>
        /// Analyze impact for an event with data from a CSV file.
        /// </summary>
        /// <param name="session">A <c>SessionData</c> object describing the prediction session to run.</param>
        /// <param name="fileName">The path to the local file containing the data you wish to submit.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <returns></returns>
        public Task<Session> EventImpactFromCsvAsync(SessionData session, string fileName, string eventName, string targetColumn)
        {
            return EventImpactFromCsvAsync(session, new FileInfo(fileName), eventName, targetColumn);
        }

        /// <summary>
        /// Analyze impact for an event with data from a CSV file.
        /// </summary>
        /// <param name="session">A <c>SessionData</c> object describing the prediction session to run.</param>
        /// <param name="file">A reference to a local file which contains the data you wish to submit.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <returns></returns>
        public async Task<Session> EventImpactFromCsvAsync(SessionData session, FileInfo file, string eventName, string targetColumn)
        {
            if (file.Exists)
            {
                TestParseCsv(file);
                var contents = file.OpenText().ReadToEnd();
                return new Session(await SessionsCreateImpactSessionAsync(session.DataSetName, targetColumn, eventName, session.StartDate?.ToString(), session.EndDate.ToString(), contents, CancellationToken.None));
            }
            else
            {
                throw new FileNotFoundException("The data file submitted could not be found. Please check the path and try again.");
            }

        }

        /// <summary>
        /// Analyze impact for an event with data in the request.
        /// </summary>
        /// <param name="session">A <c>SessionData</c> object describing the prediction session to run.</param>
        /// <param name="data">A <see cref="DataSet">DataSet</see> containing the data used for the analysis.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <returns></returns>
        public async Task<Session> EventImpactFromDataAsync(SessionData session, DataSet data, string eventName, string targetColumn)
        {
            return new Session(await SessionsCreateImpactSessionAsync(session.DataSetName, targetColumn, eventName, session.StartDate?.ToString(), session.EndDate.ToString(), data.ToDto()));
        }

        /// <summary>
        /// Analyze impact for an event with data already saved to the API.
        /// </summary>
        /// <param name="session">A <c>SessionData</c> object describing the prediction session to run.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="targetColumn">The name of the column that should be used as the source data.</param>
        /// <returns></returns>
        public async Task<Session> EventImpactFromSavedDataAsync(SessionData session, string eventName, string targetColumn)
        {
            return new Session(await SessionsCreateImpactSessionAsync(session.DataSetName, targetColumn, eventName, session.StartDate?.ToString(), session.EndDate.ToString(), null));
        }

        /// <summary>
        /// List sessions that have been run. This will show the information about them such as the id, status, and dates that predictions were run over. 
        /// </summary>
        /// <returns>The list of sessions </returns>
        public async Task<List<Session>> GetSessions()
        {
            var sessions = await SessionsListAllAsync(null, null, null);
            return new List<Session>(sessions.Results.Select(s => new Session(s)));
        }

        /// <summary>
        /// Get the results of the session.
        /// </summary>
        /// <param name="sessionId">The sessionId to lookup.</param>
        /// <returns>A DataSet which contains the results of the run.</returns>
        public async Task<DataSet> GetSessionResultsAsync(Guid sessionId)
        {
            var result = await SessionsRetrieveResultsAsync(sessionId.ToString());
            return new DataSet { Data = new List<DataSetRow>(result.Data) };
        }

        /// <summary>
        /// Lookup the status of the session. This lets you keep track of the progress of your work. 
        /// </summary>
        /// <param name="sessionId">The <c>sessionId</c> to lookup.</param>
        /// <returns>SessionStatus</returns>
        public Task<SessionStatus> GetSessionStatusAsync(Guid sessionId)
        {
            return GetSessionStatusInternal(sessionId);
        }

        /// <summary>
        /// Save data to the API for later use.
        /// </summary>
        /// <param name="dataSetName">Name of the dataset to which to add data.</param>
        /// <param name="data">A DataSet containing the data.</param>
        /// <returns>DataSetSummary</returns>
        public Task<DataSetSummary> SaveDataSetAsync(string dataSetName, DataSet data)
        {
            return DatasetsAddDataAsync(dataSetName, data.ToDto());
        }

        /// <summary>
        /// Gets the list of all data sets that have been saved to the system.
        /// </summary>
        /// <returns>List of DataSetSummary</returns>
        public async Task<List<DataSetSummary>> ListDataSetsAsync()
        {
            return (await DatasetsListAllAsync(null)).DataSets.ToList();
        }

        /// <summary>
        /// Gets the list of data sets that have been saved to the system, filtering by partial name match.
        /// </summary>
        /// <param name="nameFilter">Limits results to only those datasets with names containing the specified value</param>
        /// <returns>DataSetSummary</returns>
        public async Task<List<DataSetSummary>> ListDataSetsAsync(string nameFilter)
        {
            return (await DatasetsListAllAsync(nameFilter)).DataSets.ToList();
        }


        /// <summary>
        /// Test the CSV file to determine if it is valid and will be readable by the API.
        /// </summary>
        /// <param name="fileName">The path to the file to test.</param>
        /// <returns>CsvFileMetadata</returns>
        public static CsvFileMetadata TestParseCsv(string fileName)
        {
            return TestParseCsv(new FileInfo(fileName));
        }

        /// <summary>
        /// Test the CSV file to determine if it is valid and will be readable by the API.
        /// </summary>
        /// <param name="file">A <see cref="FileInfo">FileInfo</see> object that references the file to test.</param>
        /// <returns>CsvFileMetadata</returns>
        public static CsvFileMetadata TestParseCsv(FileInfo file)
        {
            List<Exception> exceps = new List<Exception>();
            var metadata = new CsvFileMetadata();
            metadata.MBSizeOnDisk = file.Length / 1024d;
            var config = new CsvHelper.Configuration.CsvConfiguration { IgnoreReadingExceptions = true, DetectColumnCountChanges = true };
            config.ReadingExceptionCallback = (ex, row) =>
            {
                exceps.Add(ex);
            };
            config.BadDataCallback = (str) =>
            {
                exceps.Add(new Exception($"There was a problem reading {str}"));
            };
            using (var textReader = file.OpenText())
            {
                var csv = new CsvReader(textReader, config);
                var hasHeader = csv.ReadHeader();
                var rowCount = 0;
                try
                {
                    while (csv.Read())
                    {
                        rowCount++;
                        if (metadata.ColumnCount == 0)
                            metadata.ColumnCount = csv.CurrentRecord.Length;
                    }
                    metadata.RowCount = rowCount;
                }
                catch (CsvBadDataException eData)
                {
                    exceps.Add(eData);
                }
                if (exceps.Count > 0)
                    throw new CsvParseException("There were one more errors found while trying to parse the given CSV file. Please review the ParseExceptions property for details.") { ParseExceptions = exceps };
            }
            return metadata;
        }

        private async Task<SessionResponseDto> SessionsForecastPostDataAsync(string data, string dataSetName, string targetColumn, string predictionStartDate, string predictionEndDate)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl).Append("/sessions/forecast?");
            if (dataSetName != null) urlBuilder_.Append("dataSetName=").Append(System.Uri.EscapeDataString(dataSetName.ToString())).Append("&");
            if (targetColumn != null) urlBuilder_.Append("targetColumn=").Append(System.Uri.EscapeDataString(targetColumn.ToString())).Append("&");
            if (predictionStartDate != null) urlBuilder_.Append("startDate=").Append(System.Uri.EscapeDataString(predictionStartDate.ToString())).Append("&");
            if (predictionEndDate != null) urlBuilder_.Append("endDate=").Append(System.Uri.EscapeDataString(predictionEndDate.ToString())).Append("&");
            urlBuilder_.Length--;

            var client_ = new System.Net.Http.HttpClient();
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    var content_ = new System.Net.Http.StringContent(data);
                    content_.Headers.ContentType.MediaType = "text/csv";
                    request_.Content = content_;
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        foreach (var item_ in response_.Content.Headers)
                            headers_[item_.Key] = item_.Value;

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var responseData_ = await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            var result_ = default(SessionResponseDto);
                            try
                            {
                                result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<SessionResponseDto>(responseData_);
                                result_.AssignCost(headers_);
                                return result_;
                            }
                            catch (System.Exception exception)
                            {
                                throw new SwaggerException("Could not deserialize the response body.", status_, responseData_, headers_, exception);
                            }
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new SwaggerException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", status_, responseData_, headers_, null);
                        }

                        return default(SessionResponseDto);
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (client_ != null)
                    client_.Dispose();
            }
        }

        private async System.Threading.Tasks.Task<SessionResponseDto> SessionsCreateImpactSessionAsync(string dataSetName, string targetColumn, string eventName, string startDate, string endDate, string dataSetData, System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl).Append("/sessions/impact?");
            if (dataSetName != null) urlBuilder_.Append("dataSetName=").Append(System.Uri.EscapeDataString(dataSetName.ToString())).Append("&");
            if (targetColumn != null) urlBuilder_.Append("targetColumn=").Append(System.Uri.EscapeDataString(targetColumn.ToString())).Append("&");
            if (eventName != null) urlBuilder_.Append("eventName=").Append(System.Uri.EscapeDataString(eventName.ToString())).Append("&");
            if (startDate != null) urlBuilder_.Append("startDate=").Append(System.Uri.EscapeDataString(startDate.ToString())).Append("&");
            if (endDate != null) urlBuilder_.Append("endDate=").Append(System.Uri.EscapeDataString(endDate.ToString())).Append("&");
            urlBuilder_.Length--;
    
            var client_ = new System.Net.Http.HttpClient();
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    var content_ = new System.Net.Http.StringContent(dataSetData);
                    content_.Headers.ContentType.MediaType = "text/csv";
                    request_.Content = content_;
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    
                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);
    
                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        foreach (var item_ in response_.Content.Headers)
                            headers_[item_.Key] = item_.Value;
    
                        ProcessResponse(client_, response_);
    
                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200") 
                        {
                            var responseData_ = await response_.Content.ReadAsStringAsync().ConfigureAwait(false); 
                            var result_ = default(SessionResponseDto); 
                            try
                            {
                                result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<SessionResponseDto>(responseData_);
                                result_.AssignCost(headers_);
                                return result_; 
                            } 
                            catch (System.Exception exception) 
                            {
                                throw new SwaggerException("Could not deserialize the response body.", status_, responseData_, headers_, exception);
                            }
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = await response_.Content.ReadAsStringAsync().ConfigureAwait(false); 
                            throw new SwaggerException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", status_, responseData_, headers_, null);
                        }
            
                        return default(SessionResponseDto);
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (client_ != null)
                    client_.Dispose();
            }
        }

        private async Task<SessionStatus> GetSessionStatusInternal(Guid sessionId)
        {
            if (sessionId == null)
                throw new System.ArgumentNullException(nameof(sessionId));
    
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl).Append("/sessions/{sessionId}");
            urlBuilder_.Replace("{sessionId}", System.Uri.EscapeDataString(sessionId.ToString()));
    
            var client_ = new System.Net.Http.HttpClient();
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("HEAD");
    
                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);
    
                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, CancellationToken.None).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value, StringComparer.OrdinalIgnoreCase);
                        foreach (var item_ in response_.Content.Headers)
                            headers_[item_.Key.ToLowerInvariant()] = item_.Value;
    
                        ProcessResponse(client_, response_);
    
                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            return new SessionStatus
                            {
                                SessionId = sessionId,
                                Status = (SessionResponseDtoStatus)Enum.Parse(typeof (SessionResponseDtoStatus), headers_["nexosis-session-status"].First())
                            };
                        }
                        else
                        if (status_ == "404") 
                        {
                            var responseData_ = await response_.Content.ReadAsStringAsync().ConfigureAwait(false); 
                            throw new SwaggerException("Specified session was not found", status_, responseData_, headers_, null);
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = await response_.Content.ReadAsStringAsync().ConfigureAwait(false); 
                            throw new SwaggerException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", status_, responseData_, headers_, null);
                        }

                        return default(SessionStatus);
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (client_ != null)
                    client_.Dispose();
            }
        }

        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request,
            System.Text.StringBuilder urlBuilder)
        {
            if (!request.Headers.Contains("api-key"))
                request.Headers.Add("api-key", _apiKey);

            request.Headers.Add("User-Agent", $"Nexosis C# API Client/1.0");
        }

#if DEBUG
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url)
        {
            Console.WriteLine($"Requesting: {url}");
        }
#endif

        partial void ProcessResponse(HttpClient client, HttpResponseMessage response)
        {
            
        }

        private SessionType ToSessionType(object otherEnum)
        {
            if (otherEnum != null && otherEnum.GetType().GetTypeInfo().IsEnum)
            {
                var valueName = Enum.GetName(otherEnum.GetType(), otherEnum);
                return (SessionType) Enum.Parse(typeof(SessionType), valueName);
            }
            else
            {
                throw new InvalidOperationException("The object provided should be of type enum");
            }
        }

    }

    public class SessionStatus
    {
        public Guid SessionId { get; set; }
        public SessionResponseDtoStatus Status { get; set; }
    }

    public class Session 
    {
        public Session(SessionResponseDto source)
        {
            SessionId = source.SessionId.Value;
            DataSetName = source.DataSetName;
            PredictionStartDate = source.PredictionStartDate.Value;
            PredictionEndDate = source.PredictionEndDate.Value;
            TargetColumn = source.TargetColumn;
            Status = source.Status;
            Cost = source.Cost;
            Balance = source.Balance;
        }

        public string TargetColumn { get; set; }

        public DateTime PredictionEndDate { get; set; }

        public DateTime PredictionStartDate { get; set; }

        public string DataSetName { get; set; }

        public Guid SessionId { get; set; }

        public SessionResponseDtoStatus? Status { get; set; }

        public string Cost { get; set; }

        public string Balance { get; set; }
    }


    public class SessionData
    {
        public string DataSetName { get; set; }

        [Newtonsoft.Json.JsonProperty("predictionStartDate", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTime? StartDate
        {
            get; set;
        }

        [Newtonsoft.Json.JsonProperty("predictionEndDate", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTime? EndDate
        {
            get; set;
        }
    }

    public class DataSet 
    {
        SessionData Session { get; set; }

        public List<DataSetRow> Data
        {
            get; set;
        }

        internal DataSetDataDto ToDto()
        {
            return new DataSetDataDto
            {
                Data = new ObservableCollection<DataSetRow>(this.Data)
            };
        }
    }

    public enum SessionType
    {
        [System.Runtime.Serialization.EnumMember(Value = "import")]
        Import = 0,

        [System.Runtime.Serialization.EnumMember(Value = "forecast")]
        Forecast = 1,

        [System.Runtime.Serialization.EnumMember(Value = "impact")]
        Impact = 2,
    }
}
