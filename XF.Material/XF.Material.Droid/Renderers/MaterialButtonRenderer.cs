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

            if(e?.OldElement != null)
            {
                _helper.Clean();
            }

            if (e?.NewElement != null)
            {
                _materialButton = this.Element as MaterialButton;
                _helper = new MaterialDrawableHelper(_materialButton, this.Control);
                _helper.UpdateDrawable();

                this.Control.SetMinimumWidth((int)MaterialHelper.ConvertToDp(64));
                this.Control.SetAllCaps(_materialButton.AllCaps);

                this.SetButtonIcon();
                this.SetTextColors();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e?.PropertyName == nameof(MaterialButton.Image))
            {
                this.SetButtonIcon();
            }
            else if (e?.PropertyName == nameof(MaterialButton.AllCaps))
            {
                this.Control.SetAllCaps(_materialButton.AllCaps);
            }
            else if (e?.PropertyName == nameof(Button.TextColor))
            {
                this.SetTextColors();
            }
        }

        private void SetButtonIcon()
        {
            var withIcon = !string.IsNullOrEmpty(_materialButton.Image);
            _helper.UpdateHasIcon(withIcon);

            if (withIcon)
            {
                var fileName = _materialButton.Image.File.Split('.').First();
                var id = this.Resources.GetIdentifier(fileName, "drawable", Material.Context.PackageName);
                var width = _materialButton.ButtonType == MaterialButtonType.Text ? (int)MaterialHelper.ConvertToDp(18) : (int)MaterialHelper.ConvertToDp(18 + 4);
                var height = (int)MaterialHelper.ConvertToDp(18);
                var left = _materialButton.ButtonType == MaterialButtonType.Text ? 0 : (int)MaterialHelper.ConvertToDp(4);

                using (var drawable = MaterialHelper.GetDrawableCopyFromResource<Drawable>(id))
                {
                    drawable.SetBounds(left, 0, width, height);
                    drawable.TintDrawable(_materialButton.TextColor.ToAndroid());

                    this.Control.SetCompoundDrawables(drawable, null, null, null);
                    this.Control.CompoundDrawablePadding = 0;
                }
            }
        }
        private void SetTextColors()
        {
            var states = new int[][]
            {
                new int[] { Android.Resource.Attribute.StatePressed },
                new int[] { Android.Resource.Attribute.StateFocused, Android.Resource.Attribute.StateEnabled },
                new int[] { Android.Resource.Attribute.StateEnabled },
                new int[] { Android.Resource.Attribute.StateFocused },
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
    }
}