using System;
using System.Linq;
using System.Web;
using NLogSql.Web.Infrastructure.Extensions.System_;

namespace NLogSql.Web.Infrastructure.Diagnostics.Info
{
    public class FormInfo : DiagnosticInfoBase
    {
        private readonly HttpRequestBase _request;

        public FormInfo(HttpRequestBase request)
        {
            _request = request;
        }

        protected override void GenerateReport()
        {
            StartTable();

            var keys = _request.Form.AllKeys.OrderBy(s => s).ToList();

            foreach (var name in keys)
            {
                var value = _request.Form[name];

                if (null != value && name.Contains("password", StringComparison.OrdinalIgnoreCase))
                {
                    value = new string('*', value.Length);
                }

                AppendRow(name, value);
            }

            EndTable();
        }
    }
}