using Android.Content;
using Android.Graphics.Drawables;
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

            if(e?.NewElement != null)
            {
                _materialCard = this.Element as MaterialCard;
                var cornerRadius = MaterialExtensions.ConvertDpToPx((int)this.Element.CornerRadius);
                //var drawable = new GradientDrawable();
                //drawable.SetShape(ShapeType.Rectangle);
                //drawable.SetCornerRadius(cornerRadius);
                //drawable.SetColor(this.Element.BackgroundColor.ToAndroid());

                //this.Control.MaxCardElevation = MaterialExtensions.ConvertDpToPx(_materialCard.Elevation);
                //this.Control.CardElevation = MaterialExtensions.ConvertDpToPx(_materialCard.Elevation);
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