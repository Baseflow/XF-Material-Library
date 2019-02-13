using MaterialMvvmSample.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using XF.Material.Forms.UI;
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
            //await MaterialDialog.Instance.InputAsync("Enter password", "Enter your current password to proceed");
            //await MaterialDialog.Instance.AlertAsync("This is an alert dialog. It displays to the user the current context.", "Alert Dialog");
            await MaterialDialog.Instance.SelectChoicesAsync("Select an item", new string[] 
            {
                "Company 1",
                "Company 2",
                "Company 3",
                "Company 4",
                "Company 5",
                "Company 6"
            });
        }

        private void MaterialTextField_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            (sender as MaterialTextField).HasError = string.IsNullOrEmpty(e.NewTextValue);
        }
    }

    public abstract class BaseMainView : BaseView<MainViewModel> { }
}
