using System.ComponentModel;
using System.Linq;
using Android.Content;
using Android.Content.Res;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Compatibility.Platform.Android.AppCompat;
using Microsoft.Maui.Controls.Platform;
using XF.Material.Droid.Renderers;
using XF.Material.Maui.UI;

[assembly: ExportRenderer(typeof(MaterialButton), typeof(MaterialButtonRenderer))]

namespace XF.Material.Droid.Renderers
{
    public sealed class MaterialButtonRenderer : ButtonRenderer
    {
        private MaterialDrawableHelper _helper;
        private MaterialButton _materialButton;

        public MaterialButtonRenderer(Context context) : base(context)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _helper.Clean();
            }

            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
                return;

            if (e?.OldElement != null)
                _helper.Clean();

            if (e?.NewElement == null)
                return;

            _materialButton = (MaterialButton)Element;
            _helper = new MaterialDrawableHelper(_materialButton, Control);
            _helper.UpdateDrawable();

            Control.SetMinimumWidth((int)MaterialHelper.ConvertDpToPx(64));
            Control.SetAllCaps(_materialButton != null && _materialButton.AllCaps);
            Control.SetMaxLines(1);

            SetTextColors();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
            {
                return;
            }

            switch (e?.PropertyName)
            {
                case nameof(MaterialButton.AllCaps):
                    Control.SetAllCaps(_materialButton.AllCaps);
                    break;
                case nameof(Button.TextColor):
                    SetTextColors();
                    break;
            }
        }

        private void SetTextColors()
        {
            var states = new[]
            {
                new[] { Android.Resource.Attribute.StatePressed },
                new[] { Android.Resource.Attribute.StateFocused, Android.Resource.Attribute.StateEnabled },
                new[] { Android.Resource.Attribute.StateEnabled },
                new[] { Android.Resource.Attribute.StateFocused },
                new int[] { }
            };

            var colors = new int[]
            {
                _materialButton.TextColor.ToAndroid(),
                _materialButton.TextColor.ToAndroid(),
                _materialButton.TextColor.ToAndroid(),
                _materialButton.TextColor.ToAndroid(),
                _materialButton.TextColor.MultiplyAlpha((float)0.38).ToAndroid()
             };

            Control.SetTextColor(new ColorStateList(states, colors));
        }
    }
}
