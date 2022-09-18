using System;
using MaterialMvvmSample.ViewModels;
using XF.Material.Maui.UI;
using XF.Material.Maui.UI.Dialogs;

namespace MaterialMvvmSample.Views
{
    public partial class MaterialButtonView : ContentPage
    {
        public MaterialButtonView()
        {
            InitializeComponent();
            BindingContext = new MaterialButtonViewModel();
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            ((MaterialButton)sender).Text = "Clicked 1 Time";
        }

        private async void MaterialButton_Clicked(object sender, EventArgs e)
        {
            var jobs = new string[]
            {
            "Mobile Developer (Xamarin)",
            "Mobile Developer (Native)",
            "Web Developer (.NET)",
            "Web Developer (Laravel)",
            "Quality Assurance Engineer",
            "Business Analyst",
            "Recruitment Officer",
            "Project Manager",
            "Scrum Master"
            };
            await MaterialDialog.Instance.AlertAsync(message: "This is an alert dialog",
                                                            title: "Alert Dialog",
                                                            acknowledgementText: "Got It");
            //Show confirmation dialog for choosing one.
            var result = await MaterialDialog.Instance.SelectChoicesAsync(title: "Select a job",
                selectedIndices: new int[] { 1, 6 },
                                                              choices: jobs);
        }
    }
}
