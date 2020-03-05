using System.Windows.Input;
using MaterialMvvmSample.Views;
using Xamarin.Forms;

namespace MaterialMvvmSample.ViewModels
{
    public class LandingViewModel : BaseViewModel
    {
        public ICommand GoToMaterialDialogsCommand => GoToCommand(ViewNames.MaterialDialogsView);

        public ICommand GoToChipFontSizeViewCommand => GoToCommand(ViewNames.ChipFontSizeView);

        public ICommand GoToCheckboxesSampleCommand => GoToCommand(ViewNames.CheckboxesView);

        public ICommand GoToMaterialCircularViewCommand => GoToCommand(ViewNames.MaterialCircularView);

        public ICommand GoToMaterialTextFieldSampleCommand => GoToCommand(ViewNames.MaterialTextFieldView);

        public ICommand GoToMaterialMenuButtonViewCommand => GoToCommand(ViewNames.MaterialMenuButtonView);

        public ICommand GoToMaterialCardViewCommand => GoToCommand(ViewNames.MaterialCardView);

        public ICommand GoToMaterialButtonViewCommand => GoToCommand(ViewNames.MaterialButtonView);

        public ICommand GoToMaterialPickerViewCommand => GoToCommand(ViewNames.MaterialPicker);

        private ICommand GoToCommand(string name) => new Command(() => Navigation.PushAsync(name));

    }
}
