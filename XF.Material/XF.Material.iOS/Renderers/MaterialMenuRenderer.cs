using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI;
using XF.Material.iOS.Renderers;

[assembly: ExportRenderer(typeof(MaterialMenu), typeof(MaterialMenuRenderer))]
namespace XF.Material.iOS.Renderers
{
    public class MaterialMenuRenderer : VisualElementRenderer<MaterialMenu>
    {

        protected override void OnElementChanged(ElementChangedEventArgs<MaterialMenu> e)
        {
            base.OnElementChanged(e);

            if(e?.OldElement != null)
            {
                this.GestureRecognizers = null;
            }

            if (e?.NewElement != null)
            {
                this.AddGestureRecognizer(new UIGestureRecognizer() { Delegate = new MaterialMenuTouchDelegate(this.Element) });
            }
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