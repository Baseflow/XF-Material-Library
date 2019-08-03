using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI;
using XF.Material.iOS.Renderers;

[assembly: ExportRenderer(typeof(MaterialMenuButton), typeof(MaterialMenuRenderer))]
namespace XF.Material.iOS.Renderers
{
    public class MaterialMenuRenderer : VisualElementRenderer<MaterialMenuButton>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<MaterialMenuButton> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                this.Element.InternalCommand = new Command(this.OnClick);
            }
        }

        private void OnClick()
        {
            var globalLocation = this.ConvertPointToView(this.GetContentRenderer().Frame.Location, null);

            this.Element.OnViewTouch(globalLocation.X, globalLocation.Y);
        }

        private UIView GetContentRenderer()
        {
            return Platform.GetRenderer(this.Element?.Content).NativeView;
        }
    }
}