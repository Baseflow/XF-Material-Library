using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI.Internals;
using XF.Material.iOS.Renderers.Internals;

[assembly: ExportRenderer(typeof(MaterialBoxView), typeof(MaterialBoxViewRenderer))]
namespace XF.Material.iOS.Renderers.Internals
{
    internal class MaterialBoxViewRenderer : BoxRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);

            if(e?.NewElement != null)
            {
                this.NativeView?.AddGestureRecognizer(new UIGestureRecognizer() { Delegate = new BoxViewGestureRecognizerDelegate(this.Element as MaterialBoxView) });
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