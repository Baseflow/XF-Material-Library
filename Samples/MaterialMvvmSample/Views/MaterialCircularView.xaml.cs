using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace MaterialMvvmSample.Views
{
    public partial class MaterialCircularView : ContentPage
    {
        public MaterialCircularView()
        {
            InitializeComponent();
            BindingContext = new MaterialCircularViewModel();
            RunLoadingDialog();
        }

        public async Task RunLoadingDialog()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Something is running");
            await Task.Delay(5000); // Represents a task that is running.
            await loadingDialog.DismissAsync();
        }
    }
}
