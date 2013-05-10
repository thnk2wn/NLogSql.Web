using System;

namespace NLogSql.Web.Models
{
    public class ErrorModel
    {
        public string RequestedUrl { get; set; }
        public string ReferrerUrl { get; set; }
        public Exception Exception { get; set; }
    }
}