using Autofac;
using MaterialMvvmSample.Core;

namespace MaterialMvvmSample.Droid.Core
{
    public class AndroidAppContainer : AppContainer
    {
        protected override void RegisterServices(ContainerBuilder containerBuilder)
        {
            base.RegisterServices(containerBuilder);

            //Register platform-specific services
        }
    }
}