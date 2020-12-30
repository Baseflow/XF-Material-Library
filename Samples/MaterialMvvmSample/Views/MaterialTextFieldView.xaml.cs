using System.Diagnostics;
using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;
using XamSvg.XamForms;
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


            if (s.InputType == MaterialTextFieldInputType.Password)
            {
                s.InputType = MaterialTextFieldInputType.Plain;
                s.TrailingIcon = new SvgImageSource()
                {
                    Svg = "res:images.eye-solid-18dp"
                };
            }
            else
            {
                s.InputType = MaterialTextFieldInputType.Password;
                s.TrailingIcon = new SvgImageSource()
                {
                    Svg = "res:images.eye-slash-solid-18dp"
                };
            }


          


            Debug.WriteLine(e);
        }

        private void MaterialTextField_Unfocused(object sender, FocusEventArgs e)
        {
            var s = sender as MaterialTextField;

            if (s.Text != "CAPS")
            {
                s.HasError = true;
            }
        }
    }
}
