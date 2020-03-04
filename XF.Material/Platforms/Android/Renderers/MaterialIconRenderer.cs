using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using AndroidX.Core.Graphics.Drawable;
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

            if (Control == null)
            {
                return;
            }

            if (e?.NewElement == null)
            {
                return;
            }

            _materialIcon = Element as MaterialIcon;
            UpdateDrawable();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
            {
                return;
            }

            if (e?.PropertyName == nameof(MaterialIcon.TintColor) || e?.PropertyName == nameof(Image.Source))
            {
                UpdateDrawable();
            }
        }

        private void UpdateDrawable()
        {
            using (var drawable = Control.Drawable.GetDrawableCopy())
            {
                ChangeTintColor(drawable);
            }
        }

        private void ChangeTintColor(Drawable drawable)
        {
            if (_materialIcon.TintColor.IsDefault || drawable == null)
            {
                return;
            }

            var tintColor = _materialIcon.TintColor.ToAndroid();
            DrawableCompat.SetTint(drawable, tintColor);
            Control.SetImageDrawable(drawable);
        }
    }
}
