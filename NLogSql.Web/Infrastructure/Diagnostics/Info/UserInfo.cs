using System.Threading;
using System.Web;

namespace NLogSql.Web.Infrastructure.Diagnostics.Info
{
    public class UserInfo : DiagnosticInfoBase
    {
        private readonly HttpRequestBase _request;

        public UserInfo(HttpRequestBase request)
        {
            _request = request;
        }

        protected override void GenerateReport()
        {
            var isAuthenticated = Thread.CurrentPrincipal.Identity.IsAuthenticated;
            var identityName = Thread.CurrentPrincipal.Identity.Name ?? string.Empty;

            StartTable();

            AppendRow("Authenticated?", isAuthenticated);

            if (isAuthenticated)
                AppendRow("Identity", identityName);

            AppendRow("User Agent", _request.UserAgent);

            if (isAuthenticated)
            {
                //TODO: create custom user principal with extended fields and adjust register/login
                //TODO: capture custom user principal fields here
            }

            EndTable();
        }
    }
}