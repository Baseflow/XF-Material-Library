using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.iOS.Renderers.Internal;
using XF.Material.Forms.Views.Internal;

[assembly: ExportRenderer(typeof(MaterialEntry), typeof(MaterialEntryRenderer))]
namespace XF.Material.iOS.Renderers.Internal
{
    internal class MaterialEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(e?.NewElement != null)
            {
                this.Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}