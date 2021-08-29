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

            Opendialog.Clicked += Opendialog_Clicked;
            OpenSimpleDialog.Clicked += OpenSimpleDialog_Clicked;
            OpenConfirmDialog();

            MaterialDailogMultipleMessage();
        }

        private async Task MaterialDailogMultipleMessage()
        {
            using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Something is running"))
            {
                await Task.Delay(5000); // Represents a task that is running.
                dialog.MessageText = "Something else is running now!";
                await Task.Delay(5000); // Represents a task that is running.
                dialog.MessageText = "Something else is running now aswell!";
                await Task.Delay(5000); // Represents a task that is running.
                dialog.MessageText = "Something else is running now last time!";
                await Task.Delay(5000); // Represents a task that is running.
            };
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
            {
                return;
            }

            DialogResult.Text = choices[choice];
        }

        private async void OpenAlertDialog_Clicked(object sender, EventArgs e)
        {
            var result = await MaterialDialog.Instance.ConfirmAsync("Message", "Title", "Confirm", "Dismiss");
            if (result.HasValue)
            {
                if (result.Value)
                {
                    Debug.WriteLine("Confirm");
                }
                else
                {
                    Debug.WriteLine("Dimiss");
                }
            }
            else
            {
                Debug.WriteLine("Closed");
            }
        }

        private async void OpenInputDialog_Clicked(object sender, EventArgs e)
        {
            await MaterialDialog.Instance.InputAsync("Message", "Title", "Confirm", "Dismiss");
        }

        private async void OpenSelectDialog_Clicked(object sender, EventArgs e)
        {
            var choices = new List<string>()
            {
                "Choice 1",
                "Choice 1",
                "Choice 3",
            };
            await MaterialDialog.Instance.SelectChoiceAsync("Title", choices, "Confirm", "Dismiss");
        }
    }
}
