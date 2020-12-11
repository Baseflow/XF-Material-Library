using System;
using System.ComponentModel;
using System.Threading.Tasks;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI;
using XF.Material.iOS.Delegates;
using XF.Material.iOS.Renderers;

[assembly: ExportRenderer(typeof(MaterialButton), typeof(MaterialButtonRenderer))]

namespace XF.Material.iOS.Renderers
{
    public class MaterialButtonRenderer : ButtonRenderer
    {
        private CALayer _animationLayer;
        private UIColor _borderColor;
        private CABasicAnimation _colorPressed;
        private CABasicAnimation _colorResting;
        private UIColor _disabledBackgroundColor;
        private UIColor _disabledBorderColor;
        private UIColor _disabledTextColor;
        private MaterialButton _materialButton;
        private UIColor _normalTextColor;
        private UIColor _pressedBackgroundColor;
        private UIColor _restingBackgroundColor;
        private UIColor _rippleColor;
        private CABasicAnimation _shadowOffsetPressed;
        private CABasicAnimation _shadowOffsetResting;
        private CABasicAnimation _shadowRadiusPressed;
        private CABasicAnimation _shadowRadiusResting;
        private UIGestureRecognizer _rippleGestureRecognizer;
        private bool _withIcon;

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            UpdateLayerFrame();
            UpdateCornerRadius();
            UpdateButtonLayer();
            UpdateState();
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
                Control.TouchDown -= Control_Pressed;
                Control.TouchDragEnter -= Control_Pressed;
                Control.TouchUpInside -= Control_Released;
                Control.TouchCancel -= Control_Released;
                Control.TouchDragExit -= Control_Released;
            }

            if (e?.NewElement != null)
            {
                _materialButton = Element as MaterialButton;

                if (_materialButton != null)
                {
                    _withIcon = _materialButton.Image != null || _materialButton.ImageSource != null && !_materialButton.ImageSource.IsEmpty;
                }

                SetupColors();
                CreateStateAnimations();
                UpdateButtonLayer();
                UpdateState();
                UpdateText();
                Control.TouchDown += Control_Pressed;
                Control.TouchDragEnter += Control_Pressed;
                Control.TouchUpInside += Control_Released;
                Control.TouchCancel += Control_Released;
                Control.TouchDragExit += Control_Released;
            }
        }

        protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
            {
                return;
            }

            if (e.PropertyName == nameof(Button.IsEnabled))
            {
                UpdateState();
            }

            switch (e.PropertyName)
            {
                case MaterialButton.MaterialButtonColorChanged:
                case nameof(MaterialButton.ButtonType):
                    SetupColors();
                    CreateStateAnimations();
                    UpdateButtonLayer();
                    await UpdateBackgroundColor();
                    break;

                case nameof(MaterialButton.ImageSource):
                case nameof(MaterialButton.Image):
                    UpdateButtonLayer();
                    SetupIcon();
                    break;

                case nameof(MaterialButton.CornerRadius):
                    UpdateCornerRadius();
                    break;

                case nameof(MaterialButton.Text):
                case nameof(MaterialButton.TextColor):
                case nameof(MaterialButton.AllCaps):
                    UpdateText();
                    break;
            }
        }

        private async void Control_Pressed(object sender, EventArgs e)
        {
            if (_materialButton.ButtonType == MaterialButtonType.Elevated)
            {
                await AnimateAsync(0.150, () =>
                {
                    Control.Layer.AddAnimation(_shadowOffsetPressed, "shadowOffsetPressed");
                    Control.Layer.AddAnimation(_shadowRadiusPressed, "shadowRadiusPressed");
                    _animationLayer.AddAnimation(_colorPressed, "backgroundColorPressed");
                });
            }
            else
            {
                await AnimateAsync(0.300, () => _animationLayer.AddAnimation(_colorPressed, "backgroundColorPressed"));
            }
        }

        private async void Control_Released(object sender, EventArgs e)
        {
            _colorResting.From = _colorPressed.ValueForKeyPath(new NSString("backgroundColor"));
            _animationLayer.RemoveAnimation("backgroundColorPressed");

            if (_materialButton.ButtonType == MaterialButtonType.Elevated && Control?.Layer != null)
            {
                var shadowOffsetAnim = Control.Layer.AnimationForKey("shadowOffsetPressed");
                _shadowOffsetResting.From = shadowOffsetAnim.ValueForKeyPath(new NSString("shadowOffset"));

                var shadowRadiusAnim = Control.Layer.AnimationForKey("shadowRadiusPressed");
                _shadowRadiusResting.From = shadowRadiusAnim.ValueForKeyPath(new NSString("shadowRadius"));

                await AnimateAsync(0.150, () =>
                {
                    Control.Layer.AddAnimation(_shadowOffsetResting, "shadowOffsetResting");
                    Control.Layer.AddAnimation(_shadowRadiusResting, "shadowRadiusResting");
                    _animationLayer.AddAnimation(_colorResting, "backgroundColorResting");
                });
            }
            else
            {
                await AnimateAsync(0.300, () => _animationLayer.AddAnimation(_colorResting, "backgroundColorResting"));
            }
        }

        private void CreateContainedButtonLayer(bool elevated)
        {
            _animationLayer.BackgroundColor = _restingBackgroundColor.CGColor;
            _animationLayer.BorderColor = _borderColor.CGColor;
            _animationLayer.BorderWidth = (nfloat)_materialButton.BorderWidth;

            if (elevated)
            {
                Control.Elevate(_materialButton.Elevation.RestingElevation);
            }
        }

        private void CreateOutlinedButtonLayer()
        {
            _animationLayer.BorderColor = _materialButton.BorderColor.ToCGColor();
            _animationLayer.BorderWidth = (nfloat)_materialButton.BorderWidth;
        }

        private void CreateStateAnimations()
        {
            if (_materialButton.ButtonType == MaterialButtonType.Elevated)
            {
                _shadowOffsetPressed = CABasicAnimation.FromKeyPath("shadowOffset");
                _shadowOffsetPressed.Duration = 0.150;
                _shadowOffsetPressed.FillMode = CAFillMode.Forwards;
                _shadowOffsetPressed.RemovedOnCompletion = false;
                _shadowOffsetPressed.To = FromObject(new CGSize(0, _materialButton.Elevation.PressedElevation));

                _shadowRadiusPressed = CABasicAnimation.FromKeyPath("shadowRadius");
                _shadowRadiusPressed.Duration = 0.150;
                _shadowRadiusPressed.FillMode = CAFillMode.Forwards;
                _shadowRadiusPressed.RemovedOnCompletion = false;
                _shadowRadiusPressed.To = NSNumber.FromDouble(_materialButton.Elevation.PressedElevation);

                _shadowOffsetResting = CABasicAnimation.FromKeyPath("shadowOffset");
                _shadowOffsetResting.Duration = 0.150;
                _shadowOffsetResting.FillMode = CAFillMode.Forwards;
                _shadowOffsetResting.RemovedOnCompletion = false;
                _shadowOffsetResting.To = FromObject(new CGSize(0, _materialButton.Elevation.RestingElevation));

                _shadowRadiusResting = CABasicAnimation.FromKeyPath("shadowRadius");
                _shadowRadiusResting.Duration = 0.150;
                _shadowRadiusResting.FillMode = CAFillMode.Forwards;
                _shadowRadiusResting.RemovedOnCompletion = false;
                _shadowRadiusResting.To = NSNumber.FromDouble(_materialButton.Elevation.RestingElevation);
            }

            _colorPressed = CABasicAnimation.FromKeyPath("backgroundColor");
            _colorPressed.Duration = 0.3;
            _colorPressed.FillMode = CAFillMode.Forwards;
            _colorPressed.RemovedOnCompletion = false;
            _colorPressed.To = FromObject(_pressedBackgroundColor.CGColor);

            _colorResting = CABasicAnimation.FromKeyPath("backgroundColor");
            _colorResting.Duration = 0.3;
            _colorResting.FillMode = CAFillMode.Forwards;
            _colorResting.RemovedOnCompletion = false;
            _colorResting.To = FromObject(_restingBackgroundColor.CGColor);

            if (_rippleGestureRecognizer == null)
            {
                _rippleGestureRecognizer = new UITapGestureRecognizer() { Delegate = new MaterialRippleGestureRecognizerDelegate(_rippleColor.CGColor) };
                Control.AddGestureRecognizer(_rippleGestureRecognizer);
            }
        }

        private void CreateTextButtonLayer()
        {
            _animationLayer.BorderColor = UIColor.Clear.CGColor;
            _animationLayer.BorderWidth = 0f;
        }

        private void SetupColors()
        {
            if (_materialButton.ButtonType == MaterialButtonType.Elevated || _materialButton.ButtonType == MaterialButtonType.Flat)
            {
                _restingBackgroundColor = _materialButton.BackgroundColor.ToUIColor();
                _disabledBackgroundColor = _materialButton.DisabledBackgroundColor.IsDefault ? _materialButton.BackgroundColor.ToUIColor().GetDisabledColor() : _materialButton.DisabledBackgroundColor.ToUIColor();

                if (_materialButton.PressedBackgroundColor.IsDefault)
                {
                    _rippleColor = _materialButton.BackgroundColor.ToUIColor().IsColorDark() ? Color.FromHex("#52FFFFFF").ToUIColor() : Color.FromHex("#52000000").ToUIColor();
                    _pressedBackgroundColor = _restingBackgroundColor.IsColorDark() ? _restingBackgroundColor.LightenColor() : _restingBackgroundColor.DarkenColor();
                }
                else
                {
                    _rippleColor = _materialButton.PressedBackgroundColor.ToUIColor();
                    _pressedBackgroundColor = _restingBackgroundColor.MixColor(_rippleColor);
                }
            }
            else
            {
                _restingBackgroundColor = UIColor.Clear;
                _disabledBackgroundColor = UIColor.Clear;
                _rippleColor = _materialButton.PressedBackgroundColor.IsDefault ? Color.FromHex("#52000000").ToUIColor() : _materialButton.PressedBackgroundColor.ToUIColor();
                _pressedBackgroundColor = _rippleColor;
            }

            _borderColor = _materialButton.ButtonType != MaterialButtonType.Text ? _materialButton.BorderColor.ToUIColor() : UIColor.Clear;
            _disabledBorderColor = _borderColor.GetDisabledColor();
            _normalTextColor = _materialButton.TextColor.ToUIColor();
            _disabledTextColor = _normalTextColor.GetDisabledColor();
        }

        private void SetupIcon()
        {
            if (_withIcon)
            {
                var control = Control;
                control.TintColor = _materialButton.TextColor.ToUIColor();

                //if (Element.ContentLayout.Position == Button.ButtonContentLayout.ImagePosition.Right)
                //{
                //    //XF does not compute correctly the text's width
                //    var imageSize = control.CurrentImage.Size;

                //    if (imageSize.Width > 0 && imageSize.Height > 0)
                //    {
                //        var insets = Control.ImageEdgeInsets;
                //        var margin = (nfloat)Element.ContentLayout.Spacing;
                //        var deltaX = Control.CurrentAttributedTitle.Size.Width + Control.TitleEdgeInsets.Left + Control.TitleEdgeInsets.Right + margin;
                //        Control.ImageEdgeInsets = new UIEdgeInsets(insets.Top, deltaX, insets.Bottom, -deltaX);
                //    }
                //}
            }
        }

        ///// <summary>
        ///// Computes the intrinsic content size
        ///// </summary>
        ///// <param name="size"></param>
        ///// <remarks>
        ///// Because CharacterSpacing is non standard, the width is incorrectly computed,
        ///// leading to the button's width being too small.
        ///// Adding 2x14 seems to fit all situations.
        ///// </remarks>
        //public override CGSize SizeThatFits(CGSize size)
        //{
        //    size = base.SizeThatFits(size);
        //    if(size.Width > 0) //A negative value means "whatever"
        //        size.Width += 28;
        //    return size;
        //}

        private Task<bool> UpdateBackgroundColor()
        {
            var colorAnim = CABasicAnimation.FromKeyPath("backgroundColor");
            colorAnim.Duration = 0.150;
            colorAnim.FillMode = CAFillMode.Forwards;
            colorAnim.RemovedOnCompletion = false;
            colorAnim.To = FromObject(_restingBackgroundColor.CGColor);

            return AnimateAsync(150, () => _animationLayer.AddAnimation(colorAnim, "colorAnim"));
        }

        private void UpdateButtonLayer()
        {
            if (Control == null)
            {
                return;
            }

            if (_animationLayer == null)
            {
                _animationLayer = new CALayer();
                Control.Layer.InsertSublayer(_animationLayer, 0);
            }

            switch (_materialButton.ButtonType)
            {
                case MaterialButtonType.Elevated:
                    CreateContainedButtonLayer(true);
                    break;

                case MaterialButtonType.Flat:
                    CreateContainedButtonLayer(false);
                    break;

                case MaterialButtonType.Outlined:
                    CreateOutlinedButtonLayer();
                    break;

                case MaterialButtonType.Text:
                    CreateTextButtonLayer();
                    break;
            }
            
            _animationLayer.SetNeedsDisplay();
            SetNeedsDisplay();
        }

        private void UpdateCornerRadius()
        {
            if (Control == null || _animationLayer == null)
            {
                return;
            }

            var maxCornerRadius = _animationLayer.Frame.Height / 2;
            var preferredCornerRadius = _materialButton.CornerRadius > maxCornerRadius ? maxCornerRadius : _materialButton.CornerRadius;

            _animationLayer.CornerRadius = Convert.ToInt32(preferredCornerRadius);
            Control.Layer.CornerRadius = _animationLayer.CornerRadius;
        }

        private void UpdateLayerFrame()
        {
            if (Control == null)
            {
                return;
            }

            var width = Control.Frame.Width - 12;
            var height = Control.Frame.Height - 12;

            _animationLayer.Frame = new CGRect(6, 6, width, height);

            Control.Layer.BackgroundColor = UIColor.Clear.CGColor;
            Control.Layer.BorderColor = UIColor.Clear.CGColor;
        }

        private void UpdateState()
        {
            if (Control == null)
            {
                return;
            }

            if (_materialButton.IsEnabled)
            {
                _animationLayer.BackgroundColor = _restingBackgroundColor.CGColor;
                _animationLayer.BorderColor = _borderColor.CGColor;

                if (_materialButton.ButtonType == MaterialButtonType.Elevated)
                {
                    Control.Elevate(_materialButton.Elevation.RestingElevation);
                }

                switch (_materialButton.ButtonType)
                {
                    case MaterialButtonType.Elevated:
                    case MaterialButtonType.Flat:
                        _materialButton.TextColor = _normalTextColor.ToColor();
                        break;

                    case MaterialButtonType.Text:
                    case MaterialButtonType.Outlined:
                        Control.Alpha = 1f;
                        break;
                }
            }
            else
            {
                _animationLayer.BackgroundColor = _disabledBackgroundColor.CGColor;
                _animationLayer.BorderColor = _disabledBorderColor.CGColor;

                if (_materialButton.ButtonType == MaterialButtonType.Elevated)
                {
                    Control.Elevate(0);
                }

                switch (_materialButton.ButtonType)
                {
                    case MaterialButtonType.Elevated:
                    case MaterialButtonType.Flat:
                        _materialButton.TextColor = _disabledTextColor.ToColor();
                        break;

                    case MaterialButtonType.Text:
                    case MaterialButtonType.Outlined:
                        Control.Alpha = 0.38f;
                        break;
                }
            }
        }

        private void UpdateText()
        {

            var text = Element.Text ?? String.Empty;
            if (_materialButton.AllCaps)
                text = text.ToUpper();

            var title = new NSMutableAttributedString(text, kerning: (float)_materialButton.CharacterSpacing, foregroundColor: _materialButton.TextColor.ToUIColor());
            Control.SetAttributedTitle(title, UIControlState.Normal);
            Control.SetAttributedTitle(title, UIControlState.Highlighted);
            Control.SetAttributedTitle(title, UIControlState.Disabled);

            SetupIcon();
        }
    }
}
