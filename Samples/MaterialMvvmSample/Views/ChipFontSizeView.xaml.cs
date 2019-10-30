using System;
using System.Collections.Generic;
using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaterialMvvmSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChipFontSizeView : ContentPage
    {
        public ChipFontSizeView()
        {
            InitializeComponent();
            BindingContext = new ChipFontSizeViewModel();
        }

        public void IncreaseChipFontSize_Clicked(object sender, EventArgs e)
        {
            this.TheChip.FontSize = this.TheChip.FontSize + 1;
        }

        public void DecreaseChipFontSize_Clicked(object sender, EventArgs e)
        {
            this.TheChip.FontSize = this.TheChip.FontSize - 1;
        }
    }
}
