using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material;
using XF.Material.Dialogs;
using XF.Material.Effects;
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
            await MaterialDialogs.ShowAlertAsync("Dialogs focus user attention to ensure their content is addressed.", "Alert Dialog", positiveButtonText: "Got it", negativeButtonText: "Cancel");
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
                await Task.Delay(3000);
            }
        }

        private async void MaterialButton_ShowSnackbar(object sender, EventArgs e)
        {
            await MaterialDialogs.ShowSnackbarAsync("This shows brief processes to the user.", "Got it");
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
