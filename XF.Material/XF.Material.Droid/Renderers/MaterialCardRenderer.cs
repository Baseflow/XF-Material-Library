using Android.Content;
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
                var cornerRadius = MaterialExtensions.ConvertDpToPx((int)this.Element.CornerRadius);
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