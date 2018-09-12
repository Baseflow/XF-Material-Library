using Android.OS;
using Android.Support.V4.Content;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Utilities;
using XF.Material.Forms.Utilities;

[assembly: Dependency(typeof(MaterialUtility))]
namespace XF.Material.Droid.Utilities
{
    public class MaterialUtility : IMaterialUtility
    {
        public void ChangeStatusBarColor(Color color)
        {
            var activity = (FormsAppCompatActivity)Material.Context;

            activity.SetStatusBarColor(color.ToAndroid());

            var isColorDark = color.ToAndroid().IsColorDark();

            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                if(!isColorDark)
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