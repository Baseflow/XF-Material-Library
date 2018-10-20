using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MaterialMvvmSample.Droid.Core;

namespace MaterialMvvmSample.Droid
{
    [Activity(Label = "MaterialMvvmSample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            XF.Material.Droid.Material.Init(this, savedInstanceState);

            var appContainer = new AndroidAppContainer();
            appContainer.Setup();

            var app = CommonServiceLocator.ServiceLocator.Current.GetInstance<App>();

            this.LoadApplication(app);
        }
    }
}