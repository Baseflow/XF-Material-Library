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
            bool isColorDark = color.ToCGColor().IsColorDark();
            //Status Bar property removed in iOS 13
            //TODO: Implement statusBarManager when bindings available from Xamarin
            //if (@available(iOS 13, *))
            //{
            //    UIView* statusBar = [[UIView alloc]initWithFrame:[UIApplication sharedApplication].keyWindow.windowScene.statusBarManager.statusBarFrame] ;
            //    statusBar.backgroundColor = [UIColor redColor];
            //    [[UIApplication sharedApplication].keyWindow addSubview:statusBar];
            //}
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                System.nint tag = (System.nint)38482458385;
                UIView statusBar = UIApplication.SharedApplication.KeyWindow?.ViewWithTag(tag);
                if (statusBar != null)
                {
                    statusBar.BackgroundColor = color.ToUIColor();
                }
                else
                {
                    UIView statusBarView = new UIView(frame: UIApplication.SharedApplication.StatusBarFrame);
                    statusBarView.Tag = tag;

                    UIApplication.SharedApplication.KeyWindow?.AddSubview(statusBarView);
                    statusBarView.BackgroundColor = color.ToUIColor();
                }
            }
            else
            {
                UIApplication.SharedApplication.StatusBarStyle = isColorDark ? UIStatusBarStyle.LightContent : UIStatusBarStyle.Default;
                if (UIApplication.SharedApplication.RespondsToSelector(new ObjCRuntime.Selector("statusBar")))
                {
                    if (UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) is UIView statusBar) statusBar.BackgroundColor = color.ToUIColor();
                }
            }
        }
    }
}