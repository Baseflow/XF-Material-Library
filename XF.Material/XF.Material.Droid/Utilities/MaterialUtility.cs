using Android.OS;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Utilities;
using XF.Material.Forms.Utilities;

[assembly: Dependency(typeof(MaterialUtility))]
namespace XF.Material.Droid.Utilities
{
    public class MaterialUtility : Java.Lang.Object, IMaterialUtility
    {
        public void ChangeStatusBarColor(Color color)
        {
            var activity = (FormsAppCompatActivity)Material.Context;

            var isColorDark = color.ToAndroid().IsColorDark();
            activity.SetStatusBarColor(color.ToAndroid());

            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                if (!isColorDark)
                {
                    activity.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;
                }

                else
                {
                    activity.Window.DecorView.SystemUiVisibility = StatusBarVisibility.Visible;
                }
            }
        }
    }
}