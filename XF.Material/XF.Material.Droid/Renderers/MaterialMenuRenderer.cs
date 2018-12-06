using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Forms.UI;

[assembly: ExportRenderer(typeof(MaterialMenuButton), typeof(MaterialMenuRenderer))]
namespace XF.Material.Droid.Renderers
{
    public class MaterialMenuRenderer : MaterialIconButtonRenderer
    {
        private MaterialMenuButton _materialMenu;

        public MaterialMenuRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<MaterialIconButton> e)
        {
            base.OnElementChanged(e);

            if(e?.NewElement != null)
            {
                _materialMenu = this.Element as MaterialMenuButton;
            }
        }

        protected override void OnClick(double x, double y)
        {
            _materialMenu.OnViewTouch(x, y);
        }
    }
}