using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers.Internals;
using XF.Material.Forms.Views;
using XF.Material.Forms.Views.Internals;

[assembly: ExportRenderer(typeof(MaterialTextField), typeof(MaterialElementRenderer))]
[assembly: ExportRenderer(typeof(MaterialRadioButton), typeof(MaterialElementRenderer))]
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