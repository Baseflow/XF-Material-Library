using MaterialMvvmSample.ViewModels;

namespace MaterialMvvmSample.Views
{
    public partial class MaterialPicker : ContentPage
    {
        public MaterialPicker()
        {
            InitializeComponent();
            BindingContext = new MaterialPickerViewModel();

        }
    }
}
