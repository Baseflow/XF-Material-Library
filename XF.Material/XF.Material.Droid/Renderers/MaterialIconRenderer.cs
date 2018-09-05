using Android.Content;
using Android.Support.V4.Graphics.Drawable;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Forms.Views;

[assembly: ExportRenderer(typeof(MaterialIcon), typeof(MaterialIconRenderer))]
namespace XF.Material.Droid.Renderers
{
    public class MaterialIconRenderer : ImageRenderer
    {
        private MaterialIcon _materialIcon;

        public MaterialIconRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if(e?.NewElement != null)
            {
                _materialIcon = this.Element as MaterialIcon;
                this.ChangeTintColor();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if(e?.PropertyName == nameof(MaterialIcon.TintColor) || e?.PropertyName == nameof(Image.Source))
            {
                this.ChangeTintColor();
            }
        }

        private void ChangeTintColor()
        {
            if(!_materialIcon.TintColor.IsDefault)
            {
                var tintColor = _materialIcon.TintColor.ToAndroid();
                DrawableCompat.SetTint(this.Control.Drawable, tintColor);
            }
        }
    }
}