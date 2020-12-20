using System.Diagnostics;
using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace MaterialMvvmSample.Views
{
    public partial class MaterialTextFieldView : ContentPage
    {
        public MaterialTextFieldView()
        {
            InitializeComponent();
            BindingContext = new MaterialTextFieldViewModel();
        }

        private void MaterialTextField_TrailingIconSelected(object sender, System.EventArgs e)
        {

            var s = sender as MaterialTextField;
           
            s.HasError = !s.HasError;

            if (s.HasError)
            {
                s.InputType = MaterialTextFieldInputType.Text;
            }
            else
            {
                s.InputType = MaterialTextFieldInputType.Password;
            }


            Debug.WriteLine(e);
        }
    }
}
