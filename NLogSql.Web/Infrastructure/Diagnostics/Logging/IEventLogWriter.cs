using System;

namespace NLogSql.Web.Infrastructure.Diagnostics.Logging
{
    public interface IEventLogWriter
    {
        bool TryWriteError(Exception ex);
        bool TryWriteError(string errorText, params object[] formatArgs);
        bool TryWriteInfo(string infoText, params object[] formatArgs);
        string LastError { get; }
    }
}
