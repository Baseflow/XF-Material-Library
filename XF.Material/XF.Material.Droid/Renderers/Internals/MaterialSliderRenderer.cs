using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers.Internals;
using XF.Material.Forms.Views;

[assembly: ExportRenderer(typeof(MaterialSlider), typeof(MaterialSliderRenderer))]
namespace XF.Material.Droid.Renderers.Internals
{
    internal class MaterialSliderRenderer : ViewRenderer<MaterialSlider, Android.Views.View>
    {
        public MaterialSliderRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<MaterialSlider> e)
        {
            base.OnElementChanged(e);

            if(e?.OldElement != null)
            {
                this.Element.ElementChanged(false);
            }

            if(e?.NewElement != null)
            {
                this.Element.ElementChanged(true);
            }
        }
    }
}