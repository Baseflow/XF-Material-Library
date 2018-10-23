using Autofac;
using MaterialMvvmSample.Core;

namespace MaterialMvvmSample.iOS.Core
{
    public class PlatformContainer : AppContainer
    {
        protected override void RegisterServices(ContainerBuilder containerBuilder)
        {
            base.RegisterServices(containerBuilder);

            //Register platform-specific services
        }
    }
}