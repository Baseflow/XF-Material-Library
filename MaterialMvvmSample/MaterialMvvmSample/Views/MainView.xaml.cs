using MaterialMvvmSample.ViewModels;

namespace MaterialMvvmSample.Views
{
    public partial class MainView : BaseMainView
    {
        public MainView()
        {
            this.InitializeComponent();
        }

        private void MaterialButton_Clicked(object sender, System.EventArgs e)
        {
            //b.IsEnabled = !b.IsEnabled;
        }
    }

    public abstract class BaseMainView : BaseView<MainViewModel> { }
}
