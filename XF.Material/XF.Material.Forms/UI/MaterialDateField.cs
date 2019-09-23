using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Internals;

namespace XF.Material.Forms.UI
{
    /// <inheritdoc />
    /// <summary>
    /// A control that let users enter and edit a date.
    /// </summary>
    public class MaterialDateField : MaterialTextField, IMaterialElementConfiguration
    {
        private DateTime? _date;

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialDateField"/>.
        /// </summary>
        public MaterialDateField() : base()
        {
            this.InputType = MaterialTextFieldInputType.Date;
            base.TrailingIcon = "xf_arrow_dropdown";
            //base.entry.SetBinding(MaterialTextField.TextProperty, "Date", BindingMode.OneWay);
            base.Focused += this.MaterialDateField_Focused;
        }

        private async void MaterialDateField_Focused(object sender, FocusEventArgs e)
        {
            await this.PickDate();
            base.Unfocus();
        }

        public DateTime? Date
        {
            get => this._date;
            set
            {
                this._date = value;
                base.Entry.Text = value != null ? value.Value.ToShortDateString() : string.Empty;
                base.OnPropertyChanged(nameof(this.Date));
            }
        }

        protected override async Task OnPartcipatingInNonUserInteractiveInput()
        {
            await base.OnPartcipatingInNonUserInteractiveInput();

            base.Focus();
        }

        private async Task PickDate()
        {
            string title = MaterialConfirmationDialog.GetDialogTitle(this);
            string confirmingText = MaterialConfirmationDialog.GetDialogConfirmingText(this);
            string dismissiveText = MaterialConfirmationDialog.GetDialogDismissiveText(this);
            Dialogs.Configurations.MaterialConfirmationDialogConfiguration configuration = MaterialConfirmationDialog.GetDialogConfiguration(this);

            this.Date = await XF.Material.Forms.UI.Dialogs.MaterialDatePicker.Show(title, confirmingText, dismissiveText, configuration);
        }
    }
}