using System.ComponentModel;
using System.Linq;
using Android.Content;
using Android.Content.Res;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Forms.UI;
using Button = Xamarin.Forms.Button;

[assembly: ExportRenderer(typeof(MaterialButton), typeof(MaterialButtonRenderer))]

namespace XF.Material.Droid.Renderers
{
    public sealed class MaterialButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        private MaterialDrawableHelper _helper;
        private MaterialButton _materialButton;

        public MaterialButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                return;
            }

            if (e?.OldElement != null)
            {
                _helper.Clean();
            }

            if (e?.NewElement == null)
            {
                return;
            }

            _materialButton = Element as MaterialButton;
            _helper = new MaterialDrawableHelper(_materialButton, Control);
            _helper.UpdateDrawable();

            Control.SetMinimumWidth((int)MaterialHelper.ConvertDpToPx(64));
            Control.SetAllCaps(_materialButton != null && _materialButton.AllCaps);

            SetButtonIcon();
            SetTextColors();
            SetTextLetterSpacing();
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
                case nameof(MaterialButton.ImageSource):
                case nameof(MaterialButton.Image):
                    SetButtonIcon();
                    break;
                case nameof(MaterialButton.AllCaps):
                    Control.SetAllCaps(_materialButton.AllCaps);
                    break;
                case nameof(Button.TextColor):
                    SetTextColors();
                    break;
                case nameof(MaterialButton.LetterSpacing):
                    SetTextLetterSpacing();
                    break;
            }
        }

        private void SetButtonIcon()
        {
            var withIcon = !string.IsNullOrEmpty(_materialButton.Image) || !(_materialButton.ImageSource?.IsEmpty ?? true);
            _helper.UpdateHasIcon(withIcon);

            if (!withIcon)
            {
                return;
            }

            var drawable = Control.GetCompoundDrawables().FirstOrDefault(s => s != null);

            if (drawable == null)
            {
                return;
            }

            var drawableCopy = drawable.GetDrawableCopy();
            var width = _materialButton.ButtonType == MaterialButtonType.Text ? (int)MaterialHelper.ConvertDpToPx(18) : (int)MaterialHelper.ConvertDpToPx(18 + 4);
            var height = (int)MaterialHelper.ConvertDpToPx(18);
            var left = _materialButton.ButtonType == MaterialButtonType.Text ? 0 : (int)MaterialHelper.ConvertDpToPx(4);
            drawableCopy.SetBounds(left, 0, width, height);
            drawableCopy.TintDrawable(_materialButton.TextColor.ToAndroid());

            Control.SetCompoundDrawables(drawableCopy, null, null, null);
            Control.CompoundDrawablePadding = 0;
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
                _materialButton.TextColor.MultiplyAlpha(0.38).ToAndroid()
             };

            Control.SetTextColor(new ColorStateList(states, colors));
        }

        private void SetTextLetterSpacing()
        {
            var rawLetterSpacing = _materialButton.LetterSpacing / Control.TextSize;
            Control.LetterSpacing = MaterialHelper.ConvertSpToPx(rawLetterSpacing);
        }
    }
}
