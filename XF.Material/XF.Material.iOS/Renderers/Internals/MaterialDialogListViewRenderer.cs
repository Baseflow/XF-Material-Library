using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI.Internals;
using XF.Material.iOS.Renderers.Internals;

[assembly: ExportRenderer(typeof(MaterialDialogListView), typeof(MaterialDialogListViewRenderer))]
namespace XF.Material.iOS.Renderers.Internals
{
    internal class MaterialDialogListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if(e?.NewElement != null)
            {
                this.Control.Bounces = false;
            }
        }
    }
}