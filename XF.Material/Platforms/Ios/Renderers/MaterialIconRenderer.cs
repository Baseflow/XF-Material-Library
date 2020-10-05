using System.ComponentModel;
using System.Threading.Tasks;
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
        private MaterialIcon _materialIcon => Element as MaterialIcon;

        protected override async Task TrySetImage(Image previous = null)
        {
            await base.TrySetImage(previous);
            Control.Image = Control.Image?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            ChangeTintColor();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == MaterialIcon.TintColorProperty.PropertyName)
                ChangeTintColor();
        }

        private void ChangeTintColor()
        {
            var control = Control;
            var element = _materialIcon;
            if (control == null || element == null)
                return;

            if (element.TintColor.IsDefault)
                control.TintColor = null;
            else
                control.TintColor = element.TintColor.ToUIColor();
        }
    }
}
