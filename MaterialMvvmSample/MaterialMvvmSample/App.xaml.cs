using MaterialMvvmSample.Utilities;
using MaterialMvvmSample.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MaterialMvvmSample
{
    public partial class App : Application
    {
        public App(INavigationService navigationService)
        {
            this.InitializeComponent();

            XF.Material.Forms.Material.Init(this, "Material.Style");

            this.Resources.MergedDictionaries.Add(new Styles());

            navigationService.SetRootView(ViewNames.MainView);
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
