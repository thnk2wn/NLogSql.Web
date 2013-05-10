using System;
using System.Linq;

namespace NLogSql.Web.Infrastructure.Extensions.System_
{
    public static class StringExtensions
    {
        public static string SmartJoin(this string separator, params string[] items)
        {
            return String.Join(separator, items.Where(x => !String.IsNullOrEmpty(x)).ToArray());
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}