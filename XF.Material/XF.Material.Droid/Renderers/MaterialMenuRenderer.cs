using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Forms.UI;

[assembly: ExportRenderer(typeof(MaterialMenu), typeof(MaterialMenuRenderer))]
namespace XF.Material.Droid.Renderers
{
    public class MaterialMenuRenderer : MaterialIconButtonRenderer
    {
        private MaterialMenu _materialMenu;

        public MaterialMenuRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<MaterialIconButton> e)
        {
            base.OnElementChanged(e);

            if(e?.NewElement != null)
            {
                _materialMenu = this.Element as MaterialMenu;
            }
        }

        protected override void OnClick(double x, double y)
        {
            _materialMenu.OnViewTouch(x, y);
        }
    }
}