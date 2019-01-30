using System.ComponentModel;
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
                this.Control.ShowsVerticalScrollIndicator = ((MaterialDialogListView)this.Element).ShouldShowScrollbar;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e?.PropertyName == nameof(MaterialDialogListView.ShouldShowScrollbar))
            {
                this.Control.ShowsVerticalScrollIndicator = ((MaterialDialogListView)this.Element).ShouldShowScrollbar;
            }
        }
    }
}