using Autofac;
using MaterialMvvm.Core;
using MaterialMvvm.iOS.Database;
using MaterialMvvm.Repositories.Database;

namespace MaterialMvvm.iOS.Core
{
    public class iOSAppSetup : AppSetup
    {
        protected override void RegisterDependencies(ContainerBuilder builder)
        {
            base.RegisterDependencies(builder);

            builder.RegisterType<iOSDatabaseConnection>().As<IPlatformDatabaseConnection>().SingleInstance();

            //Register platform-specific services
        }
    }
}