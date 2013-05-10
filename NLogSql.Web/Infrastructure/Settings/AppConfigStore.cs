using System;
using System.Collections.Specialized;
using System.Configuration;

namespace NLogSql.Web.Infrastructure.Settings
{
    public class AppConfigStore : IConfigStore
    {
        private readonly string _sectionName;
        private readonly NameValueCollection _sectionCollection ;

        public AppConfigStore() : this(null)
        {
        }

        public AppConfigStore(string sectionName)
        {
            _sectionName = sectionName;

            if (!string.IsNullOrWhiteSpace(_sectionName))
            {
                _sectionCollection = (NameValueCollection)ConfigurationManager.GetSection(_sectionName);
            }
        }

        public object GetSetting(string key)
        {
            var setting =  (null != _sectionCollection) 
                ? _sectionCollection[key]
                : ConfigurationManager.AppSettings[key];

            if (RequireByDefault)
            {
                if (null == setting)
                    throw new NullReferenceException(string.Format("Failed to find key '{0}' in app settings", key));
            }
            return setting;
        }

        public DateTime GetDateSetting(string key)
        {
            return Convert.ToDateTime(GetSetting(key));
        }

        public int GetIntSetting(string key)
        {
            return Convert.ToInt32(GetSetting(key));
        }

        public bool GetBoolSetting(string key)
        {
            return Convert.ToBoolean(GetSetting(key));
        }

        public string GetStringSetting(string key)
        {
            return Convert.ToString(GetSetting(key));
        }

        public void SetSeting(string key, object value)
        {
            if (null != _sectionCollection)
                _sectionCollection[key] = (string) value;
            else 
                ConfigurationManager.AppSettings[key] = (string)value;
        }

        public bool RequireByDefault { get; set; }
    }
}