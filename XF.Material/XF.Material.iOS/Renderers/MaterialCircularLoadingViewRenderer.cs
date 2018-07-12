using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Airbnb.Lottie;
using Foundation;
using Lottie.Forms;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.iOS.Renderers;
using XF.Material.Views;

[assembly: ExportRenderer(typeof(MaterialCircularLoadingView), typeof(MaterialCircularLoadingViewRenderer))]
namespace XF.Material.iOS.Renderers
{
    public class MaterialCircularLoadingViewRenderer : Lottie.Forms.iOS.Renderers.AnimationViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<AnimationView> e)
        {
            base.OnElementChanged(e);

            if(e?.NewElement != null)
            {
                var materialElement = this.Element as MaterialCircularLoadingView;
                var animation = LOTAnimationView.AnimationNamed("loading_animation");
                var colorValueCallback = LOTColorValueCallback.WithCGColor(materialElement.Color.ToCGColor());
                animation.SetValueDelegate(colorValueCallback, LOTKeypath.KeypathWithString("**"));
                animation.LogHierarchyKeypaths();
                animation.ContentMode = UIViewContentMode.ScaleAspectFit;
                animation.LoopAnimation = true;
                this.SetNativeControl(animation);
                animation.Play();
            }
        }
    }
}