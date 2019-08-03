﻿using Foundation;
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
            if (UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) is UIView statusBar) statusBar.BackgroundColor = color.ToUIColor();
        }
    }
}