using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.iOS.Renderers.Internals;
using XF.Material.Forms.Views.Internals;

[assembly: ExportRenderer(typeof(MaterialEntry), typeof(MaterialEntryRenderer))]
namespace XF.Material.iOS.Renderers.Internals
{
    internal class MaterialEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(e?.NewElement != null)
            {
                this.Element.VerticalOptions = LayoutOptions.Center;
                this.Element.Margin = new Thickness(12);
                this.Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}