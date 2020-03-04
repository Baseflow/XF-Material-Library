using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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