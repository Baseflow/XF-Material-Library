using XF.Material.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI.Internals;

[assembly:ExportRenderer(typeof(MaterialDatePicker), typeof(MaterialDatePickerRenderer))]

namespace XF.Material.iOS.Renderers
{
    /// <summary>
    /// Remove border
    /// </summary>
    public class MaterialDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }

        protected override UITextField CreateNativeControl()
        {
            var control = base.CreateNativeControl();
            control.BorderStyle = UITextBorderStyle.None;
            return control;
        }
    }
}
