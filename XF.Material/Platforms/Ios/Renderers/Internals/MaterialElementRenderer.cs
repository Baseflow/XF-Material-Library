using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI;
using XF.Material.Forms.UI.Internals;
using XF.Material.iOS.Renderers.Internals;

[assembly: ExportRenderer(typeof(MaterialTextField), typeof(MaterialElementRenderer))]
[assembly: ExportRenderer(typeof(MaterialSlider), typeof(MaterialElementRenderer))]
namespace XF.Material.iOS.Renderers.Internals
{
    internal class MaterialElementRenderer : ViewRenderer<View, UIView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e?.OldElement != null)
            {
                (Element as IMaterialElementConfiguration)?.ElementChanged(false);
            }

            if (e?.NewElement != null)
            {
                (Element as IMaterialElementConfiguration)?.ElementChanged(true);
            }
        }
    }
}