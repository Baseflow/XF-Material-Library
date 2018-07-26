using Airbnb.Lottie;
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
        private MaterialCircularLoadingView _materialElement;
        private LOTColorValueCallback _valueCallback;

        protected override void OnElementChanged(ElementChangedEventArgs<AnimationView> e)
        {
            base.OnElementChanged(e);

            if(e?.NewElement != null)
            {
                _materialElement = this.Element as MaterialCircularLoadingView;
                _materialElement.Animation = "loading_animation.json";
                this.Control.ContentMode = UIViewContentMode.ScaleAspectFill;
            }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if(_valueCallback == null)
            {
                _valueCallback = LOTColorValueCallback.WithCGColor(_materialElement.Color.ToCGColor());
                var keyPath = LOTKeypath.KeypathWithString("Shape Layer 1 Comp 1.Shape Layer 1.Ellipse 1.Stroke 1.Color");
                this.Control.SetValueDelegate(_valueCallback, keyPath);
                this.Control.Play();
            }
        }
    }
}