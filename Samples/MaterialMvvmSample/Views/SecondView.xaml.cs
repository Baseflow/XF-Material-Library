using MaterialMvvmSample.ViewModels;

namespace MaterialMvvmSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecondView : ContentPage
    {
        public SecondView()
        {
            InitializeComponent();
            BindingContext = new SecondViewModel();

        }
    }

}
