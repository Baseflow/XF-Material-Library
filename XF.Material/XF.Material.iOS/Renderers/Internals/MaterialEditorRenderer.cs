using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.iOS.Renderers.Internals;
using XF.Material.Forms.UI.Internals;
using System.ComponentModel;
using System.Drawing;

[assembly: ExportRenderer(typeof(MaterialEditor), typeof(MaterialEditorRenderer))]
namespace XF.Material.iOS.Renderers.Internals
{
    internal class MaterialEditorRenderer : EditorRenderer
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
            if (this.Control == null)
            {
                return;
            }

            NSLayoutConstraint heightConstraint = NSLayoutConstraint.Create(this.Control, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 20f);
            this.Control.AddConstraint(heightConstraint);
            this.Control.BackgroundColor = UIColor.Clear;
            this.Control.TintColor = (this.Element as MaterialEditor)?.TintColor.ToUIColor();
            this.Control.Layer.BorderWidth = 0;
            this.Control.TranslatesAutoresizingMaskIntoConstraints = false;

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (this.Control == null) return;

            if (e?.PropertyName == nameof(MaterialEditor.TintColor))
            {
                this.Control.TintColor = (this.Element as MaterialEditor)?.TintColor.ToUIColor();
            }


        }

    }
}