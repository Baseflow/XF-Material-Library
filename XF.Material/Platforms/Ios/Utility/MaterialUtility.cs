using Foundation;
using UIKit;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Compatibility;
using XF.Material.Maui.Utilities;
using XF.Material.iOS.Utility;
using Microsoft.Maui.Platform;

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
            UIApplication.SharedApplication.StatusBarStyle = isColorDark ? UIStatusBarStyle.LightContent : UIStatusBarStyle.Default;

            UIView statusBar = null;
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                var tag = (nint)38482458385;
                statusBar = UIApplication.SharedApplication.KeyWindow?.ViewWithTag(tag);
                if (statusBar == null)
                {
                    statusBar = new UIView(UIApplication.SharedApplication.StatusBarFrame);
                    statusBar.Tag = tag;
                    UIApplication.SharedApplication.KeyWindow?.AddSubview(statusBar);
                }
            }
            else
            {
                statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            }

            if (statusBar != null)
            {
                statusBar.BackgroundColor = color.ToUIColor();
            }
        }
    }
}
