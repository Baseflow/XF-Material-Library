using Android.Content;
using Android.OS;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Views;

[assembly: ExportRenderer(typeof(MaterialCard), typeof(MaterialCardRenderer))]
namespace XF.Material.Droid.Renderers
{
    public class MaterialCardRenderer : Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer
    {
        private MaterialCard _materialCard;

        public MaterialCardRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _materialCard = this.Element as MaterialCard;

                #region SHADOW FIX FOR BELOW API 23

                /// TODO: Remove this after https://github.com/xamarin/Xamarin.Forms/issues/2423 has been resolved.
                if (Build.VERSION.SdkInt < BuildVersionCodes.N)
                {
                    _materialCard.BorderColor = _materialCard.BackgroundColor;
                }

                #endregion

                this.Control.Elevate(_materialCard.Elevation);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e?.PropertyName == nameof(MaterialCard.Elevation))
            {
                this.Control.Elevate(_materialCard.Elevation);
            }
        }
    }
}