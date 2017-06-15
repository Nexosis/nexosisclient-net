using System;
using System.Net.Http;

namespace Nexosis.Api.Client
{
    internal static class Argument
    {
        public static void IsNotNull(object value, string name)
        {
            if (value == null)
                throw new ArgumentNullException(name);
        }

        public static void IsNotNullOrEmpty(string value, string name)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be null or empty.", name);
        }

    }
}
