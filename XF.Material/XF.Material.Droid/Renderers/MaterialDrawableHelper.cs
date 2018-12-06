using Android.Animation;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Forms.UI;
using static XF.Material.Forms.UI.MaterialButton;
using Color = Android.Graphics.Color;

namespace XF.Material.Droid.Renderers
{
    internal class MaterialDrawableHelper
    {
        private readonly Android.Views.View _aView;
        private readonly BindableObject _bindableButton;
        private readonly IMaterialButton _button;
        private readonly Dictionary<string, Action> _propertyChangeActions;
        private int _borderWidth;
        private float _cornerRadius;
        private Color _disabledBorderColor;
        private Color _disabledColor;
        private Color _enabledBorderColor;
        private Color _normalColor;
        private Color _pressedColor;
        private bool _withIcon;

        public MaterialDrawableHelper(IMaterialButton button, Android.Views.View aView)
        {
            _aView = aView;
            _button = button;
            _bindableButton = (BindableObject)_button;
            _bindableButton.PropertyChanged += this.BindableButton_PropertyChanged;
            _propertyChangeActions = new Dictionary<string, Action>
            {
                { MaterialButtonColorChanged, () =>
                    {
                        this.UpdateColors();
                        this.UpdateDrawable();
                    }
                },
                { nameof(IMaterialButton.ButtonType), () =>
                    {
                        this.UpdateColors();
                        this.UpdateDrawable();
                    }
                },
                { nameof(IMaterialButton.BorderColor), () =>
                    {
                        this.UpdateBorderColor();
                        this.UpdateDrawable();
                    }
                },
                { nameof(IMaterialButton.BorderWidth), () =>
                    {
                        this.UpdateBorderWidth();
                        this.UpdateDrawable();
                    }
                },
                { nameof(IMaterialButton.CornerRadius), () =>
                    {
                        this.UpdateCornerRadius();
                        this.UpdateDrawable();
                    }
                },
                { nameof(VisualElement.IsEnabled), () =>
                    {
                        this.UpdateElevation();
                    }
                },
            };

            this.UpdateColors();
            this.UpdateCornerRadius();
            this.UpdateBorderWidth();
        }

        public void Clean()
        {
            _bindableButton.PropertyChanged -= this.BindableButton_PropertyChanged;
        }

        public void UpdateDrawable()
        {
            using (var drawable = this.GetDrawable())
            {
                _aView.Background = drawable;
            }

            this.UpdateElevation();
        }

        public void UpdateHasIcon(bool hasIcon)
        {
            _withIcon = hasIcon;
        }

        private void BindableButton_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (_propertyChangeActions != null && _propertyChangeActions.TryGetValue(e?.PropertyName, out Action handlePropertyChange))
            {
                handlePropertyChange();
            }
        }

        private GradientDrawable CreateShapeDrawable(float cornerRadius, int borderWidth, Color backgroundColor, Color borderColor)
        {
            GradientDrawable shapeDrawable = null;

            if (_button.ButtonType != MaterialButtonType.Text)
            {
                shapeDrawable = _withIcon ? MaterialHelper.GetDrawableCopyFromResource<GradientDrawable>(Resource.Drawable.drawable_shape_with_icon) : MaterialHelper.GetDrawableCopyFromResource<GradientDrawable>(Resource.Drawable.drawable_shape);
            }
            else
            {
                shapeDrawable = MaterialHelper.GetDrawableCopyFromResource<GradientDrawable>(Resource.Drawable.drawable_shape_text);
            }

            shapeDrawable.SetCornerRadius(cornerRadius);
            shapeDrawable.SetColor(backgroundColor);
            shapeDrawable.SetStroke(borderWidth, borderColor);

            return shapeDrawable;
        }

        private Drawable GetDrawable()
        {
            var normalStateDrawable = this.CreateShapeDrawable(_cornerRadius, _borderWidth, _normalColor, _enabledBorderColor);
            var disabledStateDrawable = this.CreateShapeDrawable(_cornerRadius, _borderWidth, _disabledColor, _disabledBorderColor);

            if (Material.IsLollipop)
            {
                var rippleDrawable = this.GetRippleDrawable();
                var insetDrawable = rippleDrawable.FindDrawableByLayerId(Resource.Id.inset_drawable) as InsetDrawable;
                var stateListDrawable = insetDrawable.Drawable as StateListDrawable;
                this.SetStates(stateListDrawable, normalStateDrawable, normalStateDrawable, disabledStateDrawable);

                rippleDrawable.SetColor(new ColorStateList(new int[][]
                {
                    new int[]{}
                },
                new int[]
                {
                    _pressedColor
                }));

                return rippleDrawable;
            }
            else
            {
                var pressedStateDrawable = this.CreateShapeDrawable(_cornerRadius, _borderWidth, _pressedColor, _enabledBorderColor);
                StateListDrawable stateListDrawable = null;
                Drawable backgroundDrawable = null;

                if (Material.IsJellyBean)
                {
                    stateListDrawable = new StateListDrawable();
                    backgroundDrawable = stateListDrawable;
                }
                else
                {
                    var insetDrawable = MaterialHelper.GetDrawableCopyFromResource<InsetDrawable>(Resource.Drawable.drawable_selector);
                    stateListDrawable = insetDrawable.Drawable as StateListDrawable;
                    backgroundDrawable = insetDrawable;
                }

                this.SetStates(stateListDrawable, normalStateDrawable, pressedStateDrawable, disabledStateDrawable);

                return backgroundDrawable;
            }
        }

        private RippleDrawable GetRippleDrawable()
        {
            RippleDrawable rippleDrawable = null;

            if (_button.ButtonType == MaterialButtonType.Text || _button.ButtonType == MaterialButtonType.Outlined)
            {
                if (_button.ButtonType == MaterialButtonType.Outlined)
                {
                    rippleDrawable = _withIcon ? MaterialHelper.GetDrawableCopyFromResource<RippleDrawable>(Resource.Drawable.drawable_ripple_outlined_with_icon) : MaterialHelper.GetDrawableCopyFromResource<RippleDrawable>(Resource.Drawable.drawable_ripple_outlined);
                }
                else
                {
                    rippleDrawable = MaterialHelper.GetDrawableCopyFromResource<RippleDrawable>(Resource.Drawable.drawable_ripple_text);
                }
            }

            else
            {
                rippleDrawable = _withIcon ? MaterialHelper.GetDrawableCopyFromResource<RippleDrawable>(Resource.Drawable.drawable_ripple_with_icon) : MaterialHelper.GetDrawableCopyFromResource<RippleDrawable>(Resource.Drawable.drawable_ripple);
            }

            var maskDrawable = rippleDrawable.FindDrawableByLayerId(Android.Resource.Id.Mask) as InsetDrawable;
            var rippleMaskGradientDrawable = maskDrawable.Drawable as GradientDrawable;
            rippleMaskGradientDrawable.SetCornerRadius(_cornerRadius);

            return rippleDrawable;
        }

        private void SetStates(StateListDrawable stateListDrawable, Drawable normalDrawable, Drawable pressedDrawable, Drawable disabledDrawable)
        {
            stateListDrawable.AddState(new int[] { Android.Resource.Attribute.StatePressed }, pressedDrawable);
            stateListDrawable.AddState(new int[] { Android.Resource.Attribute.StateFocused, Android.Resource.Attribute.StateEnabled }, pressedDrawable);
            stateListDrawable.AddState(new int[] { Android.Resource.Attribute.StateEnabled }, normalDrawable);
            stateListDrawable.AddState(new int[] { Android.Resource.Attribute.StateFocused }, pressedDrawable);
            stateListDrawable.AddState(new int[] { }, disabledDrawable);
        }

        private void UpdateBorderColor()
        {
            if (_button.ButtonType == MaterialButtonType.Text)
            {
                return;
            }

            var borderColor = _button.BorderColor;

            _enabledBorderColor = borderColor.IsDefault ? Color.Transparent : borderColor.ToAndroid();
            _disabledBorderColor = borderColor.IsDefault ? Color.Transparent : _enabledBorderColor.GetDisabledColor();
        }

        private void UpdateBorderWidth()
        {
            if (_button.ButtonType == MaterialButtonType.Text)
            {
                _borderWidth = 0;
                return;
            }

            _borderWidth = (int)MaterialHelper.ConvertToDp(_button.BorderWidth);
        }

        private void UpdateColors()
        {
            var stateColor = _button.BackgroundColor;
            _normalColor = stateColor.EnabledColor.ToAndroid();

            this.UpdateDisabledColor(stateColor);
            this.UpdatePressedColor(stateColor);
            this.UpdateBorderColor();

            if (_button.ButtonType == MaterialButtonType.Outlined || _button.ButtonType == MaterialButtonType.Text)
            {
                _normalColor = Color.Transparent;
                _disabledColor = Color.Transparent;
                _pressedColor = stateColor.PressedColor.IsDefault ? Color.ParseColor("#52000000") : stateColor.PressedColor.ToAndroid();
            }
        }

        private void UpdateCornerRadius()
        {
            _cornerRadius = (int)MaterialHelper.ConvertToDp(_button.CornerRadius);
        }

        private void UpdateDisabledColor(MaterialColor stateColor)
        {
            if (stateColor.DisabledColor.IsDefault)
            {
                _disabledColor = _normalColor.GetDisabledColor();
            }
            else
            {
                _disabledColor = stateColor.DisabledColor.ToAndroid();
            }
        }

        private void UpdateElevation()
        {
            if (!Material.IsLollipop)
            {
                return;
            }

            StateListAnimator stateListAnimator = null;

            if (_button.ButtonType == MaterialButtonType.Elevated && _aView.Enabled)
            {
                stateListAnimator = AnimatorInflater.LoadStateListAnimator(_aView.Context, Resource.Animator.material_button_state_list_anim);

                if (_aView is AppCompatImageButton)
                {
                    _aView.OutlineProvider = new MaterialOutlineProvider(_button.CornerRadius);
                    _aView.ClipToOutline = false;
                }
            }

            _aView.StateListAnimator = stateListAnimator;
        }

        private void UpdatePressedColor(MaterialColor stateColor)
        {
            if (stateColor.PressedColor.IsDefault)
            {
                _pressedColor = _normalColor.IsColorDark() ? Color.ParseColor("#52FFFFFF") : Color.ParseColor("#52000000");
            }
            else
            {
                _pressedColor = stateColor.PressedColor.ToAndroid();
            }
        }

        private class MaterialOutlineProvider : ViewOutlineProvider
        {
            private readonly int _cornerRadius;

            public MaterialOutlineProvider(int cornerRadius)
            {
                _cornerRadius = cornerRadius;
            }

            public override void GetOutline(Android.Views.View view, Outline outline)
            {
                var inset = (int)MaterialHelper.ConvertToDp(6);
                var cornerRadius = (int)MaterialHelper.ConvertToDp(_cornerRadius);

                outline.SetRoundRect(inset, inset, view.Width - inset, view.Height - inset, cornerRadius);
            }
        }
    }
}