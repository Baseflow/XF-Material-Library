using CoreGraphics;
using System;
using UIKit;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.iOS.Renderers;
using XF.Material.Forms.Views;
using Xamarin.Forms.Internals;

[assembly: ExportRenderer(typeof(MaterialNavigationPage), typeof(MaterialNavigationPageRenderer))]
namespace XF.Material.iOS.Renderers
{
    public class MaterialNavigationPageRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if(e?.NewElement != null)
            {
                var materialElement = this.Element as MaterialNavigationPage;
                this.NavigationBar.Frame = new CGRect(0, 0, this.View.Frame.Size.Width, 56f);

                if(materialElement.HasShadow)
                {
                    this.NavigationBar.Layer.MasksToBounds = false;
                    this.NavigationBar.Layer.ShadowColor = UIColor.Black.CGColor;
                    this.NavigationBar.Layer.ShadowOffset = new CGSize(0, 3f);
                    this.NavigationBar.Layer.ShadowOpacity = 0.32f;
                    this.NavigationBar.Layer.ShadowRadius = 3f;
                }
            }
        }

        public override void PushViewController(UIViewController viewController, bool animated)
        {
            base.PushViewController(viewController, animated);

            viewController.NavigationItem.BackBarButtonItem = new UIBarButtonItem(string.Empty, UIBarButtonItemStyle.Plain, null);
        }
    }
}