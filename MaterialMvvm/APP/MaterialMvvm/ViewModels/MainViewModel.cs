using MaterialMvvm.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms;
using XF.Material.Forms.Dialogs;
using XF.Material.Forms.Dialogs.Configurations;

namespace MaterialMvvm.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            MaterialDialog.Instance.SetGlobalStyles(null, null, null, null, confirmationDialogConfiguration: new MaterialConfirmationDialogConfiguration
            {
                BackgroundColor = Material.Color.Primary,
                TitleTextColor = Material.Color.OnPrimary,
                TitleFontFamily = Material.GetResource<OnPlatform<string>>("FontFamily.OpenSansSemiBold"),
                TextColor = Material.Color.OnPrimary,
                TextFontFamily = Material.GetResource<OnPlatform<string>>("FontFamily.OpenSansRegular"),
                TintColor = Material.Color.OnSecondary,
                ButtonAllCaps = false,
                ButtonFontFamily = Material.GetResource<OnPlatform<string>>("FontFamily.OpenSansSemiBold"),
                ScrimColor = Color.FromHex("#90000000"),
                ControlSelectedColor = Color.White,
                ControlUnselectedColor = Color.White.MultiplyAlpha(0.66),
                CornerRadius = 12
            });
        }

        public string[] Choices => new string[] { "Card", "Login Form", "Registration Form" };

        private List<int> _selectedIndices = new List<int>();
        public List<int> SelectedIndices
        {
            get => _selectedIndices;
            set => this.Set(ref _selectedIndices, value);
        }

        public ICommand ExploreCommand => new Command(async () => await this.Explore());

        private async Task Explore()
        {
            var result = this.Choices[await MaterialDialog.Instance.SelectChoiceAsync("Select a sample to explore", this.Choices)];

            switch(result)
            {
                case "Login Form":
                    await this.Navigation.NavigateAsync(ViewNames.LoginView);
                    break;

                default:
                    await MaterialDialog.Instance.SnackbarAsync("Not yet implemented", "Ok", configuration: new MaterialSnackbarConfiguration
                    {
                        BackgroundColor = Material.Color.Secondary.AddLuminosity(0.1),
                        TintColor = Material.Color.OnSecondary
                    });
                    break;
            }
        }
    }
}
