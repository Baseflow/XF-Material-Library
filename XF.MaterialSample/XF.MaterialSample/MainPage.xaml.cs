using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material;
using XF.Material.Dialogs;
using XF.Material.Effects;
using XF.Material.Resources;
using XF.Material.Views;

namespace XF.MaterialSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
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
