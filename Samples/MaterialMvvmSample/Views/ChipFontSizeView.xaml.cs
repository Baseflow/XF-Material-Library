using System;
using MaterialMvvmSample.ViewModels;

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
            TheChip.FontSize = TheChip.FontSize + 1;
        }

        public void DecreaseChipFontSize_Clicked(object sender, EventArgs e)
        {
            TheChip.FontSize = TheChip.FontSize - 1;
        }
    }
}
