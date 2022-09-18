using MaterialMvvmSample.ViewModels;

namespace MaterialMvvmSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : BaseMainView
    {
        public MainView()
        {
            InitializeComponent();
        }
    }

    public abstract class BaseMainView : BaseView<MainViewModel> { }
}
