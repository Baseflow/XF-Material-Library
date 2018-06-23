using System;
using Xamarin.Forms;
using XF.Material;

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
            await MaterialDialogs.AlertAsync("This is an alert dialog that requires action from the user.");
        }
    }
}
