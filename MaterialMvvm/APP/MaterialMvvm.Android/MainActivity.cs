using Android.App;
using SoftInputMode = Android.Views.SoftInput;
using Android.Content.PM;
using Android.OS;
using MaterialMvvm.Android.Core;

namespace MaterialMvvm.Android
{
    [Activity(Label = "MaterialMvvm", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, WindowSoftInputMode= SoftInputMode.AdjustResize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private App _app;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            XF.Material.Droid.Material.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            var appSetup = new AndroidAppSetup();
            appSetup.CreateContainer();
            _app = new App();
            this.LoadApplication(_app);
        }

        protected override void OnStop()
        {
            _app.OnStop();
            base.OnStop();
        }
    }
}

