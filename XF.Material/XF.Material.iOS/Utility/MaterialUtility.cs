using System.Diagnostics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.Utilities;
using XF.Material.iOS.Utility;

[assembly: Dependency(typeof(MaterialUtility))]
namespace XF.Material.iOS.Utility
{
    /// <inheritdoc />
    /// <summary>
    /// Concrete implementation which provides methods that can be used to change platform-specific configurations.
    /// </summary>
    public class MaterialUtility : IMaterialUtility
    {
        public void ChangeStatusBarColor(Color color)
        {
            var isColorDark = color.ToCGColor().IsColorDark();

            if (DeviceHelper.IsiOS13OrNewer)
            {
                if (UIApplication.SharedApplication.KeyWindow == null)
                {
                    //Debug.WriteLine("ChangeStatusBarColor(color): UIApplication.SharedApplication.KeyWindow is null - This operation cannot be complete until KeyWindow is setted.");
                    return;
                }

                UIApplication.SharedApplication.StatusBarStyle = isColorDark ? UIStatusBarStyle.LightContent : UIStatusBarStyle.Default;
                UIView statusBar = new UIView(UIApplication.SharedApplication.KeyWindow.WindowScene.StatusBarManager.StatusBarFrame);
                statusBar.BackgroundColor = color.ToUIColor();
                UIApplication.SharedApplication.KeyWindow.AddSubview(statusBar);
            }
            else
            {
                UIApplication.SharedApplication.StatusBarStyle = isColorDark ? UIStatusBarStyle.LightContent : UIStatusBarStyle.Default;
                if (UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) is UIView statusBar) statusBar.BackgroundColor = color.ToUIColor();
            }
        }
    }
}