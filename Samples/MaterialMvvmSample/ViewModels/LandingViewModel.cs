using System;
using System.Windows.Input;
using MaterialMvvmSample.Views;
using Xamarin.Forms;

namespace MaterialMvvmSample.ViewModels
{
    public class LandingViewModel : BaseViewModel
    {
        public LandingViewModel()
        {

        }

        public ICommand GoToChipFontSizeViewCommand => new Command(() => this.Navigation.PushAsync(ViewNames.ChipFontSizeView));
    }
}
