using Android.OS;
using Android.Views;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using XF.Material.Droid.Utilities;
using XF.Material.Maui.Utilities;

[assembly: Dependency(typeof(MaterialUtility))]
namespace XF.Material.Droid.Utilities
{
    /// <inheritdoc />
    /// <summary>
    /// Concrete implementation which provides methods that can be used to change platform-specific configurations.
    /// </summary>
    public class MaterialUtility : IMaterialUtility
    {
        public void ChangeStatusBarColor(Color color)
        {
            var activity = (MauiAppCompatActivity)Material.Context;

            var isColorDark = color.ToAndroid().IsColorDark();
            // TODO: activity.SetStatusBarColor(color.ToAndroid());

            if (Build.VERSION.SdkInt < BuildVersionCodes.M)
            {
                return;
            }

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
