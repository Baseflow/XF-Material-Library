using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers.Internals;
using XF.Material.Forms.UI.Internals;

[assembly: ExportRenderer(typeof(MaterialDialogListView), typeof(MaterialDialogListViewRenderer))]
namespace XF.Material.Droid.Renderers.Internals
{
    internal class MaterialDialogListViewRenderer : ListViewRenderer
    {
        public MaterialDialogListViewRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if(e?.OldElement != null)
            {
                this.Control.ItemClick -= this.Control_ItemClick;
            }

            if(e?.NewElement != null)
            {
                this.Control.ItemClick += this.Control_ItemClick;

                this.Control.Selector = new ColorDrawable(Color.Transparent.ToAndroid());

                this.Control.VerticalScrollBarEnabled = ((MaterialDialogListView)this.Element).ShouldShowScrollbar;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if(e?.PropertyName == nameof(MaterialDialogListView.ShouldShowScrollbar))
            {
                this.Control.VerticalScrollBarEnabled = ((MaterialDialogListView)this.Element).ShouldShowScrollbar;
            }
        }

        private void Control_ItemClick(object sender, Android.Widget.AdapterView.ItemClickEventArgs e)
        {
            var view = e.View;
            view.SetBackgroundColor(Android.Graphics.Color.Transparent);
            int position = e.Position - 1;

            var listView = this.Element as MaterialDialogListView;
            listView?.ItemSelectedCommand?.Execute(position);
        }
    }
}