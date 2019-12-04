using System;
using System.Collections.Generic;
using XF.Material.Forms.UI.Dialogs;

namespace MaterialMvvmSample.ViewModels
{
    public class MaterialTextFieldViewModel : BaseViewModel
    {
        public MaterialTextFieldViewModel()
        {
        }

        private string _selectedChoice;
        public string SelectedChoice
        {
            get => _selectedChoice;
            set => Set(ref _selectedChoice, value);
        }

        private List<string> _selectChoise;
        public List<string> SelectChoise
        {
            get
                
        }

        private async void SelectChoises()
        {
            var choices = new List<string>
            {
                "choice 1",
                "choice 2",
                "choice 3",
            };

            var choice = await MaterialDialog.Instance.SelectChoiceAsync("Select choices", choices);

            if (choice < 0)
                return;

            this.DialogResult.Text = choices[choice];
        }
    }
}
