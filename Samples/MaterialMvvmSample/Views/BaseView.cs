using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MaterialMvvmSample.Views
{
    public abstract class BaseView<TViewModel> : ContentPage where TViewModel : BaseViewModel
    {
        protected TViewModel ViewModel { get; }

        protected BaseView()
        {
            this.ViewModel = CommonServiceLocator.ServiceLocator.Current.GetInstance<TViewModel>();
            this.BindingContext = this.ViewModel;
            this.On<iOS>().SetUseSafeArea(true);
        }
    }
}
