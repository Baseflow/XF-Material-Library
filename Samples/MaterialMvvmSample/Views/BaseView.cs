using MaterialMvvmSample.ViewModels;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace MaterialMvvmSample.Views
{
    public abstract class BaseView<TViewModel> : ContentPage where TViewModel : BaseViewModel
    {
        protected TViewModel ViewModel { get; }

        protected BaseView()
        {
            ViewModel = CommonServiceLocator.ServiceLocator.Current.GetInstance<TViewModel>();
            BindingContext = ViewModel;
            On<iOS>().SetUseSafeArea(true);
        }
    }
}
