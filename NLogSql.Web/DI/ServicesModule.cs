using NLogSql.Services;
using NLogSql.Services.Mapping;
using NLogSql.Web.Infrastructure.Messaging;
using Ninject.Modules;

namespace NLogSql.Web.DI
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMusicService>().To<MusicService>();
            Bind<IMailer>().To<Mailer>();

            Bind<IMappingService>().To<MappingService>();
        }
    }
}