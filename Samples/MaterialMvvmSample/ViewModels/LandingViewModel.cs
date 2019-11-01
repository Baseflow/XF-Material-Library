using System;
using System.Windows.Input;
using MaterialMvvmSample.Views;
using Xamarin.Forms;
using XF.Material.Forms.Models;

namespace MaterialMvvmSample.ViewModels
{
    public class LandingViewModel : BaseViewModel
    {
        public ICommand GoToMaterialDialogsCommand => this.GoToCommand(ViewNames.MaterialDialogsView);

        public ICommand GoToChipFontSizeViewCommand => this.GoToCommand(ViewNames.ChipFontSizeView);

        public ICommand GoToCheckboxesSampleCommand => this.GoToCommand(ViewNames.CheckboxesView);

        public ICommand GoToMaterialCircularViewCommand => this.GoToCommand(ViewNames.MaterialCircularView);

        public ICommand GoToMaterialTextFieldSampleCommand => this.GoToCommand(ViewNames.MaterialTextFieldView);

        public ICommand GoToMaterialMenuButtonViewCommand => this.GoToCommand(ViewNames.MaterialMenuButtonView);

        public ICommand GoToMaterialCardViewCommand => this.GoToCommand(ViewNames.MaterialCardView);

        private ICommand GoToCommand(string name) => new Command(() => this.Navigation.PushAsync(name));

    }
}
