using Android.Content;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers.Internals;
using XF.Material.Forms.UI.Internals;
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
            var elementX = percentageX * this.Element.Width;
            var elementY = percentageY * this.Element.Height;

            _boxView.OnTapped(elementX, elementY);

            return true;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);

            if(e?.NewElement != null)
            {
                _boxView = this.Element as MaterialBoxView;
                this.ViewGroup.SetOnTouchListener(null);
                this.ViewGroup.SetOnTouchListener(this);
            }
        }
    }
}