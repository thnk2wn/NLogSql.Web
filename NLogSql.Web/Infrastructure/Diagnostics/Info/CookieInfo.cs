using System;
using System.Linq;
using System.Web;

namespace NLogSql.Web.Infrastructure.Diagnostics.Info
{
    public class CookieInfo : DiagnosticInfoBase
    {
        private readonly HttpRequestBase _request;

        public CookieInfo(HttpRequestBase request)
        {
            _request = request;
        }

        protected override void GenerateReport()
        {
            StartTable();

            var keys = _request.Cookies.AllKeys.OrderBy(s => s).ToList();

            for (var i = 0; i < keys.Count; i++)
            {
                var cookie = _request.Cookies[keys[i]];

                if (null != cookie)
                {
                    AppendRow(cookie.Name, cookie.Value);

                    if (i + 1 < keys.Count)
                        AppendText(Environment.NewLine);
                }
            }

            EndTable();
        }
    }
}