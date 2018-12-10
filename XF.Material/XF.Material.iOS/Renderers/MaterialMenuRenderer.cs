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

            if(e?.OldElement != null && this.Element != null)
            {
                this.Element.Clicked -= this.Element_Clicked;
            }

            if (e?.NewElement != null)
            {
                this.Element.Clicked += this.Element_Clicked;
            }
        }

        private void Element_Clicked(object sender, System.EventArgs e)
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