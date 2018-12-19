using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Forms.UI;

[assembly: ExportRenderer(typeof(MaterialMenuButton), typeof(MaterialMenuRenderer))]
namespace XF.Material.Droid.Renderers
{
    public class MaterialMenuRenderer : VisualElementRenderer<MaterialMenuButton>
    {
        public MaterialMenuRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<MaterialMenuButton> e)
        {
            base.OnElementChanged(e);

            if(e?.NewElement != null)
            {
                this.Element.InternalCommand = new Command(this.OnClick);
            }
        }

        private void OnClick()
        {
            var displayDensity = this.Context.Resources.DisplayMetrics.Density;
            var position = new int[2];
            this.ViewGroup.GetChildAt(0).GetLocationInWindow(position);
            this.Element.OnViewTouch(position[0] / displayDensity, position[1] / displayDensity);
        }
    }
}