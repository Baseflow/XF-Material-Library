using UIKit;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Compatibility;
using XF.Material.Maui.UI.Internals;
using XF.Material.iOS.Renderers.Internals;
using Microsoft.Maui.Controls.Platform;

[assembly: ExportRenderer(typeof(MaterialBoxView), typeof(MaterialBoxViewRenderer))]
namespace XF.Material.iOS.Renderers.Internals
{
    internal class MaterialBoxViewRenderer : BoxRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                NativeView?.AddGestureRecognizer(new UIGestureRecognizer() { Delegate = new BoxViewGestureRecognizerDelegate(Element as MaterialBoxView) });
            }
        }

        private class BoxViewGestureRecognizerDelegate : UIGestureRecognizerDelegate
        {
            private readonly MaterialBoxView _boxView;

            public BoxViewGestureRecognizerDelegate(MaterialBoxView boxView)
            {
                _boxView = boxView;
            }

            public override bool ShouldReceiveTouch(UIGestureRecognizer recognizer, UITouch touch)
            {
                var location = touch.LocationInView(touch.View);

                _boxView.OnTapped(location.X, location.Y);

                return false;
            }
        }
    }
}
