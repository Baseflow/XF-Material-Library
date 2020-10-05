using Android.Graphics;
using Com.Airbnb.Lottie;
using Lottie.Forms;
using Lottie.Forms.Platforms.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Forms.UI;
using static Android.Widget.ImageView;

[assembly: ExportRenderer(typeof(MaterialCircularLoadingView), typeof(MaterialCircularLoadingViewRenderer))]
namespace XF.Material.Droid.Renderers
{
    public class MaterialCircularLoadingViewRenderer : AnimationViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<AnimationView> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement == null)
            {
                return;
            }

            var materialElement = Element as MaterialCircularLoadingView;
            Control.SetAnimation(Resource.Raw.loading_animation);
            Control.SetScaleType(scaleType: ScaleType.CenterCrop);
            if (materialElement != null)
            {
                Control.AddValueCallback(new Com.Airbnb.Lottie.Model.KeyPath("**"), LottieProperty.ColorFilter,
                    new Com.Airbnb.Lottie.Value.LottieValueCallback(
                        new PorterDuffColorFilter(materialElement.TintColor.ToAndroid(), PorterDuff.Mode.SrcAtop)));
            }

            Control.PlayAnimation();
        }
    }
}
