using System.ComponentModel;
using System.Globalization;
using NLog;

namespace NLogSql.Web.Infrastructure.Diagnostics.Logging
{
    public class Log : Logger, ILog
    { 
        public void Write(LogType type, object properties, string message, params object[] args)
        {
            var info = new LogEventInfo(LogLevel.FromOrdinal((int)type), Name, CultureInfo.CurrentCulture, message, args);

            if (null != properties)
            {
                foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(properties))
                {
                    var value = propertyDescriptor.GetValue(properties);
                    info.Properties[propertyDescriptor.Name] = value;
                }
            }

            Log(info);
        }
    }

    public enum LogType
    {
        Trace, Debug, Info, Warn, Error, Fatal
    }
}