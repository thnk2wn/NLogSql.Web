namespace NLogSql.Web.Infrastructure.Settings
{
    public partial class AppSettings
    {
        private readonly IConfigStore _configStore;

        public AppSettings(IConfigStore configStore = null)
        {
            _configStore = configStore ?? new AppConfigStore();
            Email = new EmailSettings(new AppConfigStore("emailSettings"));
        }

        private static readonly object SyncRoot = new object();
        private static AppSettings _default = new AppSettings(new AppConfigStore());

        public static AppSettings Default
        {
            get
            {
                return _default;
            }
            set
            {
                lock (SyncRoot)
                {
                    _default = value;
                }
            }
        }

        public IConfigStore ConfigStore
        {
            get
            {
                return _configStore;
            }
        }

        public EmailSettings Email { get; private set; }
    }
}