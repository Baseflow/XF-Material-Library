using System;
using System.Collections.Generic;
using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace MaterialMvvmSample.Views
{
    public partial class MaterialButtonView : ContentPage
    {
        public MaterialButtonView()
        {
            InitializeComponent();
            BindingContext = new MaterialButtonViewModel();
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            ((MaterialButton)sender).Text = "Clicked 1 Time";
        }
    }
}
