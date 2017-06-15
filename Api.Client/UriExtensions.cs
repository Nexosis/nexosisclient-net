using System;
using System.Collections.Generic;
using System.Linq;

namespace Nexosis.Api.Client
{
    internal static class UriExtensions
    {
        /// <summary>
        /// Merge a dictionary of values with an existing <see cref="Uri"/>
        /// </summary>
        /// <param name="uri">Original request Uri</param>
        /// <param name="parameters">Collection of key-value pairs</param>
        /// <returns>Updated request Uri</returns>
        /// <remarks>Modified from https://github.com/octokit/octokit.net/blob/master/Octokit/Helpers/UriExtensions.cs</remarks>
        public static Uri AddParameters(this Uri uri, IList<KeyValuePair<string,string>> parameters)
        {
            if (parameters == null || !parameters.Any()) return uri;

            var hasQueryString = uri.OriginalString.IndexOf("?", StringComparison.Ordinal);
            string baseUri;
            if (hasQueryString != -1)
            {
                baseUri = uri.OriginalString.Substring(0, hasQueryString);
                var queryString = uri.OriginalString.Substring(hasQueryString);
                var values = queryString.Replace("?", "").Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

                var existingParameters = values.ToDictionary(key => key.Substring(0, key.IndexOf('=')), value => value.Substring(value.IndexOf('=') + 1));

                foreach (var existing in existingParameters)
                {
                    if (!parameters.Contains(existing))
                    {
                        parameters.Add(existing);
                    }
                }
            }
            else
            {
                baseUri = uri.OriginalString;
            }

            string query = string.Join("&", parameters.Select(kvp => kvp.Key + "=" + Uri.EscapeDataString(kvp.Value)));
            return new Uri(baseUri + "?" + query);
        }
    }
}
