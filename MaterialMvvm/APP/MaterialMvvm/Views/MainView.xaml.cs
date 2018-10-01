using MaterialMvvm.ViewModels;
using Xamarin.Forms;

namespace MaterialMvvm.Views
{
    public partial class MainView : BaseMainView
    {
        public MainView()
        {
            this.InitializeComponent();
        }
    }

    public class BaseMainView : BaseView<MainViewModel> { }
}
