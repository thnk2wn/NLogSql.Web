using System.Net;
using System.Net.Mail;

namespace NLogSql.Web.Infrastructure.Settings
{
    partial class AppSettings
    {
        public class EmailSettings
        {
            public IConfigStore ConfigStore { get; set; }

            public EmailSettings(IConfigStore configStore)
            {
                ConfigStore = configStore;
            }

            public MailAddress SystemFromAddress
            {
                get
                {
                    return new MailAddress(
                        ConfigStore.GetStringSetting("SystemFromAddress"),
                        ConfigStore.GetStringSetting("SystemFromName"));
                }
            }

            public string SmtpServer
            {
                get
                {
                    return ConfigStore.GetStringSetting("SmtpServer");
                }
            }

            public int SmtpPort
            {
                get
                {
                    return ConfigStore.GetIntSetting("SmtpPort");
                }
            }

            public string CredUser
            {
                get
                {
                    return ConfigStore.GetStringSetting("CredUser");
                }
            }

            public string CredPass
            {
                get
                {
                    return ConfigStore.GetStringSetting("CredPass");
                }
            }

            public bool EnableSSL
            {
                get { return ConfigStore.GetBoolSetting("EnableSSL"); }
            }

            public NetworkCredential Credentials
            {
                get
                {
                    var cred = (!string.IsNullOrWhiteSpace(CredUser) &&
                                 !string.IsNullOrWhiteSpace(CredPass))
                                   ? new NetworkCredential(CredUser, CredPass)
                                   : null;
                    return cred;
                }
            }

            public string ErrorMessageTo
            {
                get { return ConfigStore.GetStringSetting("ErrorMsgTo"); }
            }
        }
    }
}