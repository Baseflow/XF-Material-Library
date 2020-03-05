using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;

namespace MaterialMvvmSample.Views
{
    public partial class MaterialTextFieldView : ContentPage
    {
        public MaterialTextFieldView()
        {
            InitializeComponent();
            BindingContext = new MaterialTextFieldViewModel();
        }
    }
}
