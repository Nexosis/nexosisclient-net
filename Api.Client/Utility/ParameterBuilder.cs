using System;
using System.Collections.Generic;
using Nexosis.Api.Client.Model;

namespace Nexosis.Api.Client.Utility
{
    internal class ParameterBuilder
    {
        private readonly List<KeyValuePair<string, string>> parameters;

        public ParameterBuilder()
        {
            this.parameters = new List<KeyValuePair<string, string>>();
        }

        public void Add(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                parameters.Add(new KeyValuePair<string, string>(key, value));
            }
        }

        public void Add<T>(string key, T? value) where T : struct
        {
            if (value.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>(key, value.ToString()));
            }
        }

        public void Add(string key, DateTimeOffset? value)
        {
            if (value.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>(key, value.Value.ToString("O")));
            }
        }

        public void Add(PagingInfo paging)
        {
            if (paging != null)
            {
                Add("page", paging.PageNumber);
                Add("pageSize", paging.PageSize);
            }
        }

        public List<KeyValuePair<string, string>> GetParameters()
        {
            return parameters;
        }
    }
}