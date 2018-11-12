using Android.Content;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Forms.UI;

[assembly: ExportRenderer(typeof(MaterialMenu), typeof(MaterialMenuRenderer))]
namespace XF.Material.Droid.Renderers
{
    public class MaterialMenuRenderer : VisualElementRenderer<MaterialMenu>
    {
        public MaterialMenuRenderer(Context context) : base(context) { }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (e.Action == MotionEventActions.Down)
            {
                var displayDensity = this.Context.Resources.DisplayMetrics.Density;
                var view = this.ViewGroup.GetChildAt(0);
                int[] position = new int[2];
                view.GetLocationInWindow(position);
                this.Element.OnViewTouch(position[0] / displayDensity, position[1] / displayDensity);
            }

            return true;
        }
    }
}