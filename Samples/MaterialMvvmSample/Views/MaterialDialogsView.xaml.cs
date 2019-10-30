using System;
using System.Collections.Generic;
using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace MaterialMvvmSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialDialogsView : ContentPage
    {
        public MaterialDialogsView()
        {
            InitializeComponent();
            BindingContext = new MaterialDialogsViewModel();

            this.Opendialog.Clicked += Opendialog_Clicked;
        }

        private async void Opendialog_Clicked(object sender, EventArgs e)
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
