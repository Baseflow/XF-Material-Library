using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI;
using static XF.Material.Forms.UI.MaterialButton;

namespace XF.Material.iOS.Renderers
{
    internal class MaterialCALayerHelper
    {
        private readonly IMaterialButton _button;
        private readonly UIButton _uiButton;
        private readonly BindableObject _bindableButton;
        private readonly Dictionary<string, Action> _propertyChangeActions;
        private readonly MaterialCALayerAnimationHelper _animationHelper;
        private double _borderWidth;
        private int _cornerRadius;
        private CGColor _disabledBorderColor;
        private CGColor _disabledColor;
        private CGColor _enabledBorderColor;
        private CGColor _normalColor;
        private CGColor _pressedColor;
        private CALayer _animationLayer;
        private readonly int? _layerInitialCount;
        private bool _colorSet;
        

        public MaterialCALayerHelper(IMaterialButton button, UIButton uiButton)
        {
            _button = button;
            _uiButton = uiButton;
            _bindableButton = (BindableObject)_button;
            _bindableButton.PropertyChanged += this.BindableButton_PropertyChanged;
            _layerInitialCount = uiButton.Layer?.Sublayers?.Length;

            _uiButton.TouchDown += this.Control_Pressed;
            _uiButton.TouchDragEnter += this.Control_Pressed;
            _uiButton.TouchUpInside += this.Control_Released;
            _uiButton.TouchCancel += this.Control_Released;
            _uiButton.TouchDragExit += this.Control_Released;

            _propertyChangeActions = new Dictionary<string, Action>
            {
                { MaterialButtonColorChanged, () =>
                    {
                        this.UpdateColors();
                        this.UpdateLayer();
                    }
                },
                { nameof(IMaterialButton.ButtonType), () =>
                    {
                        this.UpdateColors();
                        this.UpdateLayer();
                    }
                },
                { nameof(IMaterialButton.BorderColor), () =>
                    {
                        this.UpdateBorderColor();
                        this.UpdateLayer();
                    }
                },
                { nameof(IMaterialButton.BorderWidth), () =>
                    {
                        this.UpdateBorderWidth();
                        this.UpdateLayer();
                    }
                },
                { nameof(IMaterialButton.CornerRadius), () =>
                    {
                        this.UpdateCornerRadius();
                        this.UpdateLayer();
                    }
                },
                { nameof(VisualElement.IsEnabled), () =>
                    {
                        this.UpdateLayerState();
                        this.UpdateTextColor();
                        this.UpdateElevation();
                    }
                },
                { nameof(Button.Text), () =>
                    {
                        this.UpdateTextColor();
                    }
                },
            };
            _animationHelper = new MaterialCALayerAnimationHelper(_uiButton, _button.ButtonType);

            this.UpdateColors();
            this.UpdateCornerRadius();
            this.UpdateBorderWidth();
            this.UpdateTextColor();
            this.UpdateLayerState();
        }

        private void UpdateLayerState()
        {
            if(_button.ButtonType == MaterialButtonType.Text || _button.ButtonType == MaterialButtonType.Outlined || _animationLayer == null)
            {
                return;
            }

            if(_button.BackgroundColor.DisabledColor.IsDefault)
            {
                _animationLayer.BackgroundColor = _button.BackgroundColor.EnabledColor.ToCGColor().GetDisabledColor();
            }

            else
            {
                _animationLayer.BackgroundColor = _button.BackgroundColor.DisabledColor.ToCGColor();
            }

            _animationLayer.BorderColor = _uiButton.Enabled ? _enabledBorderColor : _disabledBorderColor;
        }

        private void UpdateTextColor()
        {
            if (_bindableButton is Button button && !string.IsNullOrEmpty(button.Text))
            {
                var textColor = button.TextColor.ToUIColor();
                var newTextColor = button.IsEnabled ? textColor.ColorWithAlpha(1.0f) : textColor.ColorWithAlpha(0.38f);

                var attributedString = new NSAttributedString(button.Text, _uiButton.Font, newTextColor, UIColor.Clear);
                _uiButton.SetAttributedTitle(attributedString, UIControlState.Normal);
                _uiButton.SetAttributedTitle(attributedString, UIControlState.Disabled);
                _uiButton.SetAttributedTitle(attributedString, UIControlState.Highlighted);
                _uiButton.SetAttributedTitle(attributedString, UIControlState.Focused);
            }

            else if(_bindableButton is MaterialIconButton iconButton)
            {
                var tintColor = iconButton.TintColor.ToUIColor();
                _uiButton.TintColor = iconButton.IsEnabled ? tintColor.ColorWithAlpha(1.0f) : tintColor.ColorWithAlpha(0.38f);
            }
        }

        private async void Control_Pressed(object sender, EventArgs e)
        {
            await _animationHelper.ButtonPressed(_uiButton.Layer.Sublayers.LastOrDefault());
        }

        private async void Control_Released(object sender, EventArgs e)
        {
            await _animationHelper.ButtonReleased(_uiButton.Layer.Sublayers.LastOrDefault());
        }

        public void Clean()
        {
            _bindableButton.PropertyChanged -= this.BindableButton_PropertyChanged;
            _uiButton.TouchDown -= this.Control_Pressed;
            _uiButton.TouchDragEnter -= this.Control_Pressed;
            _uiButton.TouchUpInside -= this.Control_Released;
            _uiButton.TouchCancel -= this.Control_Released;
            _uiButton.TouchDragExit -= this.Control_Released;
        }

        private void BindableButton_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_propertyChangeActions != null && _propertyChangeActions.TryGetValue(e?.PropertyName, out Action handlePropertyChange))
            {
                handlePropertyChange();
            }
        }

        private void UpdateBorderColor()
        {
            if (_button.ButtonType == MaterialButtonType.Text)
            {
                _enabledBorderColor = Color.Transparent.ToCGColor();
                return;
            }

            var borderColor = _button.BorderColor;

            _enabledBorderColor = borderColor.IsDefault ? Color.Transparent.ToCGColor() : borderColor.ToCGColor();
            _disabledBorderColor = borderColor.IsDefault ? Color.Transparent.ToCGColor() : _enabledBorderColor.GetDisabledColor();
        }

        private void UpdateBorderWidth()
        {
            if (_button.ButtonType == MaterialButtonType.Text)
            {
                _borderWidth = 0;
                return;
            }

            _borderWidth = _button.BorderWidth;
        }

        private void UpdateColors()
        {
            var stateColor = _button.BackgroundColor;
            _normalColor = stateColor.EnabledColor.ToCGColor();

            this.UpdateDisabledColor(stateColor);
            this.UpdatePressedColor(stateColor);
            this.UpdateBorderColor();

            if (_button.ButtonType == MaterialButtonType.Outlined || _button.ButtonType == MaterialButtonType.Text)
            {
                _normalColor = Color.Transparent.ToCGColor();
                _disabledColor = Color.Transparent.ToCGColor();
                _pressedColor = stateColor.PressedColor.IsDefault ? Color.FromHex("#52000000").ToCGColor() : stateColor.PressedColor.ToCGColor();
            }

            _animationHelper.UpdateColors(_normalColor, _pressedColor);
            _colorSet = true;
        }

        private void UpdateCornerRadius()
        {
            _cornerRadius = _button.CornerRadius;
        }

        private void UpdateDisabledColor(MaterialColor stateColor)
        {
            if (stateColor.DisabledColor.IsDefault)
            {
                _disabledColor = _normalColor.GetDisabledColor();
            }
            else
            {
                _disabledColor = stateColor.DisabledColor.ToCGColor();
            }
        }

        private void UpdateElevation()
        {
            var elevated = _button.ButtonType == MaterialButtonType.Elevated && _uiButton.Enabled;

            _animationHelper.UpdateElevation(elevated);
        }

        private void UpdatePressedColor(MaterialColor stateColor)
        {
            if (stateColor.PressedColor.IsDefault)
            {
                _pressedColor = _normalColor.IsColorDark() ? Color.FromHex("#52FFFFFF").ToCGColor() : Color.FromHex("#52000000").ToCGColor();
            }
            else
            {
                _pressedColor = stateColor.PressedColor.ToCGColor();
            }
        }

        public void UpdateLayer()
        {
            _uiButton.Layer.BorderColor = UIColor.Clear.CGColor;

            CALayer layer = null;

            if(_uiButton.Layer.Sublayers?.Length > _layerInitialCount)
            {
                layer = _uiButton.Layer.Sublayers.FirstOrDefault();
            }

            using (layer = layer ?? new CALayer())
            {
                var width = _uiButton.Frame.Width - 12;
                var height = _uiButton.Frame.Height - 12;

                if(_cornerRadius > (int)height / 2)
                {
                    _cornerRadius = (int)height / 2;
                }

                layer.BackgroundColor = _normalColor;
                layer.CornerRadius = _cornerRadius;
                layer.BorderColor = _enabledBorderColor;
                layer.BorderWidth = (nfloat)_borderWidth;
                layer.Frame = new CGRect(6, 6, width, height);

                _uiButton.Layer.CornerRadius = _cornerRadius;
                _uiButton.Layer.InsertSublayer(layer, 0);
                _uiButton.BringSubviewToFront(_uiButton.ImageView);
                _animationLayer = layer;
            }

            this.UpdateElevation();
        }
    }
}