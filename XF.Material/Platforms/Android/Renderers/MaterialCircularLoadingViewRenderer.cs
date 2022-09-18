using Android.Graphics;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using XF.Material.Droid.Renderers;
using XF.Material.Maui.UI;
using static Android.Widget.ImageView;

[assembly: ExportRenderer(typeof(MaterialCircularLoadingView), typeof(MaterialCircularLoadingViewRenderer))]
namespace XF.Material.Droid.Renderers
{
    public class MaterialCircularLoadingViewRenderer /*: AnimationViewRenderer*/
    {
        //protected override void OnElementChanged(ElementChangedEventArgs<SKLottieView> e)
        //{
        //    base.OnElementChanged(e);

        //    if (e?.NewElement == null)
        //    {
        //        return;
        //    }

        //    var materialElement = Element as MaterialCircularLoadingView;
        //    Control.SetAnimation(Resource.Raw.loading_animation);
        //    Control.SetScaleType(scaleType: ScaleType.CenterCrop);
        //    if (materialElement != null)
        //    {
        //        Control.AddValueCallback(new Com.Airbnb.Lottie.Model.KeyPath("**"), LottieProperty.ColorFilter,
        //            new Com.Airbnb.Lottie.Value.LottieValueCallback(
        //                new PorterDuffColorFilter(materialElement.TintColor.ToAndroid(), PorterDuff.Mode.SrcAtop)));
        //    }

        //    Control.PlayAnimation();
        //}
    }
}
