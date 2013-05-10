using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using NLogSql.Web.Infrastructure.Diagnostics.Logging;

namespace NLogSql.Web.Infrastructure.Diagnostics.Info
{
    public abstract class DiagnosticInfoBase
    {
        private readonly StringBuilder _sbHtml;
        private readonly StringBuilder _sbText;

        private ILog _log;

        private static readonly string HtmlBreak = "<br/>" + Environment.NewLine;

        private const string ThStyle =
            "text-align: left; background: #FFFFCC; border-bottom: thin solid black; padding: 8px; font-family: Helvetica; font-size: 9pt; width: 25%;";

        private const string TdStyle =
            "border-bottom: thin solid black; padding: 8px; font-family: Helvetica; font-size: 9pt"; 

        private readonly List<string> _errors;

        protected DiagnosticInfoBase()
        {
            _sbHtml = new StringBuilder();
            _sbText = new StringBuilder();
            _errors = new List<string>();
            // don't call generate here
        }

        private ILog Log
        {
            get
            {
                if (null != _log) return _log;
                _log = AppLogFactory.Create(GetType().FullName);
                return _log;
            }
        }

        // error messages themselves might have {0} etc. or might have 'input string in incorrect format'
        private void SafeAppend(StringBuilder sb, string format, params object[] args)
        {
            try
            {
                // technically this should be fine. being extra cautious
                if (null == args || !args.Any())
                    sb.Append(format);
                else
                    sb.AppendFormat(format, args);
            }
            catch (Exception ex)
            {
                if (null != Log)
                {
                    var msg = string.Format("Error logging the following with {0} arg(s): {1}. Error was: {2}",
                                            null != args ? args.Length : 0, format, ex.Message);
                    Log.Error(msg);
                    _errors.Add(msg);
                }
            }
        }

        protected void Append(string format, params object[] args)
        {
            SafeAppend(_sbHtml, format, args);
            SafeAppend(_sbText, format, args);
        }

        protected void AppendLine(string format, params object[] args)
        {
            SafeAppend(_sbHtml, format + HtmlBreak, args);
            SafeAppend(_sbText, format + Environment.NewLine, args);
        }

        protected void AppendInfo(DiagnosticInfoBase info)
        {
            if (!info.IsGenerated) info.Generate();
            _sbHtml.Append(info.ReportHtml);
            _sbText.Append(info.ReportText);

            if (info.Errors.Any())
                _errors.AddRange(info.Errors);
        }

        protected void AppendHtml(string content)
        {
            _sbHtml.AppendLine(content);
        }

        protected void AppendRow(string label, Uri value)
        {
            AppendRowInternal(label, value.ToString(), string.Format(@"<a href='{0}'>{0}</a>", value));
        }

        protected void AppendRow(string label, object value)
        {
            AppendRowInternal(label, value, value);
        }

        private void AppendRowInternal(string label, object value, object htmlValue)
        {
            _sbHtml.AppendLine("<tr>");
            SafeAppend(_sbHtml, "<th style='{0}'>{1}</th>{2}", ThStyle, label, Environment.NewLine);
            SafeAppend(_sbHtml, "<td style='{0}'>{1}</td>{2}", TdStyle, htmlValue, Environment.NewLine);
            _sbHtml.AppendLine("</tr>");

            _sbText.AppendFormat("{0}: {1}{2}", label, value, Environment.NewLine);
        }

        protected void StartTable()
        {
            SafeAppend(_sbHtml, "<table border='0' cellspacing='0' cellpadding='0' style='table-layout: fixed; width: 100%;'>{0}", Environment.NewLine);
        }

        protected void EndTable()
        {
            SafeAppend(_sbHtml, "</table>{0}", Environment.NewLine);
        }

        protected void AppendLine()
        {
            _sbHtml.Append(HtmlBreak);
            _sbText.AppendLine();
        }

        protected void AppendText(string text)
        {
            _sbText.Append(text);
        }

        protected void StartSection(string sectionName)
        {
            _sbHtml.AppendFormat("<div style='font-size:12pt; font-weight:bold;'>{0}</div>{1}", sectionName, Environment.NewLine);
            _sbHtml.Append("<hr>" + Environment.NewLine);
            _sbText.AppendFormat("{0}{1}", sectionName, Environment.NewLine);
            _sbText.AppendLine(new string('=', 80));
        }

        protected void EndSection()
        {
            _sbHtml.AppendLine("<br/><br/>");
            _sbText.AppendLine();
            _sbText.AppendLine();
        }

        public bool IsGenerated { get; private set; }

        public string Generate()
        {
            try
            {
                _sbHtml.Clear();
                _sbText.Clear();
                GenerateReport();
                IsGenerated = true;
            }
            catch (Exception ex)
            {
                GenerationError = ex;

                if (null != Log)
                {
                    Log.Error(ex.ToString());
                    _errors.Add(ex.Message);
                }
            }

            return ReportHtml;
        }

        public ReadOnlyCollection<string> Errors
        {
            get { return new ReadOnlyCollection<string>(_errors); }
        }

        public Exception GenerationError { get; private set; }

        public string ReportHtml
        {
            get { return _sbHtml.ToString(); }
        }

        public string ReportText
        {
            get { return _sbText.ToString(); }
        }

        protected string SafeUnformattedMessage(string msg)
        {
            var safeMsg = msg;
            if (null != safeMsg)
                safeMsg = safeMsg.Replace("{", "{{").Replace("}", "}}");
            return safeMsg;
        }

        protected abstract void GenerateReport();

        public bool IsEmpty
        {
            get
            {
                var text = null != ReportText ? ReportText.Trim() : null;
                return string.IsNullOrWhiteSpace(text);
            }
        }
    }
}