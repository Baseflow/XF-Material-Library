using UIKit;
using Microsoft.Maui;
using XF.Material.Maui.UI;
using XF.Material.Maui.UI.Internals;
using XF.Material.iOS.Renderers.Internals;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Platform;

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
