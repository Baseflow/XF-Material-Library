using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material;
using XF.Material.Dialogs;
using XF.Material.Views;

namespace XF.MaterialSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //EmailField.FocusCommand = new Command<bool>((s) =>
            //{
            //    if(!s && PasswordField.Text?.Length > 8)
            //    {
            //        PasswordField.State = MaterialTextFieldState.Invalid;
            //    }

            //    else if (!s && PasswordField.Text?.Length <= 8)
            //    {
            //        PasswordField.State = MaterialTextFieldState.Enabled;
            //    }
            //});

            EmailField.Focused += (s, e) =>
            {
                Regex rx = new Regex(@"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");

                if (!e.IsFocused && string.IsNullOrEmpty(EmailField.Text))
                {
                    EmailField.HasError = false;
                }

                else if (!e.IsFocused && !string.IsNullOrEmpty(EmailField.Text))
                {
                    EmailField.HasError = !rx.IsMatch(EmailField.Text);
                }
            };

            EmailField2.Focused += (s, e) =>
             {
                 Regex rx = new Regex(@"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");

                 if (!e.IsFocused && string.IsNullOrEmpty(EmailField2.Text))
                 {
                     EmailField2.HasError = false;
                 }

                 else if (!e.IsFocused && !string.IsNullOrEmpty(EmailField2.Text))
                 {
                     EmailField2.HasError = !rx.IsMatch(EmailField2.Text);
                 }
             };

            //PasswordField.Focused += (s, e) =>
            //{
            //    if (!e.IsFocused && PasswordField.Text?.Length > 8)
            //    {
            //        PasswordField.State = MaterialTextFieldState.Invalid;
            //    }

            //    else if (!e.IsFocused && PasswordField.Text?.Length <= 8)
            //    {
            //        PasswordField.State = MaterialTextFieldState.Enabled;
            //    }
            //};

            //EmailField.TextChangeCommand = new Command<string>((s) => System.Diagnostics.Debug.WriteLine(s));
        }

        private async void MaterialButton_ShowDialog(object sender, EventArgs e)
        {
            //await MaterialDialogs.ShowAlertAsync("Dialogs focus user attention to ensure their content is addressed.", "Alert Dialog", positiveButtonText: "Got it", negativeButtonText: "Cancel");
            await MaterialDialogs.ShowAlertAsync("Dialogs inform users about a task and can contain critical information, require decisions, or involve multiple tasks.", "Alert Dialog");
        }

        private async void MaterialChip_ActionImageTapped(object sender, EventArgs e)
        {
            var chip = sender as MaterialChip;
            var parent = chip.Parent as StackLayout;
            var image = chip.ActionImage;
            await chip.FadeTo(0.0, 250, Easing.SinInOut);
            chip.ActionImage = null;
            chip.Text = "Goodbye!";
            await chip.FadeTo(1.0, 250, Easing.SinInOut);
            await Task.Delay(1000);
            await chip.FadeTo(0.0, 250, Easing.SinInOut);
            chip.Opacity = 0.0;
            parent.Children.Remove(chip);
            await Task.Delay(2000);
            chip.ActionImage = image;
            chip.Text = "I'm back";
            parent.Children.Add(chip);
            await chip.FadeTo(1.0, 250, Easing.SinInOut);
            chip.Opacity = 1.0;
        }

        private async void MaterialButton_ShowLoadingDialog(object sender, EventArgs e)
        {
            using (await MaterialDialogs.LoadingDialog("Something is running..."))
            {
                await Task.Delay(20000);
            }
        }

        private async void MaterialButton_ShowSnackbar(object sender, EventArgs e)
        {
            (sender as VisualElement).IsVisible = false;
            await MaterialDialogs.ShowSnackbarAsync("This is a snackbar.", "Got it", msDuration: MaterialSnackbar.DURATION_INDEFINITE, primaryAction: () =>
            {
                (sender as VisualElement).IsVisible = true;
            });
        }

        private async void MaterialButton_ShowLoadingSnackbar(object sender, EventArgs e)
        {
            using (await MaterialDialogs.LoadingSnackbar("Something is running..."))
            {
                await Task.Delay(3000);
            }
        }

        private async void Primary_Tapped(object sender, EventArgs e)
        {
            await MaterialDialogs.ShowAlertAsync("A primary color is the color displayed most frequently across your app’s screens and components.", "Primary Color");
        }
    }
}
