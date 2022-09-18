using Android.Content;
using Android.Views;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using XF.Material.Droid.Renderers.Internals;
using XF.Material.Maui.UI.Internals;
using static Android.Views.View;

[assembly: ExportRenderer(typeof(MaterialBoxView), typeof(MaterialBoxViewRenderer))]
namespace XF.Material.Droid.Renderers.Internals
{
    public class MaterialBoxViewRenderer : BoxRenderer, IOnTouchListener
    {
        private MaterialBoxView _boxView;

        public MaterialBoxViewRenderer(Context context) : base(context) { }

        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            var percentageX = e.GetX() / v.Width;
            var percentageY = e.GetY() / v.Height;
            var elementX = percentageX * Element.Width;
            var elementY = percentageY * Element.Height;

            _boxView.OnTapped(elementX, elementY);

            return true;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement == null)
            {
                return;
            }

            _boxView = Element as MaterialBoxView;
            // TODO: ViewGroup
            // ViewGroup.SetOnTouchListener(null);
            // ViewGroup.SetOnTouchListener(this);
        }
    }
}
