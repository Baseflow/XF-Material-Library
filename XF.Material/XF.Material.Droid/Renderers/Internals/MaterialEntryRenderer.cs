using Android.Content;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers.Internals;
using XF.Material.Forms.Views.Internals;
using XF.Material.Droid;

[assembly: ExportRenderer(typeof(MaterialEntry), typeof(MaterialEntryRenderer))]
namespace XF.Material.Droid.Renderers.Internals
{
    internal class MaterialEntryRenderer : EntryRenderer
    {
        public MaterialEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                this.Element.VerticalOptions = LayoutOptions.FillAndExpand;
                this.Element.Margin = new Thickness(12, 0);
                this.Control.Background = new ColorDrawable(Color.Transparent.ToAndroid());
                this.Control.SetPadding(0, 0, 0, (int) MaterialHelper.ConvertToDp(12));
                this.Control.Gravity = Android.Views.GravityFlags.Bottom;
                this.Control.SetIncludeFontPadding(false);
            }
        }
    }
}