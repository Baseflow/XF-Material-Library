using UIKit;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Compatibility;
using XF.Material.Maui.UI;
using XF.Material.iOS.Renderers;
using Microsoft.Maui.Controls.Platform;

[assembly: ExportRenderer(typeof(MaterialCircularLoadingView), typeof(MaterialCircularLoadingViewRenderer))]
namespace XF.Material.iOS.Renderers
{
    public class MaterialCircularLoadingViewRenderer /*: AnimationViewRenderer*/
    {
        //private MaterialCircularLoadingView _materialElement;
        //private LOTColorValueCallback _valueCallback;

        //protected override void OnElementChanged(ElementChangedEventArgs<AnimationView> e)
        //{
        //    base.OnElementChanged(e);

        //    if (e?.NewElement == null)
        //    {
        //        return;
        //    }

        //    _materialElement = e?.NewElement as MaterialCircularLoadingView;
        //    if (_materialElement != null && _materialElement.Animation == null)
        //    {
        //        _materialElement.Animation = "loading_animation.json";
        //    }

        //    if (Control == null)
        //    {
        //        return;
        //    }

        //    Control.ContentMode = UIViewContentMode.ScaleAspectFill;
        //}

        //public override void LayoutSubviews()
        //{
        //    base.LayoutSubviews();

        //    if (_valueCallback != null)
        //    {
        //        return;
        //    }

        //    _valueCallback = LOTColorValueCallback.WithCGColor(_materialElement.TintColor.ToCGColor());
        //    var keyPath = LOTKeypath.KeypathWithString("Shape Layer 1 Comp 1.Shape Layer 1.Ellipse 1.Stroke 1.Color");

        //    if (Control == null)
        //    {
        //        return;
        //    }

        //    Control.SetValueDelegate(_valueCallback, keyPath);
        //    Control.Play();
        //}
    }
}
