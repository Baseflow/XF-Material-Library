using Android.Animation;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Support.V4.Graphics;
using System.ComponentModel;
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
        private int _borderWidth;
        private float _cornerRadius;
        private bool _withIcon;
        private Color _borderColor;
        private Color _disabledBorderColor;
        private Color _disabledColor;
        private Color _normalColor;
        private Color _pressedColor;
        private MaterialButton _materialButton;

        public MaterialButtonRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _materialButton = this.Element as MaterialButton;
                _cornerRadius = MaterialHelper.ConvertToDp(_materialButton.CornerRadius);
                _borderWidth = (int)MaterialHelper.ConvertToDp((int)_materialButton.BorderWidth);

                this.Control.SetMinimumWidth((int)MaterialHelper.ConvertToDp(64));
                this.Control.SetAllCaps(_materialButton.AllCaps);
                this.SetupColors();
                this.SetButtonIcon();
                this.UpdateDrawable();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e?.PropertyName == MaterialButton.MaterialButtonColorChanged || e?.PropertyName == nameof(MaterialButton.ButtonType))
            {
                this.SetupColors();
                this.UpdateDrawable();
            }

            if(e?.PropertyName == nameof(MaterialButton.Image))
            {
                this.SetButtonIcon();
            }
        }

        private void SetupColors()
        {
            _normalColor = _materialButton.BackgroundColor.ToAndroid();
            _pressedColor = new Color(_normalColor.IsColorDark() ? (_normalColor + Color.ParseColor("#52FFFFFF")) : (_normalColor + Color.ParseColor("#52000000")));
            _disabledColor = new Color(ColorUtils.SetAlphaComponent(_normalColor, 80));
            _borderColor = _materialButton.BorderColor.ToAndroid();
            _disabledBorderColor = new Color(ColorUtils.SetAlphaComponent(_borderColor, 80));
        }

        private void CreateContainedButtonDrawable(bool elevated)
        {
            var normalStateDrawable = this.CreateShapeDrawable(_cornerRadius, _borderWidth, _normalColor, _borderColor);
            var disabledStateDrawable = this.CreateShapeDrawable(_cornerRadius, _borderWidth, _disabledColor, _disabledBorderColor);

            if (Material.IsLollipop)
            {
                var rippleColor = _normalColor.IsColorDark() ? Color.ParseColor("#52FFFFFF") : Color.ParseColor("#52000000");
                var rippleDrawable = _withIcon ? MaterialHelper.GetDrawableCopyFromResource<RippleDrawable>(Resource.Drawable.drawable_ripple_with_icon) : MaterialHelper.GetDrawableCopyFromResource<RippleDrawable>(Resource.Drawable.drawable_ripple);
                var insetDrawable = rippleDrawable.FindDrawableByLayerId(Resource.Id.inset_drawable) as InsetDrawable;
                var statelistDrawable = insetDrawable.Drawable as StateListDrawable;
                this.SetStates(statelistDrawable, normalStateDrawable, normalStateDrawable, disabledStateDrawable);

                rippleDrawable.SetColor(new Android.Content.Res.ColorStateList(new int[][]
                {
                    new int[]{}
                },
                new int[]
                {
                    rippleColor
                }));
                this.Control.Background = rippleDrawable;
                this.Control.StateListAnimator = elevated ? AnimatorInflater.LoadStateListAnimator(this.Context, Resource.Animator.material_button_state_list_anim) : null;
            }

            else
            {
                var insetDrawable = MaterialHelper.GetDrawableCopyFromResource<InsetDrawable>(Resource.Drawable.drawable_selector);
                var stateListDrawable = insetDrawable.Drawable as StateListDrawable;
                var pressedStateDrawable = this.CreateShapeDrawable(_cornerRadius, _borderWidth, _pressedColor, _borderColor);
                this.SetStates(stateListDrawable, normalStateDrawable, pressedStateDrawable, disabledStateDrawable);
                this.Control.Background = insetDrawable;
            }
        }

        private void CreateOutlinedButtonDrawable()
        {
            var normalStateDrawable = this.CreateShapeDrawable(_cornerRadius, _borderWidth, _normalColor, _borderColor);
            normalStateDrawable.SetColor(Color.Transparent);

            var disabledStateDrawable = this.CreateShapeDrawable(_cornerRadius, _borderWidth, _disabledColor, _disabledBorderColor);
            disabledStateDrawable.SetColor(Color.Transparent);

            if (Material.IsLollipop)
            {
                var rippleDrawable =  _withIcon ? MaterialHelper.GetDrawableCopyFromResource<RippleDrawable>(Resource.Drawable.drawable_ripple_outlined_with_icon) : MaterialHelper.GetDrawableCopyFromResource<RippleDrawable>(Resource.Drawable.drawable_ripple_outlined);
                var insetDrawable = rippleDrawable.FindDrawableByLayerId(Resource.Id.inset_drawable) as InsetDrawable;
                var statelistDrawable = insetDrawable.Drawable as StateListDrawable;
                this.SetStates(statelistDrawable, normalStateDrawable, normalStateDrawable, disabledStateDrawable);

                this.Control.Background = rippleDrawable;
                this.Control.StateListAnimator = null;
            }

            else
            {
                var insetDrawable = MaterialHelper.GetDrawableCopyFromResource<InsetDrawable>(Resource.Drawable.drawable_selector);
                var stateListDrawable = insetDrawable.Drawable as StateListDrawable;
                var pressedStateDrawable = this.CreateShapeDrawable(_cornerRadius, _borderWidth, _pressedColor, _borderColor);
                this.SetStates(stateListDrawable, normalStateDrawable, pressedStateDrawable, disabledStateDrawable);
                this.Control.Background = insetDrawable;
            }
        }

        private void CreateTextButtonDrawable()
        {

            #region Recursive logic to get parent with background color

            //var parentView = _materialButton.Parent as VisualElement;
            //var parentColor = parentView.BackgroundColor;

            //while(parentColor.IsDefault)
            //{
            //    parentView = parentView.Parent as VisualElement;

            //    if(parentView == null)
            //    {
            //        break;
            //    }

            //    parentColor = parentView.BackgroundColor;
            //}

            //_normalColor = parentColor.ToAndroid();
            //_cornerRadius = MaterialHelper.ConvertToDp(4);

            //var normalStateDrawable = this.CreateShapeDrawable(_cornerRadius, 0, _normalColor, Color.Transparent);
            //var disabledStateDrawable = this.CreateShapeDrawable(_cornerRadius, 0, Color.Transparent, Color.Transparent);


            //if (Material.IsLollipop)
            //{
            //    if (parentColor.IsDefault)
            //    {
            //        this.Control.Background = ContextCompat.GetDrawable(this.Context, Resource.Drawable.drawable_ripple_text);
            //    }

            //    else
            //    {
            //        var rippleDrawable = this.GetTemplateDrawable<RippleDrawable>();
            //        var insetDrawable = rippleDrawable.FindDrawableByLayerId(Resource.Id.inset_drawable) as InsetDrawable;
            //        var statelistDrawable = insetDrawable.Drawable as StateListDrawable;
            //        this.SetStates(statelistDrawable, normalStateDrawable, normalStateDrawable, disabledStateDrawable);
            //        this.Control.Background = rippleDrawable;
            //        this.Control.StateListAnimator = null;
            //    }
            //}

            //else
            //{
            //    var insetDrawable = this.GetTemplateDrawable<InsetDrawable>();
            //    var stateListDrawable = insetDrawable.Drawable as StateListDrawable;
            //    var pressedStateDrawable = this.CreateShapeDrawable(_cornerRadius, 0, _pressedColor, Color.Transparent);
            //    this.SetStates(stateListDrawable, normalStateDrawable, pressedStateDrawable, disabledStateDrawable);
            //    this.Control.Background = insetDrawable;
            //}

            #endregion

            if (Material.IsLollipop)
            {
                this.Control.Background = MaterialHelper.GetDrawableCopyFromResource<Drawable>(Resource.Drawable.drawable_ripple_text);
            }

            else
            {
                var normalStateDrawable = this.CreateShapeDrawable(_cornerRadius, _borderWidth, Color.Transparent, Color.Transparent);
                var pressedStateDrawable = this.CreateShapeDrawable(_cornerRadius, _borderWidth, Color.ParseColor("#52000000"), Color.Transparent);
                var insetDrawable = MaterialHelper.GetDrawableCopyFromResource<InsetDrawable>(Resource.Drawable.drawable_selector);
                var stateListDrawable = insetDrawable.Drawable as StateListDrawable;
                this.SetStates(stateListDrawable, normalStateDrawable, pressedStateDrawable, normalStateDrawable);
                this.Control.Background = insetDrawable;
            }
        }

        private GradientDrawable CreateShapeDrawable(float cornerRadius, int borderWidth, Color backgroundColor, Color borderColor)
        {
            var shapeDrawable = _withIcon ? MaterialHelper.GetDrawableCopyFromResource<GradientDrawable>(Resource.Drawable.drawable_shape_with_icon) : MaterialHelper.GetDrawableCopyFromResource<GradientDrawable>(Resource.Drawable.drawable_shape);
            shapeDrawable.SetCornerRadius(cornerRadius);
            shapeDrawable.SetColor(backgroundColor);
            shapeDrawable.SetStroke(borderWidth, borderColor);

            return shapeDrawable;
        }

        private void SetButtonIcon()
        {
            _withIcon = !string.IsNullOrEmpty(_materialButton.Image);

            if (_withIcon)
            {
                var fileName = _materialButton.Image.File.Split('.').First();
                var id = this.Resources.GetIdentifier(fileName, "drawable", Material.Context.PackageName);
                var width = (int)MaterialHelper.ConvertToDp(18);
                var height = (int)MaterialHelper.ConvertToDp(18);
                var drawable = MaterialHelper.GetDrawableCopyFromResource<Drawable>(id);
                drawable.SetBounds(0, 0, width, height);
                drawable.SetTint(_materialButton.TextColor.ToAndroid());

                this.Control.SetCompoundDrawables(drawable, null, null, null);
                this.Control.CompoundDrawablePadding = 8;
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
                case MaterialButtonType.Flat:
                    this.CreateContainedButtonDrawable(false);
                    break;
                case MaterialButtonType.Text:
                    this.CreateTextButtonDrawable();
                    break;
                case MaterialButtonType.Outlined:
                    this.CreateOutlinedButtonDrawable();
                    break;
            }
        }
    }
}