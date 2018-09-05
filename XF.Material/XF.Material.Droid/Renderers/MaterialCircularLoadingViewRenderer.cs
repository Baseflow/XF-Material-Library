using Android.Graphics;
using Com.Airbnb.Lottie;
using Lottie.Forms;
using Lottie.Forms.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Forms.Views;
using static Android.Widget.ImageView;
using static Com.Airbnb.Lottie.LottieAnimationView;

[assembly: ExportRenderer(typeof(MaterialCircularLoadingView), typeof(MaterialCircularLoadingViewRenderer))]
namespace XF.Material.Droid.Renderers
{
    public class MaterialCircularLoadingViewRenderer : AnimationViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<AnimationView> e)
        {
            base.OnElementChanged(e);

            if(e?.NewElement != null)
            {
                var materialElement = this.Element as MaterialCircularLoadingView;
                this.Control.SetAnimation(Resource.Raw.loading_animation, CacheStrategy.Strong);
                this.Control.SetScaleType(scaleType: ScaleType.CenterCrop);
                this.Control.AddValueCallback(new Com.Airbnb.Lottie.Model.KeyPath("**"), LottieProperty.ColorFilter, new Com.Airbnb.Lottie.Value.LottieValueCallback(new PorterDuffColorFilter(materialElement.TintColor.ToAndroid(), PorterDuff.Mode.SrcAtop)));
                this.Control.PlayAnimation();
            }
        }
    }
}