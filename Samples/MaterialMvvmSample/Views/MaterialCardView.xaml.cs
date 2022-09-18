using MaterialMvvmSample.ViewModels;

namespace MaterialMvvmSample.Views
{
    public partial class MaterialCardView : ContentPage
    {
        public MaterialCardView()
        {
            InitializeComponent();
            BindingContext = new MaterialCardViewModel();
        }
    }
}
