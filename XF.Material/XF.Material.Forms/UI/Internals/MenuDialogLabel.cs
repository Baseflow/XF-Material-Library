using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace XF.Material.Forms.UI.Internals
{
    internal class MenuDialogLabel : MaterialLabel
    {
        public static readonly BindableProperty SizeChangeCommandProperty = BindableProperty.Create(nameof(SizeChangeCommand), typeof(ICommand), typeof(MenuDialogLabel));

        public ICommand SizeChangeCommand
        {
            get => (ICommand)this.GetValue(SizeChangeCommandProperty);
            set => this.SetValue(SizeChangeCommandProperty, value);
        }

        public MenuDialogLabel() => this.SizeChanged += this.MenuDialogLabel_SizeChanged;

        private void MenuDialogLabel_SizeChanged(object sender, EventArgs e)
        {
            this.SizeChangeCommand?.Execute(new Dictionary<string, object>
            {
                {"width", this.Width },
                {"parameter", Convert.ToInt32(this.GetValue(MaterialMenuDialog.ParameterProperty)) }
            });

            this.SizeChanged -= this.MenuDialogLabel_SizeChanged;
        }
    }
}
