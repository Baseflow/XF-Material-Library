using CommonServiceLocator;
using MaterialMvvm.Common;
using MaterialMvvm.Utilities.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MaterialMvvm
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            XF.Material.Forms.Material.Init(this, "Material.Style");
        }

        protected override void OnStart()
        {
            var navigation = ServiceLocator.Current.GetInstance<INavigationService>();

            navigation.SetRootPage(ViewNames.MainView);
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void OnStop()
        {
            // Handle when your app is terminated/destroyed
        }
    }
}
