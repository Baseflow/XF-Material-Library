using Android.Animation;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Support.V4.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Views;
using static XF.Material.Views.MaterialButton;
using Button = Xamarin.Forms.Button;

[assembly: ExportRenderer(typeof(MaterialButton), typeof(MaterialButtonRenderer))]
namespace XF.Material.Droid.Renderers
{
    public sealed class MaterialButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        private MaterialButton _materialButton;
        private bool _withIcon;

        public MaterialButtonRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _materialButton = this.Element as MaterialButton;
                this.Control.SetAllCaps(_materialButton.AllCaps);

                if (_materialButton.Image != null)
                {
                    string fileName = _materialButton.Image.File.Split('.')[0];
                    int id = this.Resources.GetIdentifier(fileName, "drawable", Material.Context.PackageName);
                    var width = (int)MaterialExtensions.ConvertDpToPx(18);
                    var height = (int)MaterialExtensions.ConvertDpToPx(18);
                    var drawable = ContextCompat.GetDrawable(Material.Context, id);
                    drawable.SetBounds(0, 0, width, height);
                    this.Control.SetCompoundDrawables(drawable, null, null, null);
                    _withIcon = true;
                }

                this.Control.Background = Material.IsLollipop ? this.CreateRippleDrawable() : this.CreateStateListDrawable();
            }
        }

        private Drawable CreateRippleDrawable()
        {
            switch (_materialButton.ButtonType)
            {
                case MaterialButtonType.Elevated:
                    this.Control.StateListAnimator = AnimatorInflater.LoadStateListAnimator(this.Context, Resource.Animator.material_button_state_list_anim);
                    break;
                case MaterialButtonType.Flat:
                    this.Control.StateListAnimator = null;
                    this.Control.OutlineProvider = null;
                    break;
            }

            var normalColor = _materialButton.BackgroundColor.ToAndroid();
            var cornerRadius = _materialButton.CornerRadius.ConvertDpToPx();
            var borderWidth = (int)MaterialExtensions.ConvertDpToPx((int)_materialButton.BorderWidth);
            var borderColor = _materialButton.BorderColor.ToAndroid();
            var rippleDrawable = normalColor.IsColorDark() ? this.GetDrawableCopyFromResource<RippleDrawable>(Resource.Drawable.drawable_ripple_dark) : this.GetDrawableCopyFromResource<RippleDrawable>(Resource.Drawable.drawable_ripple_light);
            var insetDrawable = rippleDrawable.FindDrawableByLayerId(Resource.Id.inset_drawable) as InsetDrawable;

            var gradientDrawable = insetDrawable.Drawable as GradientDrawable;
            gradientDrawable.SetCornerRadius(cornerRadius);
            gradientDrawable.SetColor(normalColor);
            gradientDrawable.SetStroke(borderWidth, borderColor);

            return rippleDrawable;
        }

        private Drawable CreateStateListDrawable()
        {
            var normalColor = _materialButton.BackgroundColor.ToAndroid();
            var pressedColor = normalColor.IsColorDark() ? (normalColor + Android.Graphics.Color.ParseColor("#52FFFFFF")) : (normalColor + Android.Graphics.Color.ParseColor("#52000000"));
            var disabledColor = ColorUtils.SetAlphaComponent(normalColor, 80);
            var cornerRadius = _materialButton.CornerRadius.ConvertDpToPx();
            var borderWidth = (int)MaterialExtensions.ConvertDpToPx((int)_materialButton.BorderWidth);
            var borderColor = _materialButton.BorderColor.ToAndroid();

            var normalShapeDrawable = _withIcon ? this.GetDrawableCopyFromResource<GradientDrawable>(Resource.Drawable.drawable_shape_with_icon) : this.GetDrawableCopyFromResource<GradientDrawable>(Resource.Drawable.drawable_shape);
            normalShapeDrawable.SetCornerRadius(cornerRadius);
            normalShapeDrawable.SetColor(normalColor);
            normalShapeDrawable.SetStroke(borderWidth, borderColor);

            var pressedShapeDrawable = _withIcon ? this.GetDrawableCopyFromResource<GradientDrawable>(Resource.Drawable.drawable_shape_with_icon) : this.GetDrawableCopyFromResource<GradientDrawable>(Resource.Drawable.drawable_shape);
            pressedShapeDrawable.SetCornerRadius(cornerRadius);
            pressedShapeDrawable.SetColor(pressedColor);
            pressedShapeDrawable.SetStroke(borderWidth, borderColor);

            var disabledShapeDrawable = _withIcon ? this.GetDrawableCopyFromResource<GradientDrawable>(Resource.Drawable.drawable_shape_with_icon) : this.GetDrawableCopyFromResource<GradientDrawable>(Resource.Drawable.drawable_shape);
            disabledShapeDrawable.SetCornerRadius(cornerRadius);
            disabledShapeDrawable.SetColor(disabledColor);
            disabledShapeDrawable.SetStroke(borderWidth, borderColor);

            var insetDrawable = ContextCompat.GetDrawable(this.Context, Resource.Drawable.drawable_selector).GetConstantState().NewDrawable().Mutate() as InsetDrawable;
            var stateListDrawable = insetDrawable.Drawable as StateListDrawable;
            stateListDrawable.AddState(new int[] { Android.Resource.Attribute.StatePressed }, pressedShapeDrawable);
            stateListDrawable.AddState(new int[] { Android.Resource.Attribute.StateFocused, Android.Resource.Attribute.StateEnabled }, pressedShapeDrawable);
            stateListDrawable.AddState(new int[] { Android.Resource.Attribute.StateEnabled }, normalShapeDrawable);
            stateListDrawable.AddState(new int[] { Android.Resource.Attribute.StateFocused }, pressedShapeDrawable);
            stateListDrawable.AddState(new int[] { }, disabledShapeDrawable);

            return insetDrawable;
        }

        private TDrawable GetDrawableCopyFromResource<TDrawable>(int resId) where TDrawable : Drawable
        {
            return ContextCompat.GetDrawable(this.Context, resId).GetConstantState().NewDrawable().Mutate() as TDrawable;
        }
    }
}