using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using XF.Material.Views;
using XF.Material.Droid.Renderers;
using System.ComponentModel;
using Android.Support.V4.Graphics.Drawable;

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