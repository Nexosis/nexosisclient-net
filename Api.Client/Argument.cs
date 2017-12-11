using System;
using System.Linq;

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

        public static void OneOfIsNotNullOrEmpty(params Tuple<string, string>[] args)
        {
            if (args.All(a => string.IsNullOrEmpty(a.Item1)))
            {
                var keys = string.Join(",", args.Select(a => a.Item2));
                throw new ArgumentException($"One of {keys} should not be null or empty");
            }
        }
    }
}
