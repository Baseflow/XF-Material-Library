using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers.Internals;
using XF.Material.Forms.UI.Internals;

[assembly: ExportRenderer(typeof(MaterialDatePicker), typeof(MaterialDatePickerRenderer))]
namespace XF.Material.Droid.Renderers.Internals
{
    internal class MaterialDatePickerRenderer : DatePickerRenderer
    {
        private MaterialDatePicker _materialDatePicker;

        public MaterialDatePickerRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _materialDatePicker = Element as MaterialDatePicker;
                SetControl();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }

        private void SetControl()
        {
            if (Control == null)
            {
                return;
            }

            Control.Background = new ColorDrawable(Color.Transparent.ToAndroid());
            Control.SetPadding(0, 0, 0, 0);
            Control.SetIncludeFontPadding(false);
            Control.SetMinimumHeight((int)MaterialHelper.ConvertDpToPx(20));

            var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
            layoutParams.SetMargins(0, 0, 0, 0);
            Control.LayoutParameters = layoutParams;
        }
    }
}