using MaterialMvvmSample.ViewModels;
using System.Collections.Generic;
using XF.Material.Forms.UI.Dialogs;

namespace MaterialMvvmSample.Views
{
    public partial class MainView : BaseMainView
    {
        public MainView()
        {
            this.InitializeComponent();
        }

        private async void MaterialButton_Clicked(object sender, System.EventArgs e)
        {
            await MaterialDialog.Instance.InputAsync("Enter password", "Enter your current password to proceed");
        }
    }

    public abstract class BaseMainView : BaseView<MainViewModel> { }
}
