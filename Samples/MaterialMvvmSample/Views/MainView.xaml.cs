using MaterialMvvmSample.ViewModels;
using Xamarin.Forms.Xaml;

namespace MaterialMvvmSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : BaseMainView
    {
        public MainView()
        {
            this.InitializeComponent();
        }
    }

    public abstract class BaseMainView : BaseView<MainViewModel> { }
}
