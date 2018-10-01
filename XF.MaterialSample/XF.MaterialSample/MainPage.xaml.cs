using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Dialogs;
using XF.Material.Forms.Dialogs.Configurations;
using XF.Material.Forms.Effects;
using XF.Material.Forms.Resources;
using XF.Material.Forms.Views;

namespace XF.MaterialSample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            radioButtonGroup.Choices = new string[]
            {
                "Mobile Developer (Xamarin)",
                "Mobile Developer (Native)",
                "Web Developer (.NET)",
                "Web Developer (Laravel)",
                "Quality Assurance Engineer",
                "Business Analyst",
                "Recruitment Officer",
                "Project Manager",
                "Scrum Master"
            };


            //rbg.Choices = new string[]
            //{
            //    "Option 1",
            //    "Option 2",
            //    "Option 3",
            //    "Option 4"
            //};

            //radioButtonGroup.SelectedIndex = 1;
            //radioButtonGroup.SelectedIndexChangedCommand = new Command<int>((s) =>
            //{
            //    if (s >= 0)
            //    {
            //        System.Diagnostics.Debug.WriteLine(radioButtonGroup.Choices[s]);
            //    }
            //});

            //radioButtonGroup.SelectedIndices = new List<int>(2) { 1, 3 };

            //radioButtonGroup.SelectedIndicesChangedCommand = new Command<int[]>((s) =>
            //{
            //    if (s.Any())
            //    {
            //        foreach (var index in s)
            //        {
            //            System.Diagnostics.Debug.WriteLine(radioButtonGroup.Choices[index]);
            //        }
            //    }
            //});

            //radioButtonGroup.SelectedIndexChanged += (s, e) =>
            //{
            //    if (e.Index >= 0)
            //    {
            //        System.Diagnostics.Debug.WriteLine("Selected: " + (radioButtonGroup.Choices as string[])[e.Index]);
            //    }
            //};


            //MaterialDialog.Instance.SetGlobalStyles(new MaterialAlertDialogConfiguration
            //{
            //    BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.PRIMARY),
            //    TitleTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ONPRIMARY),
            //    TitleFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.Exo2Bold"),
            //    MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ONPRIMARY).MultiplyAlpha(0.8),
            //    MessageFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.OpenSansRegular"),
            //    TintColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ONPRIMARY),
            //    ButtonFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.OpenSansSemiBold"),
            //    CornerRadius = 8,
            //    ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32),
            //    ButtonAllCaps = false
            //},
            //new MaterialLoadingDialogConfiguration
            //{
            //    BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.PRIMARY),
            //    MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ONPRIMARY).MultiplyAlpha(0.8),
            //    MessageFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.OpenSansRegular"),
            //    TintColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ONPRIMARY),
            //    CornerRadius = 8,
            //    ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32)
            //}, new MaterialSnackbarConfiguration
            //{
            //    BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.PRIMARY),
            //    MessageFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.OpenSansRegular"),
            //    ButtonAllCaps = false,
            //    ButtonFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.OpenSansSemiBold"),
            //    TintColor = Color.White,
            //    MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ONPRIMARY).MultiplyAlpha(0.8)
            //},
            //new MaterialSimpleDialogConfiguration
            //{
            //    BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.PRIMARY),
            //    TitleTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ONPRIMARY),
            //    TitleFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.OpenSansSemiBold"),
            //    MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ONPRIMARY).MultiplyAlpha(0.8),
            //    MessageFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.OpenSansRegular"),
            //    TintColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ONPRIMARY),
            //    CornerRadius = 8,
            //    ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32)
            //});

            //EmailField.Focused += (s, e) =>
            //{
            //    Regex rx = new Regex(@"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");

            //    if (!e.IsFocused && string.IsNullOrEmpty(EmailField.Text))
            //    {
            //        EmailField.HasError = false;
            //    }

            //    else if (!e.IsFocused && !string.IsNullOrEmpty(EmailField.Text))
            //    {
            //        EmailField.HasError = !rx.IsMatch(EmailField.Text);
            //    }
            //};
        }

        private async void MaterialButton_ShowDialog(object sender, EventArgs e)
        {
            //await MaterialDialogs.ShowAlertAsync("You are not connected to the internet.", "No connection", new MaterialDialogConfiguration
            //{
            //    BackgroundColor = XF.Material.Forms.Material.GetMaterialResource<Color>("Material.Color.Primary"),
            //    TitleTextColor = XF.Material.Forms.Material.GetMaterialResource<Color>("Material.Color.OnPrimary"),
            //    TitleFontFamily = XF.Material.Forms.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.Exo2Bold"),
            //    MessageTextColor = XF.Material.Forms.Material.GetMaterialResource<Color>("Material.Color.OnPrimary").MultiplyAlpha(0.8),
            //    MessageFontFamily = XF.Material.Forms.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.OpenSansRegular"),
            //    AccentColor = XF.Material.Forms.Material.GetMaterialResource<Color>("Material.Color.OnPrimary"),
            //    ButtonFontFamily = XF.Material.Forms.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.OpenSansSemiBold"),
            //    CornerRadius = 8,
            //    ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32),
            //    ButtonAllCaps = false
            //});

            //await MaterialDialogs.ShowAlertAsync("Do you want to sign in?", "Sign In",confirmingAction: null, configuration: new MaterialDialogConfiguration
            //{
            //    BackgroundColor = XF.Material.Forms.Material.GetMaterialResource<Color>("Material.Color.Primary"),
            //    TitleTextColor = XF.Material.Forms.Material.GetMaterialResource<Color>("Material.Color.OnPrimary"),
            //    TitleFontFamily = XF.Material.Forms.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.Exo2Bold"),
            //    MessageTextColor = XF.Material.Forms.Material.GetMaterialResource<Color>("Material.Color.OnPrimary").MultiplyAlpha(0.8),
            //    MessageFontFamily = XF.Material.Forms.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.OpenSansRegular"),
            //    AccentColor = XF.Material.Forms.Material.GetMaterialResource<Color>("Material.Color.OnPrimary"),
            //    ButtonFontFamily = XF.Material.Forms.Material.GetMaterialResource<OnPlatform<string>>("FontFamily.OpenSansSemiBold"),
            //    CornerRadius = 8,
            //    ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32),
            //    ButtonAllCaps = false
            //});

            //await MaterialDialog.Instance.AlertAsync("Not connected to the internet", "Connection error", "Ok");
            //var actions = new List<string> { "Open in new window", "Download to device", "Archive item", "Delete item" };
            //var result = await MaterialDialog.Instance.SelectActionAsync("What do you want to do?", actions);
            //await MaterialDialog.Instance.SnackbarAsync("Selected " + actions[result], "Got It");
            //var result = await MaterialDialog.Instance.ShowConfirmAsync("Do you want to sign in?");
            //System.Diagnostics.Debug.WriteLine(result);

            //var page = new ContentPage() { Title = "Page 2" };
            //page.Appearing += delegate
            //{
            //    (Application.Current.MainPage as NavigationPage).BarBackgroundColor = Material.Forms.Material.ColorConfiguration.Secondary;
            //    (Application.Current.MainPage as NavigationPage).BarTextColor = Color.White;
            //    Material.Forms.Material.PlatformConfiguration.ChangeStatusBarColor(Material.Forms.Material.ColorConfiguration.Secondary);
            //};
            //await this.Navigation.PushAsync(page);

            var result = await MaterialDialog.Instance.SelectChoiceAsync("Select a job position that best suits you", new string[]
            {
                "Mobile Developer (Xamarin)",
                "Mobile Developer (Native)",
                "Web Developer (.NET)",
                "Web Developer (Wordpress/Drupal)",
                "Quality Assurance Engineer",
                "Business Analyst",
                "Recruitment Officer",
                "Project Manager",
                "Scrum Master"
            });

            System.Diagnostics.Debug.WriteLine(result);
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
            var a = await MaterialDialog.Instance.LoadingDialogAsync("Something is running...");
            await Task.Delay(20000);
            a.Dispose();
        }

        private async void MaterialButton_ShowLoadingSnackbar(object sender, EventArgs e)
        {
            using (await MaterialDialog.Instance.LoadingSnackbarAsync("Something is running..."))
            {
                await Task.Delay(3000);
            }
        }

        private void RadioButtonGroup_SelectedIndexChanged(object sender, SelectedIndexChangedEventArgs e)
        {
            if(e?.Index >= 0)
            {
                //System.Diagnostics.Debug.WriteLine(radioButtonGroup.Choices[e.Index]);
            }
        }

        private void checkBoxGroup_SelectedIndicesChanged(object sender, SelectedIndicesChangedEventArgs e)
        {
            if(e.Indices.Any())
            {
                foreach(var index in e.Indices)
                {
                    //System.Diagnostics.Debug.WriteLine(checkBoxGroup.Choices[index]);
                }
            }
        }
    }
}
