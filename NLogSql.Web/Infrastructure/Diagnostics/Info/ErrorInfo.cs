using System;

namespace NLogSql.Web.Infrastructure.Diagnostics.Info
{
    public class ErrorInfo : DiagnosticInfoBase
    {
        private readonly Exception _exception;

        public ErrorInfo(Exception exception)
        {
            _exception = exception;
        }

        protected override void GenerateReport()
        {
            AppendHtml("<div style='color: red;'>");
            AppendLine(SafeUnformattedMessage(_exception.Message));
            AppendHtml("</div>");
            AppendLine();

            AppendLine(SafeUnformattedMessage(_exception.ToString()));
            AppendLine();
            AppendLine("Source: {0}", _exception.Source);
        }
    }
}