using MaterialMvvmSample.Utilities;
using MaterialMvvmSample.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MaterialMvvmSample
{
    public partial class App : Application
    {
        public App(INavigationService navigationService)
        {
            XF.Material.Maui.Material.Init(this);
            InitializeComponent();
            XF.Material.Maui.Material.Use("Material.Style");

            navigationService.SetRootView(ViewNames.LandingView);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
