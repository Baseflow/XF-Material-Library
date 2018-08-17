using Android.Content;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers.Internal;
using XF.Material.Views.Internal;

[assembly: ExportRenderer(typeof(MaterialEntry), typeof(MaterialEntryRenderer))]
namespace XF.Material.Droid.Renderers.Internal
{
    internal class MaterialEntryRenderer : EntryRenderer
    {
        public MaterialEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                this.Control.Background = new ColorDrawable(Color.Transparent.ToAndroid());
                this.Control.SetPadding(0, 0, 0, 0);
                this.Control.SetIncludeFontPadding(false);
            }
        }
    }
}