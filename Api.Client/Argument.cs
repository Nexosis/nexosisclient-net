using System;

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
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be null or empty.", name);
        }

    }
}
