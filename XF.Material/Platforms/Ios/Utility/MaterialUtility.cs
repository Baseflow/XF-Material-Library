using System;
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
            UIApplication.SharedApplication.StatusBarStyle = isColorDark ? UIStatusBarStyle.LightContent : UIStatusBarStyle.Default;

            UIView statusBar = null;
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                nint tag = (System.nint)38482458385;
                statusBar = UIApplication.SharedApplication.KeyWindow?.ViewWithTag(tag);
                if (statusBar == null)
                {
                    statusBar = new UIView(UIApplication.SharedApplication.StatusBarFrame);
                    statusBar.Tag = tag;
                    UIApplication.SharedApplication.KeyWindow?.AddSubview(statusBar);
                }
            }
            else
                statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;

            if (statusBar != null)
                statusBar.BackgroundColor = color.ToUIColor();
        }
    }
}