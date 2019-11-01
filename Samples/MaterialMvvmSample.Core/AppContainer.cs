using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using MaterialMvvmSample.Utilities;
using MaterialMvvmSample.Utilities.Dialogs;
using MaterialMvvmSample.ViewModels;
using MaterialMvvmSample.Views;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace MaterialMvvmSample.Core
{
    public abstract class AppContainer
    {
        public void Setup()
        {
            var containerBuilder = new ContainerBuilder();

            RegisterServices(containerBuilder);

            var container = containerBuilder.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));

            container.BeginLifetimeScope();
        }

        protected virtual void RegisterServices(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<App>().SingleInstance();

            containerBuilder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            containerBuilder.RegisterType<JobDialogService>().As<IJobDialogService>().InstancePerDependency();

            containerBuilder.RegisterType<MainView>().Named<Page>(ViewNames.MainView).As<MainView>().InstancePerDependency();
            containerBuilder.RegisterType<ChipFontSizeView>().Named<Page>(ViewNames.ChipFontSizeView).As<ChipFontSizeView>().InstancePerDependency();
            containerBuilder.RegisterType<SecondView>().Named<Page>(ViewNames.SecondView).As<SecondView>().InstancePerDependency();
            containerBuilder.RegisterType<LandingView>().Named<Page>(ViewNames.LandingView).As<LandingView>().InstancePerDependency();
            containerBuilder.RegisterType<MaterialDialogsView>().Named<Page>(ViewNames.MaterialDialogsView).As<MaterialDialogsView>().InstancePerDependency();
            containerBuilder.RegisterType<MaterialTextFieldView>().Named<Page>(ViewNames.MaterialTextFieldView).As<MaterialTextFieldView>().InstancePerDependency();
            containerBuilder.RegisterType<MaterialCircularView>().Named<Page>(ViewNames.MaterialCircularView).As<MaterialCircularView>().InstancePerDependency();
            containerBuilder.RegisterType<CheckboxesView>().Named<Page>(ViewNames.CheckboxesView).As<CheckboxesView>().InstancePerDependency();
            containerBuilder.RegisterType<MaterialMenuButtonView>().Named<Page>(ViewNames.MaterialMenuButtonView).As<MaterialMenuButtonView>().InstancePerDependency();
            containerBuilder.RegisterType<MaterialCardView>().Named<Page>(ViewNames.MaterialCardView).As<MaterialCardView>().InstancePerDependency();

            containerBuilder.RegisterType<MainViewModel>().InstancePerDependency();
            containerBuilder.RegisterType<SecondViewModel>().InstancePerDependency();
            containerBuilder.RegisterType<LandingViewModel>().InstancePerDependency();
            containerBuilder.RegisterType<CheckboxesViewModel>().InstancePerDependency();
            containerBuilder.RegisterType<ChipFontSizeViewModel>().InstancePerDependency();
            containerBuilder.RegisterType<MaterialCircularViewModel>().InstancePerDependency();
            containerBuilder.RegisterType<MaterialDialogsViewModel>().InstancePerDependency();
            containerBuilder.RegisterType<MaterialTextFieldViewModel>().InstancePerDependency();
            containerBuilder.RegisterType<MaterialCardViewModel>().InstancePerDependency();

        }
    }
}
