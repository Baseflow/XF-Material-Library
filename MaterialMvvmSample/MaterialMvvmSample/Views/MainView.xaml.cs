using MaterialMvvmSample.ViewModels;

namespace MaterialMvvmSample.Views
{
    public partial class MainView : BaseMainView
    {
        public MainView()
        {
            this.InitializeComponent();
        }
    }

    public abstract class BaseMainView : BaseView<MainViewModel> { }
}
