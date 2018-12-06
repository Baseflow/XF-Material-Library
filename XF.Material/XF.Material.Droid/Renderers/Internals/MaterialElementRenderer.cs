using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers.Internals;
using XF.Material.Forms.UI;
using XF.Material.Forms.UI.Internals;

[assembly: ExportRenderer(typeof(MaterialTextField), typeof(MaterialElementRenderer))]
[assembly: ExportRenderer(typeof(MaterialSlider), typeof(MaterialElementRenderer))]
namespace XF.Material.Droid.Renderers.Internals
{
    internal class MaterialElementRenderer : ViewRenderer<View, Android.Views.View>
    {
        public MaterialElementRenderer(Context context): base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if(e?.OldElement != null)
            {
                (this.Element as IMaterialElementConfiguration)?.ElementChanged(false);
            }

            if(e?.NewElement != null)
            {
                (this.Element as IMaterialElementConfiguration)?.ElementChanged(true);
            }
        }
    }
}