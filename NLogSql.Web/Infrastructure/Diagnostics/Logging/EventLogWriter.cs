using System;
using System.Diagnostics;
using System.Reflection;

namespace NLogSql.Web.Infrastructure.Diagnostics.Logging
{
    public class EventLogWriter : IEventLogWriter
    {
        public bool TryWriteError(Exception ex)
        {
            return TryWriteError(ex.ToString());
        }

        public bool TryWriteError(string errorText, params object[] formatArgs)
        {
            return TryWrite(errorText, EventLogEntryType.Error, formatArgs);
        }

        public bool TryWriteInfo(string infoText, params object[] formatArgs)
        {
            return TryWrite(infoText, EventLogEntryType.Information, formatArgs);
        }

        public string LastError { get; private set; }

        private static void EnsureSourceExists()
        {
            if (!EventLog.SourceExists(EventLogSource))
            {
                EventLog.CreateEventSource(EventLogSource, "Application");
            }
        }

        private static string EventLogSource
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Name;
            }
        }

        private bool TryWrite(string text, EventLogEntryType type, params object[] formatArgs)
        {
            // 32766 is the supposed limit but just under still seems to trigger error
            const int maxLen = 30000;
            try
            {
                EnsureSourceExists();
                var textResolved = string.Format(text, formatArgs);

                if (textResolved.Length > maxLen)
                    textResolved = textResolved.Substring(0, maxLen - 3) + "...";

                EventLog.WriteEntry(EventLogSource, textResolved, type);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Failed to write to event log. Error: {0}. Message: {1}", ex, text));
                this.LastError = ex.Message;
                return false;
            }

            return true;
        }
    }
}