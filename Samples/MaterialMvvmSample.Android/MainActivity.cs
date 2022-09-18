using Android.App;
using Android.Content.PM;
using Android.OS;
using MaterialMvvmSample.Droid.Core;
using XF.Material.Droid;

namespace MaterialMvvmSample.Droid
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Material.Init(this, savedInstanceState);
        }

        public override void OnBackPressed()
        {
            Material.HandleBackButton(base.OnBackPressed);
        }
    }
}
