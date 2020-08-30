using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace MaterialMvvmSample.ViewModels
{
    public class MaterialTextFieldViewModel : BaseViewModel
    {
        public IList<string> Choices => new List<string>
        {
            "Ayala Corporation",
            "San Miguel Corporation",
            "YNGEN Holdings Inc.",
            "ERNI Development Center Philippines, Inc., Bern, Switzerland"
        };

        public ICommand OpenCustomChoiceCommand { get; }

        public MaterialTextFieldViewModel()
        {
            OpenCustomChoiceCommand = new Command(async () =>
            {
                await MaterialDialog.Instance.AlertAsync("Command tapped. Do what you want ! You can open a custom popup, ask some data, and update the textfield text with this data");
            });
        }
    }
}
