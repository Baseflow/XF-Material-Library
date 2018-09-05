using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.iOS.Renderers;
using XF.Material.Forms.Views;

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
                NavigationBar.Frame = new CGRect(0, 0, this.View.Frame.Size.Width, 56f);

                if(materialElement.HasShadow)
                {
                    NavigationBar.Layer.MasksToBounds = false;
                    NavigationBar.Layer.ShadowColor = UIColor.Black.CGColor;
                    NavigationBar.Layer.ShadowOffset = new CGSize(0, 3f);
                    NavigationBar.Layer.ShadowOpacity = 0.32f;
                    NavigationBar.Layer.ShadowRadius = 3f;
                }
            }
        }
    }
}