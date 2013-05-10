using System;
using System.Linq;
using System.Text;
using System.Web;

namespace NLogSql.Web.Infrastructure.Diagnostics.Info
{
    public class ServerInfo : DiagnosticInfoBase
    {
        private readonly HttpRequestBase _request;

        public ServerInfo(HttpRequestBase request)
        {
            _request = request;
        }

        public string HostName { get; private set; }

        protected override void GenerateReport()
        {
            HostName = GetMachineName(_request.RequestContext.HttpContext);

            StartTable();

            AppendRow("Host", HostName);
            AppendLine();

            // these values we already have and just add to the noise
            var keys = _request.ServerVariables.AllKeys.Where(key =>
                !key.StartsWith("ALL_") &&
                key != "HTTP_COOKIE" &&
                key != "HTTP_USER_AGENT")
                .OrderBy(s => s).ToList();

            foreach (var name in keys)
            {
                string[] values = null;
                try
                {
                    values = _request.ServerVariables.GetValues(name);
                }
                catch { }

                if (null != values)
                {
                    var sb = new StringBuilder();
                    for (var j = 0; j < values.Length; j++)
                    {
                        if (!string.IsNullOrWhiteSpace(values[j]))
                        {
                            sb.Append(values[j]);
                            if (j + 1 < values.Length)
                                sb.Append(", ");
                        }
                    }

                    if (sb.Length > 0)
                        AppendRow(name, sb.ToString());
                }

                AppendText(Environment.NewLine);
            }

            EndTable();
        }

        public static string GetMachineName(HttpContextBase context)
        {
            if (context != null) 
            {
                try { return context.Server.MachineName; }
                catch (Exception) {}
            }

            try { return Environment.MachineName; }
            catch (Exception) { }

            return string.Empty;
        }
    }
}