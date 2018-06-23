using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material;
using XF.Material.Views;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace XF.MaterialSample
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            XF.Material.Material.Init(this);

            MainPage = new NavigationPage(new MainPage());
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
