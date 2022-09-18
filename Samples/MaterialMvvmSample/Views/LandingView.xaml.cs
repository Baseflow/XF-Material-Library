using MaterialMvvmSample.ViewModels;

namespace MaterialMvvmSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingView : ContentPage
    {
        public LandingView()
        {
            InitializeComponent();
            BindingContext = new LandingViewModel();

        }
    }


    //public abstract class BaseLandingView : BaseView<LandingViewModel> { }
}
