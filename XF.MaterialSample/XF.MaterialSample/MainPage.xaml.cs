using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Dialogs;
using XF.Material.Dialogs.Configurations;
using XF.Material.Resources;
using XF.Material.Views;

namespace XF.MaterialSample
{
    public partial class MainPage : ContentPage
    {
        private int _counter = 0;

        public MainPage()
        {
            InitializeComponent();

            MaterialDialogs.SetGlobalStyles(new MaterialAlertDialogConfiguration
            {
                BackgroundColor = XF.Material.Material.GetMaterialResource<Color>(MaterialConstants.Color.PRIMARY),
                TitleTextColor = XF.Material.Material.GetMaterialResource<Color>(MaterialConstants.Color.ONPRIMARY),
                TitleFontFamily = XF.Material.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.Exo2Bold"),
                MessageTextColor = XF.Material.Material.GetMaterialResource<Color>(MaterialConstants.Color.ONPRIMARY).MultiplyAlpha(0.8),
                MessageFontFamily = XF.Material.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.OpenSansRegular"),
                TintColor = XF.Material.Material.GetMaterialResource<Color>(MaterialConstants.Color.ONPRIMARY),
                ButtonFontFamily = XF.Material.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.OpenSansSemiBold"),
                CornerRadius = 8,
                ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32),
                ButtonAllCaps = false
            },
            new MaterialLoadingDialogConfiguration
            {
                BackgroundColor = XF.Material.Material.GetMaterialResource<Color>(MaterialConstants.Color.PRIMARY),
                MessageTextColor = XF.Material.Material.GetMaterialResource<Color>(MaterialConstants.Color.ONPRIMARY).MultiplyAlpha(0.8),
                MessageFontFamily = XF.Material.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.OpenSansRegular"),
                TintColor = XF.Material.Material.GetMaterialResource<Color>(MaterialConstants.Color.ONPRIMARY),
                CornerRadius = 8,
                ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32)
            }, new MaterialSnackbarConfiguration
            {
                BackgroundColor = XF.Material.Material.GetMaterialResource<Color>(MaterialConstants.Color.PRIMARY),
                MessageFontFamily = XF.Material.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.OpenSansRegular"),
                ButtonAllCaps = false,
                ButtonFontFamily = XF.Material.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.OpenSansSemiBold"),
                TintColor = Color.White,
                MessageTextColor = XF.Material.Material.GetMaterialResource<Color>(MaterialConstants.Color.ONPRIMARY).MultiplyAlpha(0.8)
            });

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
            //await MaterialDialogs.ShowAlertAsync("You are not connected to the internet.", "No connection", new MaterialDialogConfiguration
            //{
            //    BackgroundColor = XF.Material.Material.GetMaterialResource<Color>("Material.Color.Primary"),
            //    TitleTextColor = XF.Material.Material.GetMaterialResource<Color>("Material.Color.OnPrimary"),
            //    TitleFontFamily = XF.Material.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.Exo2Bold"),
            //    MessageTextColor = XF.Material.Material.GetMaterialResource<Color>("Material.Color.OnPrimary").MultiplyAlpha(0.8),
            //    MessageFontFamily = XF.Material.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.OpenSansRegular"),
            //    AccentColor = XF.Material.Material.GetMaterialResource<Color>("Material.Color.OnPrimary"),
            //    ButtonFontFamily = XF.Material.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.OpenSansSemiBold"),
            //    CornerRadius = 8,
            //    ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32),
            //    ButtonAllCaps = false
            //});

            //await MaterialDialogs.ShowAlertAsync("Do you want to sign in?", "Sign In",confirmingAction: null, configuration: new MaterialDialogConfiguration
            //{
            //    BackgroundColor = XF.Material.Material.GetMaterialResource<Color>("Material.Color.Primary"),
            //    TitleTextColor = XF.Material.Material.GetMaterialResource<Color>("Material.Color.OnPrimary"),
            //    TitleFontFamily = XF.Material.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.Exo2Bold"),
            //    MessageTextColor = XF.Material.Material.GetMaterialResource<Color>("Material.Color.OnPrimary").MultiplyAlpha(0.8),
            //    MessageFontFamily = XF.Material.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.OpenSansRegular"),
            //    AccentColor = XF.Material.Material.GetMaterialResource<Color>("Material.Color.OnPrimary"),
            //    ButtonFontFamily = XF.Material.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.OpenSansSemiBold"),
            //    CornerRadius = 8,
            //    ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32),
            //    ButtonAllCaps = false
            //});

            //await MaterialDialogs.ShowAlertAsync("Not connected to the internet");

            switch (_counter)
            {
                case 0:
                    await MaterialDialogs.ShowAlertAsync(message: "This is an alert dialog");
                    break;
                case 1:
                    await MaterialDialogs.ShowAlertAsync(message: "This is an alert dialog", acknowledgementText: "Got It");
                    break;
                case 2:
                    await MaterialDialogs.ShowAlertAsync(message: "This is an alert dialog", title: "Alert Dialog", acknowledgementText: "Got It");
                    break;
                case 3:
                    await MaterialDialogs.ShowAlertAsync(message: "Is this an alert dialog?", confirmingText: "Yes", confirmingAction: null);
                    break;
                case 4:
                    //await MaterialDialogs.ShowAlertAsync(message: "Is this an alert dialog?", title: "Alert Dialog", confirmingText: "Yes", confirmingAction: null, dismissiveText: "No");
                    await MaterialDialogs.ShowSnackbarAsync(message:"This is a snackbar.", 
                                                            actionButtonText: "Got It",
                                                            msDuration: 5000);
                    break;
                case 5:
                    var a = await MaterialDialogs.ShowLoadingSnackbarAsync("Something is running...");
                    await Task.Delay(10000);
                    a.Dispose();
                    break;
            }

            _counter = _counter + 1;

            if (_counter == 6)
            {
                _counter = 0;
            }

            //using (await MaterialDialogs.ShowLoadingDialogAsync("Something is running."))
            //{
            //    await Task.Delay(3000);
            //}

            //await MaterialDialogs.ShowSnackbarAsync("This is a snackbar.", "Ok", null, configuration: new MaterialSnackbarConfiguration
            //{
            //    BackgroundColor = XF.Material.Material.GetMaterialResource<Color>("Material.Color.Primary"),
            //    MessageFontFamily = XF.Material.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.OpenSansRegular"),
            //    ButtonFontFamily = XF.Material.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.OpenSansSemiBold"),
            //    TintColor = Color.White,
            //    MessageTextColor = XF.Material.Material.GetMaterialResource<Color>("Material.Color.OnPrimary").MultiplyAlpha(0.8)
            //});
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
            var a = await MaterialDialogs.ShowLoadingDialogAsync("Something is running...");
            await Task.Delay(20000);
            a.Dispose();
        }

        private async void MaterialButton_ShowLoadingSnackbar(object sender, EventArgs e)
        {
            using (await MaterialDialogs.ShowLoadingSnackbarAsync("Something is running..."))
            {
                await Task.Delay(3000);
            }
        }
    }
}
