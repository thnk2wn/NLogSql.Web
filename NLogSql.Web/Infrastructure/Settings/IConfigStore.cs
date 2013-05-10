using System;

namespace NLogSql.Web.Infrastructure.Settings
{
    public interface IConfigStore
    {
        object GetSetting(string key);

        DateTime GetDateSetting(string key);
        int GetIntSetting(string key);
        bool GetBoolSetting(string key);
        string GetStringSetting(string key);

        void SetSeting(string key, object value);

        bool RequireByDefault { get; set; }
    }
}
