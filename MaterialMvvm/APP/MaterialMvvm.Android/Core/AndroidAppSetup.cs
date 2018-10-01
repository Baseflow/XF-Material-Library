using Autofac;
using MaterialMvvm.Core;
using MaterialMvvm.Android.Database;
using MaterialMvvm.Repositories.Database;

namespace MaterialMvvm.Android.Core
{
    public class AndroidAppSetup : AppSetup
    {
        protected override void RegisterDependencies(ContainerBuilder builder)
        {
            base.RegisterDependencies(builder);

            builder.RegisterType<AndroidDatabaseConnection>().As<IPlatformDatabaseConnection>().SingleInstance();

            //Register platform-specific services
        }
    }
}