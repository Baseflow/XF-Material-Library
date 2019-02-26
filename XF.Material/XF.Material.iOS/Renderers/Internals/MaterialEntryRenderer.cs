using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.iOS.Renderers.Internals;
using XF.Material.Forms.UI.Internals;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(MaterialEntry), typeof(MaterialEntryRenderer))]
namespace XF.Material.iOS.Renderers.Internals
{
    internal class MaterialEntryRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                this.SetControl();
            }
        }

        private void SetControl()
        {
            if(this.Control == null)
            {
                return;
            }

            this.Control.BackgroundColor = UIColor.Clear;
            this.Control.TintColor = (this.Element as MaterialEntry)?.TintColor.ToUIColor();
            this.Control.TranslatesAutoresizingMaskIntoConstraints = false;
            var heightConstraint = NSLayoutConstraint.Create(this.Control, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 20f);
            this.Control.AddConstraint(heightConstraint);
            this.Control.TextContainerInset = UIEdgeInsets.Zero;
            this.Control.TextContainer.LineFragmentPadding = 0f;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (this.Control == null) return;

            if (e?.PropertyName == nameof(MaterialEntry.TintColor))
            {
                this.Control.TintColor = (this.Element as MaterialEntry)?.TintColor.ToUIColor();
            }
        }
    }
}