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
                Element.InternalCommand = new Command(OnClick);
            }
        }

        private void OnClick()
        {
            var globalLocation = ConvertPointToView(GetContentRenderer().Frame.Location, null);

            Element.OnViewTouch(globalLocation.X, globalLocation.Y);
        }

        private UIView GetContentRenderer()
        {
            return Platform.GetRenderer(Element?.Content).NativeView;
        }
    }
}