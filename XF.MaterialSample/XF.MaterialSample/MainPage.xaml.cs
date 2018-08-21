using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material;
using XF.Material.Dialogs;
using XF.Material.Dialogs.Configurations;
using XF.Material.Views;

namespace XF.MaterialSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();


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
        }

        private async void MaterialButton_ShowDialog(object sender, EventArgs e)
        {
            await MaterialDialogs.ShowAlertAsync("You are not connected to the internet.", "No connection", new MaterialDialogConfiguration
            {
                //BackgroundColor = Material.Material.GetMaterialResource<Color>("Material.Color.Primary"),
                //TitleTextColor = Material.Material.GetMaterialResource<Color>("Material.Color.OnPrimary"),
                //TitleFontFamily = Material.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.Exo2Bold"),
                //MessageTextColor = Material.Material.GetMaterialResource<Color>("Material.Color.OnPrimary").MultiplyAlpha(0.8),
                //MessageFontFamily = Material.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.OpenSansRegular"),
                //ButtonAccentColor = Material.Material.GetMaterialResource<Color>("Material.Color.OnPrimary"),
                //ButtonFontFamily = Material.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.OpenSansSemiBold"),
                //CornerRadius = 8,
                //ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32),
                //ButtonAllCaps = false
            });
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
    }
}
