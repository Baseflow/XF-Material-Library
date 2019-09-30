using MaterialMvvmSample.ViewModels;
using Xamarin.Forms.Xaml;

namespace MaterialMvvmSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecondView : BaseSecondView
    {
        public SecondView()
        {
            InitializeComponent();
        }
    }

    public abstract class BaseSecondView : BaseView<SecondViewModel> { }
}