using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;

namespace XF.MaterialSample.Droid
{
    [Activity(Label = "XF.MaterialSample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            XF.Material.Droid.Material.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnBackPressed()
        {
            XF.Material.Droid.Material.HandleBackButton(base.OnBackPressed);
        }
    }
}

