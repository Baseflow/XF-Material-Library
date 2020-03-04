using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;

namespace MaterialMvvmSample.Views
{
    public partial class MaterialMenuButtonView : ContentPage
    {
        public MaterialMenuButtonView()
        {
            InitializeComponent();
            BindingContext = new MaterialMenuButtonViewModel();
        }
    }
}
