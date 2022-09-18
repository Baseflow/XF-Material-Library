using UIKit;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Compatibility;
using XF.Material.Maui.UI;
using XF.Material.iOS.Renderers;
using Microsoft.Maui.Controls.Platform;
using Platform = Microsoft.Maui.Controls.Compatibility.Platform.iOS.Platform;

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
