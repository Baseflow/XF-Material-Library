using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace MaterialMvvmSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialDialogsView : BaseMainView
    {
        public MaterialDialogsView()
        {
            InitializeComponent();

            this.Opendialog.Clicked += Opendialog_Clicked;
        }

        private async void Opendialog_Clicked(object sender, EventArgs e)
        {
            await MaterialDialog.Instance.SelectChoicesAsync("Select choices", new List<string>
            {
                "choice 1",
                "choice 2",
                "choice 3",
            });
        }
    }
}
