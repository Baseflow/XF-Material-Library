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

            if (e?.NewElement == null || Control == null)
            {
                return;
            }

            _materialIcon = Element as MaterialIcon;
            _image = Control.Image?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            ChangeTintColor();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e?.PropertyName != nameof(MaterialIcon.TintColor) && e?.PropertyName != nameof(Image.Source) || Control == null)
            {
                return;
            }

            _image = Control.Image?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            ChangeTintColor();
        }

        private void ChangeTintColor()
        {
            if (_materialIcon.TintColor.IsDefault || _image == null || Control == null)
            {
                return;
            }

            Control.TintColor = _materialIcon.TintColor.ToUIColor();
            Control.Image = _image;
        }
    }
}