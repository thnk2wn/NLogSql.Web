using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NLogSql.Web.Infrastructure.Extensions.System_;

namespace NLogSql.Web.Infrastructure.Extensions.System_Web_Mvc_
{
    public static class ModelStateDictionaryExtensions
    {
        public static IList<string> GetModelErrors(this ModelStateDictionary dict)
        {
            var modelErrors = dict.Keys.SelectMany(k => dict[k].Errors)
                .Select(m => m.ErrorMessage)
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .ToList();
            return modelErrors;
        }

        public static string GetModelErrorText(this ModelStateDictionary dict, string delimiter)
        {
            return delimiter.SmartJoin(dict.GetModelErrors().ToArray());
        }
    }
}