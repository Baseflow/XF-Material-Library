using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI;
using XF.Material.iOS.Renderers;

[assembly: ExportRenderer(typeof(MaterialMenu), typeof(MaterialMenuRenderer))]
namespace XF.Material.iOS.Renderers
{
    public class MaterialMenuRenderer : MaterialIconButtonRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<MaterialIconButton> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                this.Control.AddGestureRecognizer(new UIGestureRecognizer() { Delegate = new MaterialMenuTouchDelegate(this.Element as MaterialMenu) });
            }
        }

        protected override void OnClick()
        {
            //Do not handle click
        }

        private class MaterialMenuTouchDelegate : UIGestureRecognizerDelegate
        {
            private readonly MaterialMenu _materialMenu;

            public MaterialMenuTouchDelegate(MaterialMenu materialMenu)
            {
                _materialMenu = materialMenu;
            }

            public override bool ShouldReceiveTouch(UIGestureRecognizer recognizer, UITouch touch)
            {
                var view = touch.View;
                var globalLocation = view.ConvertPointToView(view.Frame.Location, null);

                _materialMenu.OnViewTouch(globalLocation.X, globalLocation.Y);

                return false;
            }
        }
    }
}