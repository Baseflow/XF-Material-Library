using System;
using System.Windows.Input;
using MaterialMvvmSample.Views;
using Xamarin.Forms;

namespace MaterialMvvmSample.ViewModels
{
    public class LandingViewModel : BaseViewModel
    {
        public ICommand GoToMaterialDialogsCommand => this.GoToCommand(ViewNames.MaterialDialogsView);

        public ICommand GoToChipFontSizeViewCommand => this.GoToCommand(ViewNames.ChipFontSizeView);

        private ICommand GoToCommand(string name) => new Command(() => this.Navigation.PushAsync(name));
    }
}
