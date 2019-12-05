using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace MaterialMvvmSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialDialogsView : ContentPage
    {
        public MaterialDialogsView()
        {
            InitializeComponent();
            BindingContext = new MaterialDialogsViewModel();

            this.Opendialog.Clicked += Opendialog_Clicked;
            this.OpenSimpleDialog.Clicked += OpenSimpleDialog_Clicked;
            OpenConfirmDialog();
        }

        private async Task OpenConfirmDialog()
        {
            await MaterialDialog.Instance.ConfirmAsync(message: "Do you want to sign in?",
                                                         confirmingText: "Sign In");
        }

        private async void OpenSimpleDialog_Clicked(object sender, EventArgs e)
        {
            await MaterialDialog.Instance.SelectActionAsync(new List<string>
            {
                "action"
            });
        }

        private async void Opendialog_Clicked(object sender, EventArgs e)
        {
            var choices = new List<string>
            {
                "choice 1",
                "choice 2",
                "choice 3",
            };

            var choice = await MaterialDialog.Instance.SelectChoiceAsync("Select choices", choices);

            if (choice < 0)
                return;

            this.DialogResult.Text = choices[choice];
        }

        private async void OpenAlertDialog_Clicked(object sender, EventArgs e)
        {
            var result = await MaterialDialog.Instance.ConfirmAsync("Message", "Title", "Confirm", "Dismiss");
            if (result.HasValue)
            {
                if (result.Value)
                    Debug.WriteLine("Confirm");
                else
                    Debug.WriteLine("Dimiss");
            }
            else
            {
                Debug.WriteLine("Closed");
            }
        }
    }
}
