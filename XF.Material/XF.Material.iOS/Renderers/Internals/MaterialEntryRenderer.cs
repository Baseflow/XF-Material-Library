using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.iOS.Renderers.Internals;
using XF.Material.Forms.Views.Internals;
using System.ComponentModel;

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
                this.Control.BorderStyle = UITextBorderStyle.None;
                this.Control.TintColor = (this.Element as MaterialEntry).TintColor.ToUIColor();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if(e?.PropertyName == nameof(MaterialEntry.TintColor))
            {
                this.Control.TintColor = (this.Element as MaterialEntry).TintColor.ToUIColor();
            }
        }
    }
}