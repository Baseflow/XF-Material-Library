using Android.App;
using Android.OS;
using MaterialMvvmSample.Droid.Core;
using XF.Material.Droid;

namespace MaterialMvvmSample.Droid
{
    [Activity(Label = "MaterialMvvmSample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, WindowSoftInputMode = Android.Views.SoftInput.AdjustResize)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Material.Init(this, savedInstanceState);

            var appContainer = new PlatformContainer();
            appContainer.Setup();

            var app = CommonServiceLocator.ServiceLocator.Current.GetInstance<App>();

            this.LoadApplication(app);
        }

        public override void OnBackPressed()
        {
            Material.HandleBackButton(base.OnBackPressed);
        }
    }
}