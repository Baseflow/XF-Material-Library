using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI;
using XF.Material.iOS.Renderers;

[assembly: ExportRenderer(typeof(MaterialIcon), typeof(MaterialIconRenderer))]
namespace XF.Material.iOS.Renderers
{
    public class MaterialIconRenderer : ImageRenderer
    {
        private MaterialIcon _materialIcon;
        private UIImage _image;

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement == null) return;
            _materialIcon = this.Element as MaterialIcon;
            _image = this.Control.Image?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            this.ChangeTintColor();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e?.PropertyName != nameof(MaterialIcon.TintColor) && e?.PropertyName != nameof(Image.Source)) return;
            _image = this.Control.Image?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            this.ChangeTintColor();
        }

        private void ChangeTintColor()
        {
            if (_materialIcon.TintColor.IsDefault || _image == null) return;
            this.Control.TintColor = _materialIcon.TintColor.ToUIColor();
            this.Control.Image = _image;
        }
    }
}