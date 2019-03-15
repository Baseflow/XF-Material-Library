using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using System.ComponentModel;
using System.Linq;
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

            if (this.Control == null) return;

            if (e?.OldElement != null)
            {
                _helper.Clean();
            }

            if (e?.NewElement == null) return;
            _materialButton = this.Element as MaterialButton;
            _helper = new MaterialDrawableHelper(_materialButton, this.Control);
            _helper.UpdateDrawable();

            this.Control.SetMinimumWidth((int)MaterialHelper.ConvertToDp(64));
            this.Control.SetAllCaps(_materialButton != null && _materialButton.AllCaps);

            this.SetButtonIcon();
            this.SetTextColors();
            this.SetTextLetterSpacing();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (this.Control == null) return;

            switch (e?.PropertyName)
            {
                case nameof(MaterialButton.Image):
                    this.SetButtonIcon();
                    break;
                case nameof(MaterialButton.AllCaps):
                    this.Control.SetAllCaps(_materialButton.AllCaps);
                    break;
                case nameof(Button.TextColor):
                    this.SetTextColors();
                    break;
                case nameof(MaterialButton.LetterSpacing):
                    this.SetTextLetterSpacing();
                    break;
            }
        }

        private void SetButtonIcon()
        {
            var withIcon = !string.IsNullOrEmpty(_materialButton.Image);
            _helper.UpdateHasIcon(withIcon);

            if (!withIcon) return;

            var drawable = this.Control.GetCompoundDrawables().FirstOrDefault(s => s != null);

            if (drawable == null) return;

            var drawableCopy = drawable.GetDrawableCopy();
            var width = _materialButton.ButtonType == MaterialButtonType.Text ? (int)MaterialHelper.ConvertToDp(18) : (int)MaterialHelper.ConvertToDp(18 + 4);
            var height = (int)MaterialHelper.ConvertToDp(18);
            var left = _materialButton.ButtonType == MaterialButtonType.Text ? 0 : (int)MaterialHelper.ConvertToDp(4);
            drawableCopy.SetBounds(left, 0, width, height);
            drawableCopy.TintDrawable(_materialButton.TextColor.ToAndroid());

            this.Control.SetCompoundDrawables(drawableCopy, null, null, null);
            this.Control.CompoundDrawablePadding = 0;
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

            this.Control.SetTextColor(new ColorStateList(states, colors));
        }

        private void SetTextLetterSpacing()
        {
            var rawLetterSpacing = _materialButton.LetterSpacing / this.Control.TextSize;
            this.Control.LetterSpacing = MaterialHelper.ConvertToSp(rawLetterSpacing);
        }
    }
}