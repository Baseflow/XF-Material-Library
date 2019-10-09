using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaterialMvvmSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChipFontSizeView : BaseMainView
    {
        public ChipFontSizeView()
        {
            InitializeComponent();
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
