using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers.Internals;
using XF.Material.Forms.UI.Internals;
using static Android.Text.TextUtils;
using static Android.Widget.TextView;

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
                _materialDatePicker = this.Element as MaterialDatePicker;
                this.SetControl();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);  
        }

        private void SetControl()
        {
            if(this.Control == null)
            {
                return;
            }

            this.Control.Background = new ColorDrawable(Color.Transparent.ToAndroid());
            this.Control.SetPadding(0, 0, 0, 0);
            this.Control.SetIncludeFontPadding(false);
            this.Control.SetMinimumHeight((int)MaterialHelper.ConvertToDp(20));

            var layoutParams = new MarginLayoutParams(this.Control.LayoutParameters);
            layoutParams.SetMargins(0, 0, 0, 0);
            this.Control.LayoutParameters = layoutParams;            
        }        
    }
}