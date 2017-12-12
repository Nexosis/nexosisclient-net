using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    /// <summary>
    /// The parameters needed to import data into the Nexosis API from a URL
    /// </summary>
    public class ImportFromUrlRequest : ImportRequest
    {
        /// <summary>
        /// The URL to retrieve
        /// </summary>
        public string Url { get; set; }


        /// <summary>
        /// Authentication parameters to be used to retrieve the data at the given url
        /// </summary>
        public ImportFromUrlAuthentication Auth { get; set; }
    }

    /// <summary>
    /// Url Authentication parameters
    /// </summary>
    public class ImportFromUrlAuthentication
    {
        /// <summary>
        /// Basic Authentication
        /// </summary>
        public BasicAuthentication Basic { get; set; }
    }

    public class BasicAuthentication
    {
        /// <summary>
        /// If the url is secured by basic authentication, use this username to authenticate
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// If the url is secured by basic authentication, use this password to authenticate
        /// </summary>
        public string Password { get; set; }
    }
}