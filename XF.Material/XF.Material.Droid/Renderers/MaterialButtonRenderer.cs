using Android.Animation;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Support.V4.Graphics;
using Android.Support.V7.Widget;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Views;
using static XF.Material.Views.MaterialButton;
using Button = Xamarin.Forms.Button;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(MaterialButton), typeof(MaterialButtonRenderer))]
namespace XF.Material.Droid.Renderers
{
    public sealed class MaterialButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        private Color _borderColor;
        private int _borderWidth;
        private float _cornerRadius;
        private Color _disabledBorderColor;
        private Color _disabledColor;
        private MaterialButton _materialButton;
        private Color _normalColor;
        private Color _pressedColor;
        private bool _withIcon;

        public MaterialButtonRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _materialButton = this.Element as MaterialButton;
                _normalColor = _materialButton.BackgroundColor.ToAndroid();
                _pressedColor = new Color(_normalColor.IsColorDark() ? (_normalColor + Color.ParseColor("#52FFFFFF")) : (_normalColor + Color.ParseColor("#52000000")));
                _disabledColor = new Color(ColorUtils.SetAlphaComponent(_normalColor, 80));
                _cornerRadius = MaterialUtilities.ConvertDpToPx(_materialButton.CornerRadius);
                _borderWidth = (int)MaterialUtilities.ConvertDpToPx((int)_materialButton.BorderWidth);
                _borderColor = _materialButton.BorderColor.ToAndroid();
                _disabledBorderColor = new Color(ColorUtils.SetAlphaComponent(_borderColor, 80));
                _withIcon = _materialButton.Image != null;

                this.Control.SetAllCaps(_materialButton.AllCaps);
                this.SetButtonIcon();
                this.UpdateDrawable();
            }
        }

        private void CreateContainedButtonDrawable(bool elevated)
        {
            var normalStateDrawable = this.CreateShapeDrawable(_cornerRadius, _borderWidth, _normalColor, _borderColor);
            var disabledStateDrawable = this.CreateShapeDrawable(_cornerRadius, _borderWidth, _disabledColor, _disabledBorderColor);

            if (Material.IsLollipop)
            {
                var rippleDrawable = this.GetTemplateDrawable<RippleDrawable>();
                var insetDrawable = rippleDrawable.FindDrawableByLayerId(Resource.Id.inset_drawable) as InsetDrawable;
                var statelistDrawable = insetDrawable.Drawable as StateListDrawable;
                this.SetStates(statelistDrawable, normalStateDrawable, normalStateDrawable, disabledStateDrawable);
                this.Control.Background = rippleDrawable;
                this.Control.StateListAnimator = elevated ? AnimatorInflater.LoadStateListAnimator(this.Context, Resource.Animator.material_button_state_list_anim) : null;
            }

            else
            {
                var insetDrawable = this.GetTemplateDrawable<InsetDrawable>();
                var stateListDrawable = insetDrawable.Drawable as StateListDrawable;
                var pressedStateDrawable = this.CreateShapeDrawable(_cornerRadius, _borderWidth, _pressedColor, _borderColor);
                this.SetStates(stateListDrawable, normalStateDrawable, pressedStateDrawable, disabledStateDrawable);
                this.Control.Background = insetDrawable;
            }
        }

        private GradientDrawable CreateShapeDrawable(float cornerRadius, int borderWidth, Color backgroundColor, Color borderColor)
        {
            var shapeDrawable = _withIcon ? MaterialUtilities.GetDrawableCopyFromResource<GradientDrawable>(Resource.Drawable.drawable_shape_with_icon) : MaterialUtilities.GetDrawableCopyFromResource<GradientDrawable>(Resource.Drawable.drawable_shape);
            shapeDrawable.SetCornerRadius(cornerRadius);
            shapeDrawable.SetColor(backgroundColor);
            shapeDrawable.SetStroke(borderWidth, borderColor);

            return shapeDrawable;
        }

        private T GetTemplateDrawable<T>() where T : Drawable
        {
            if (Material.IsLollipop)
            {
                if (_normalColor.IsColorDark())
                {
                    return MaterialUtilities.GetDrawableCopyFromResource<T>(Resource.Drawable.drawable_ripple_dark);
                }
                else
                {
                    return MaterialUtilities.GetDrawableCopyFromResource<T>(Resource.Drawable.drawable_ripple_light);
                }
            }

            else
            {
                return MaterialUtilities.GetDrawableCopyFromResource<T>(Resource.Drawable.drawable_selector);
            }
        }

        private void SetButtonIcon()
        {
            if (_withIcon)
            {
                string fileName = _materialButton.Image.File.Split('.').First();
                int id = this.Resources.GetIdentifier(fileName, "drawable", Material.Context.PackageName);
                var width = (int)MaterialUtilities.ConvertDpToPx(18);
                var height = (int)MaterialUtilities.ConvertDpToPx(18);
                var drawable = ContextCompat.GetDrawable(Material.Context, id);
                drawable.SetBounds(0, 0, width, height);
                this.Control.SetCompoundDrawables(drawable, null, null, null);
            }
        }

        private void SetStates(StateListDrawable stateListDrawable, Drawable normalDrawable, Drawable pressedDrawable, Drawable disabledDrawable)
        {
            stateListDrawable.AddState(new int[] { Android.Resource.Attribute.StatePressed }, pressedDrawable);
            stateListDrawable.AddState(new int[] { Android.Resource.Attribute.StateFocused, Android.Resource.Attribute.StateEnabled }, pressedDrawable);
            stateListDrawable.AddState(new int[] { Android.Resource.Attribute.StateEnabled }, normalDrawable);
            stateListDrawable.AddState(new int[] { Android.Resource.Attribute.StateFocused }, pressedDrawable);
            stateListDrawable.AddState(new int[] { }, disabledDrawable);
        }

        private void UpdateDrawable()
        {
            switch (_materialButton.ButtonType)
            {
                case MaterialButtonType.Elevated:
                    this.CreateContainedButtonDrawable(true);
                    break;
            }
        }
    }
}