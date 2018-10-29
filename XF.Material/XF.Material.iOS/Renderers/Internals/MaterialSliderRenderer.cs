using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.Views;
using XF.Material.iOS.Renderers.Internals;

[assembly: ExportRenderer(typeof(MaterialSlider), typeof(MaterialSliderRenderer))]
namespace XF.Material.iOS.Renderers.Internals
{
    internal class MaterialSliderRenderer : ViewRenderer<MaterialSlider, UIView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<MaterialSlider> e)
        {
            base.OnElementChanged(e);

            if (e?.OldElement != null)
            {
                this.Element.ElementChanged(false);
            }

            if (e?.NewElement != null)
            {
                this.Element.ElementChanged(true);
            }
        }
    }
}