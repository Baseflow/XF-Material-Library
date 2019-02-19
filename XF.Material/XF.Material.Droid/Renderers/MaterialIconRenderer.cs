using Android.Content;
using Android.Graphics.Drawables;
using Android.Support.V4.Graphics.Drawable;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Forms.UI;

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

            if (this.Control == null) return;

            if (e?.NewElement == null) return;
            _materialIcon = this.Element as MaterialIcon;
            this.UpdateDrawable();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (this.Control == null) return;

            if (e?.PropertyName == nameof(MaterialIcon.TintColor) || e?.PropertyName == nameof(Image.Source))
            {
                this.UpdateDrawable();
            }
        }

        private void UpdateDrawable()
        {
            using (var drawable = this.Control.Drawable.GetDrawableCopy())
            {
                this.ChangeTintColor(drawable);
            }
        }

        private void ChangeTintColor(Drawable drawable)
        {
            if (_materialIcon.TintColor.IsDefault || drawable == null) return;
            var tintColor = _materialIcon.TintColor.ToAndroid();
            DrawableCompat.SetTint(drawable, tintColor);
            this.Control.SetImageDrawable(drawable);
        }
    }
}