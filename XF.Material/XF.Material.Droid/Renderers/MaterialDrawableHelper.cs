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
using R = Android.Resource;

namespace XF.Material.Droid.Renderers
{
    internal class MaterialDrawableHelper
    {
        private readonly Android.Views.View _aView;
        private readonly BindableObject _bindableButton;
        private readonly IMaterialButtonControl _button;
        private readonly Dictionary<string, Action> _propertyChangeActions;
        private int _borderWidth;
        private float _cornerRadius;
        private Color _disabledBorderColor;
        private Color _disabledColor;
        private Color _enabledBorderColor;
        private Color _normalColor;
        private Color _pressedColor;
        private bool _withIcon;

        public MaterialDrawableHelper(IMaterialButtonControl button, Android.Views.View aView)
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
                { nameof(IMaterialButtonControl.ButtonType), () =>
                    {
                        this.UpdateColors();
                        this.UpdateDrawable();
                    }
                },
                { nameof(IMaterialButtonControl.BorderColor), () =>
                    {
                        this.UpdateBorderColor();
                        this.UpdateDrawable();
                    }
                },
                { nameof(IMaterialButtonControl.BorderWidth), () =>
                    {
                        this.UpdateBorderWidth();
                        this.UpdateDrawable();
                    }
                },
                { nameof(IMaterialButtonControl.CornerRadius), () =>
                    {
                        this.UpdateCornerRadius();
                        this.UpdateDrawable();
                    }
                },
                { nameof(IMaterialButtonControl.Elevation), this.UpdateElevation
                },
                { nameof(VisualElement.IsEnabled), this.UpdateElevation
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
            _aView.Background = this.GetDrawable();

            this.UpdateElevation();
        }

        public void UpdateHasIcon(bool hasIcon)
        {
            _withIcon = hasIcon;
        }

        private void BindableButton_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var prop = e?.PropertyName;

            if (prop == null) return;
            if (_propertyChangeActions != null && _propertyChangeActions.TryGetValue(prop, out var handlePropertyChange))
            {
                handlePropertyChange();
            }
        }

        private GradientDrawable CreateShapeDrawable(float cornerRadius, int borderWidth, Color backgroundColor, Color borderColor)
        {
            GradientDrawable shapeDrawable;

            if (_button.ButtonType != MaterialButtonType.Text)
            {
                shapeDrawable = _withIcon ? MaterialHelper.GetDrawableCopyFromResource<GradientDrawable>(Resource.Drawable.drawable_shape_with_icon) 
                                          : MaterialHelper.GetDrawableCopyFromResource<GradientDrawable>(Resource.Drawable.drawable_shape);
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
                if (rippleDrawable.FindDrawableByLayerId(Resource.Id.inset_drawable) is InsetDrawable insetDrawable)
                {
                    var stateListDrawable = insetDrawable.Drawable as StateListDrawable;
                    SetStates(stateListDrawable, normalStateDrawable, normalStateDrawable, disabledStateDrawable);
                }

                rippleDrawable.SetColor(new ColorStateList(new[]
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
                StateListDrawable stateListDrawable;
                Drawable backgroundDrawable;

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

                SetStates(stateListDrawable, normalStateDrawable, pressedStateDrawable, disabledStateDrawable);

                return backgroundDrawable;
            }
        }

        private RippleDrawable GetRippleDrawable()
        {
            RippleDrawable rippleDrawable;

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

            if (!(rippleDrawable.FindDrawableByLayerId(Android.Resource.Id.Mask) is InsetDrawable maskDrawable))
                return rippleDrawable;
            if (maskDrawable.Drawable is GradientDrawable rippleMaskGradientDrawable) rippleMaskGradientDrawable.SetCornerRadius(_cornerRadius);

            return rippleDrawable;
        }

        private static void SetStates(StateListDrawable stateListDrawable, Drawable normalDrawable, Drawable pressedDrawable, Drawable disabledDrawable)
        {
            stateListDrawable.AddState(new[] { Android.Resource.Attribute.StatePressed }, pressedDrawable);
            stateListDrawable.AddState(new[] { Android.Resource.Attribute.StateFocused, Android.Resource.Attribute.StateEnabled }, pressedDrawable);
            stateListDrawable.AddState(new[] { Android.Resource.Attribute.StateEnabled }, normalDrawable);
            stateListDrawable.AddState(new[] { Android.Resource.Attribute.StateFocused }, pressedDrawable);
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
            _normalColor = _button.BackgroundColor.ToAndroid();

            this.UpdateDisabledColor(_button.DisabledBackgroundColor);
            this.UpdatePressedColor(_button.PressedBackgroundColor);
            this.UpdateBorderColor();

            if (_button.ButtonType != MaterialButtonType.Outlined &&
                _button.ButtonType != MaterialButtonType.Text) return;
            _normalColor = Color.Transparent;
            _disabledColor = Color.Transparent;
            _pressedColor = _button.PressedBackgroundColor.IsDefault ? Color.ParseColor("#52000000") : _button.PressedBackgroundColor.ToAndroid();
        }

        private void UpdateCornerRadius()
        {
            _cornerRadius = (int)MaterialHelper.ConvertToDp(_button.CornerRadius);
        }

        private void UpdateDisabledColor(Xamarin.Forms.Color disabledColor)
        {
            _disabledColor = disabledColor.IsDefault ? _normalColor.GetDisabledColor() : disabledColor.ToAndroid();
        }

        private void UpdateElevation()
        {
            if (!Material.IsLollipop)
            {
                return;
            }

            _aView.StateListAnimator = this.CreateStateListAnimator();
        }

        private StateListAnimator CreateStateListAnimator()
        {
            if (_button.ButtonType != MaterialButtonType.Elevated || !_aView.Enabled) return null;
            var stateListAnimator = new StateListAnimator();

            var objAnimTransZEnabled = ObjectAnimator.OfFloat(_aView, "translationZ", MaterialHelper.ConvertToDp(_button.Elevation.RestingElevation))
                .SetDuration(100);
            objAnimTransZEnabled.StartDelay = 100;

            var objAnimElevationEnabled = ObjectAnimator.OfFloat(_aView, "elevation", MaterialHelper.ConvertToDp(_button.Elevation.RestingElevation))
                .SetDuration(0);

            var objAnimTransZPressed = ObjectAnimator.OfFloat(_aView, "translationZ", MaterialHelper.ConvertToDp(_button.Elevation.PressedElevation))
                .SetDuration(100);

            var objAnimElevationPressed = ObjectAnimator.OfFloat(_aView, "elevation", MaterialHelper.ConvertToDp(_button.Elevation.RestingElevation))
                .SetDuration(0);

            var b = new AnimatorSet();
            b.PlayTogether(objAnimTransZEnabled, objAnimElevationEnabled);
            b.SetTarget(_aView);

            var bb = new AnimatorSet();
            bb.PlayTogether(objAnimTransZPressed, objAnimElevationPressed);
            bb.SetTarget(_aView);


            stateListAnimator.AddState(new[] { R.Attribute.StatePressed }, bb);
            stateListAnimator.AddState(new[] { R.Attribute.StateFocused, R.Attribute.StateEnabled }, bb);
            stateListAnimator.AddState(new[] { R.Attribute.StateEnabled }, b);
            stateListAnimator.AddState(new[] { R.Attribute.StateFocused }, bb);

            if (!(_aView is AppCompatImageButton)) return stateListAnimator;
            _aView.OutlineProvider = new MaterialOutlineProvider(_button.CornerRadius);
            _aView.ClipToOutline = false;

            return stateListAnimator;

        }

        private void UpdatePressedColor(Xamarin.Forms.Color pressedColor)
        {
            if (pressedColor.IsDefault)
            {
                _pressedColor = _normalColor.IsColorDark() ? Color.ParseColor("#52FFFFFF") : Color.ParseColor("#52000000");
            }
            else
            {
                _pressedColor = pressedColor.ToAndroid();
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