using Android.Content;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using XF.Material.Droid.Renderers.Internals;
using XF.Material.Maui.UI;
using XF.Material.Maui.UI.Internals;

[assembly: ExportRenderer(typeof(MaterialTextField), typeof(MaterialElementRenderer))]
[assembly: ExportRenderer(typeof(MaterialSlider), typeof(MaterialElementRenderer))]
namespace XF.Material.Droid.Renderers.Internals
{
    internal class MaterialElementRenderer : ViewRenderer<View, Android.Views.View>
    {
        public MaterialElementRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e?.OldElement != null)
            {
                (Element as IMaterialElementConfiguration)?.ElementChanged(false);
            }

            if (e?.NewElement != null)
            {
                (Element as IMaterialElementConfiguration)?.ElementChanged(true);
            }
        }
    }
}
