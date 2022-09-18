using System.Linq;
using Android.Content;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform;
using XF.Material.Droid.Renderers;
using XF.Material.Maui.UI;

[assembly: ExportRenderer(typeof(MaterialMenuButton), typeof(MaterialMenuRenderer))]
namespace XF.Material.Droid.Renderers
{
    public class MaterialMenuRenderer : VisualElementRenderer<MaterialMenuButton>
    {
        public MaterialMenuRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<MaterialMenuButton> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                Element.InternalCommand = new Command(OnClick);
            }
        }

        private void OnClick()
        {
            var displayDensity = Context.Resources.DisplayMetrics.Density;
            var position = new int[2];
            // TODO: ViewGroup.GetChildAt(0).GetLocationInWindow(position);
            Element.OnViewTouch(position.ElementAtOrDefault(0) / displayDensity, position.ElementAtOrDefault(1) / displayDensity);
        }
    }
}
