using CoreAnimation;
using CoreGraphics;
using Foundation;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
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

            this.UpdateLayerFrame();
            this.UpdateCornerRadius();
            this.UpdateTextSizing();
            this.UpdateButtonLayer();
            this.UpdateState();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null) return;

            if (e?.OldElement != null)
            {
                this.Control.TouchDown -= this.Control_Pressed;
                this.Control.TouchDragEnter -= this.Control_Pressed;
                this.Control.TouchUpInside -= this.Control_Released;
                this.Control.TouchCancel -= this.Control_Released;
                this.Control.TouchDragExit -= this.Control_Released;
            }

            if (e?.NewElement != null)
            {
                _materialButton = this.Element as MaterialButton;

                if (_materialButton != null)
                {
                    _withIcon = _materialButton.Image != null;

                    if (_materialButton.AllCaps)
                    {
                        _materialButton.Text = _materialButton.Text?.ToUpper();
                    }
                }

                this.SetupIcon();
                this.SetupColors();
                this.UpdateText();
                this.CreateStateAnimations();
                this.UpdateButtonLayer();
                this.UpdateState();
                this.Control.TouchDown += this.Control_Pressed;
                this.Control.TouchDragEnter += this.Control_Pressed;
                this.Control.TouchUpInside += this.Control_Released;
                this.Control.TouchCancel += this.Control_Released;
                this.Control.TouchDragExit += this.Control_Released;
            }
        }

        protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (this.Control == null) return;

            if (e.PropertyName == nameof(Button.IsEnabled))
            {
                this.UpdateState();
            }

            switch (e.PropertyName)
            {
                case MaterialButton.MaterialButtonColorChanged:
                case nameof(MaterialButton.ButtonType):
                    this.SetupColors();
                    this.CreateStateAnimations();
                    this.UpdateButtonLayer();
                    await this.UpdateBackgroundColor();
                    break;

                case nameof(MaterialButton.Image):
                    this.SetupIcon();
                    this.UpdateButtonLayer();
                    break;

                case nameof(MaterialButton.AllCaps):
                    _materialButton.Text = _materialButton.AllCaps ? _materialButton.Text.ToUpper() : _materialButton.Text.ToLower();
                    break;

                case nameof(MaterialButton.CornerRadius):
                    this.UpdateCornerRadius();
                    break;

                case nameof(MaterialButton.LetterSpacing):
                    this.UpdateText();
                    break;

                case nameof(MaterialButton.Text):
                    this.UpdateText();
                    this.UpdateTextSizing();
                    break;

                case nameof(MaterialButton.Padding):
                    this.UpdatePadding();
                    break;
            }
        }

        private async void Control_Pressed(object sender, EventArgs e)
        {
            if (_materialButton.ButtonType == MaterialButtonType.Elevated)
            {
                await AnimateAsync(0.150, () =>
                {
                    this.Control.Layer.AddAnimation(_shadowOffsetPressed, "shadowOffsetPressed");
                    this.Control.Layer.AddAnimation(_shadowRadiusPressed, "shadowRadiusPressed");
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

            if (_materialButton.ButtonType == MaterialButtonType.Elevated)
            {
                var shadowOffsetAnim = this.Control.Layer.AnimationForKey("shadowOffsetPressed");
                _shadowOffsetResting.From = shadowOffsetAnim.ValueForKeyPath(new NSString("shadowOffset"));

                var shadowRadiusAnim = this.Control.Layer.AnimationForKey("shadowRadiusPressed");
                _shadowRadiusResting.From = shadowRadiusAnim.ValueForKeyPath(new NSString("shadowRadius"));

                await AnimateAsync(0.150, () =>
                {
                    this.Control.Layer.AddAnimation(_shadowOffsetResting, "shadowOffsetResting");
                    this.Control.Layer.AddAnimation(_shadowRadiusResting, "shadowRadiusResting");
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
                this.Control.Elevate(_materialButton.Elevation.RestingElevation);
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
                _shadowRadiusPressed.To = NSNumber.FromFloat(_materialButton.Elevation.PressedElevation);

                _shadowOffsetResting = CABasicAnimation.FromKeyPath("shadowOffset");
                _shadowOffsetResting.Duration = 0.150;
                _shadowOffsetResting.FillMode = CAFillMode.Forwards;
                _shadowOffsetResting.RemovedOnCompletion = false;
                _shadowOffsetResting.To = FromObject(new CGSize(0, _materialButton.Elevation.RestingElevation));

                _shadowRadiusResting = CABasicAnimation.FromKeyPath("shadowRadius");
                _shadowRadiusResting.Duration = 0.150;
                _shadowRadiusResting.FillMode = CAFillMode.Forwards;
                _shadowRadiusResting.RemovedOnCompletion = false;
                _shadowRadiusResting.To = NSNumber.FromFloat(_materialButton.Elevation.RestingElevation);
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


            if(_rippleGestureRecognizer == null)
            {
                _rippleGestureRecognizer = new UITapGestureRecognizer() { Delegate = new MaterialRippleGestureRecognizerDelegate(_rippleColor.CGColor) };
                this.Control.AddGestureRecognizer(_rippleGestureRecognizer);
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
                UIImage image = null;

                try
                {
                    image = UIImage.FromFile(_materialButton.Image.File) ?? UIImage.FromBundle(_materialButton.Image.File);

                    UIGraphics.BeginImageContextWithOptions(new CGSize(18, 18), false, 0f);
                    image?.Draw(new CGRect(0, 0, 18, 18));

                    using (var newImage = UIGraphics.GetImageFromCurrentImageContext())
                    {
                        UIGraphics.EndImageContext();

                        this.Control.SetImage(newImage, UIControlState.Normal);
                        this.Control.SetImage(newImage, UIControlState.Disabled);

                        this.Control.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
                        this.Control.ImageEdgeInsets = new UIEdgeInsets(0f, 0f, 0f, 0f);
                        this.Control.TintColor = _materialButton.TextColor.ToUIColor();
                    }
                }
                finally
                {
                    image?.Dispose();
                }
            }
            else
            {
                this.Control.TitleEdgeInsets = new UIEdgeInsets(0, 0, 0, 0);
                this.Control.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            }
        }

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
            if (this.Control == null) return;

            if (_animationLayer == null)
            {
                _animationLayer = new CALayer();
                this.Control.Layer.InsertSublayer(_animationLayer, 0);
            }

            switch (_materialButton.ButtonType)
            {
                case MaterialButtonType.Elevated:
                    this.CreateContainedButtonLayer(true);
                    break;

                case MaterialButtonType.Flat:
                    this.CreateContainedButtonLayer(false);
                    break;

                case MaterialButtonType.Outlined:
                    this.CreateOutlinedButtonLayer();
                    break;

                case MaterialButtonType.Text:
                    this.CreateTextButtonLayer();
                    break;
            }

            this.UpdatePadding();
        }

        private void UpdatePadding()
        {
            if (this.Control == null) return;

            var additionalPadding = this.Element.Padding;

            if (_materialButton.ButtonType != MaterialButtonType.Text && _withIcon)
            {
                this.Control.ContentEdgeInsets = new UIEdgeInsets(
                    10f + (nfloat)additionalPadding.Top,
                    18f + (nfloat)additionalPadding.Left,
                    10f + (nfloat)additionalPadding.Bottom,
                    22f + (nfloat)additionalPadding.Right);
            }
            else if (_materialButton.ButtonType != MaterialButtonType.Text && !_withIcon)
            {
                this.Control.ContentEdgeInsets = new UIEdgeInsets(
                    10f + (nfloat)additionalPadding.Top,
                    22f + (nfloat)additionalPadding.Left,
                    10f + (nfloat)additionalPadding.Bottom,
                    22f + (nfloat)additionalPadding.Right);
            }
            else if (_materialButton.ButtonType is MaterialButtonType.Text && _withIcon)
            {
                this.Control.ContentEdgeInsets = new UIEdgeInsets(
                    10f + (nfloat)additionalPadding.Top,
                    18f + (nfloat)additionalPadding.Left,
                    10f + (nfloat)additionalPadding.Bottom,
                    22f + (nfloat)additionalPadding.Right);
            }
            else if (_materialButton.ButtonType is MaterialButtonType.Text && !_withIcon)
            {
                this.Control.ContentEdgeInsets = new UIEdgeInsets(
                    10f + (nfloat)additionalPadding.Top,
                    14f + (nfloat)additionalPadding.Left,
                    10f + (nfloat)additionalPadding.Bottom,
                    14f + (nfloat)additionalPadding.Right);
            }
        }

        private void UpdateCornerRadius()
        {
            if (this.Control == null || _animationLayer == null)
            {
                return;
            }

            var maxCornerRadius = _animationLayer.Frame.Height / 2;
            var preferredCornerRadius = _materialButton.CornerRadius > maxCornerRadius ? maxCornerRadius : _materialButton.CornerRadius;

            _animationLayer.CornerRadius = Convert.ToInt32(preferredCornerRadius);
            this.Control.Layer.CornerRadius = _animationLayer.CornerRadius;
        }

        private void UpdateLayerFrame()
        {
            if (this.Control == null) return;

            var width = this.Control.Frame.Width - 12;
            var height = this.Control.Frame.Height - 12;

            _animationLayer.Frame = new CGRect(6, 6, width, height);

            this.Control.Layer.BackgroundColor = UIColor.Clear.CGColor;
            this.Control.Layer.BorderColor = UIColor.Clear.CGColor;
        }

        private void UpdateState()
        {
            if (this.Control == null) return;

            if (_materialButton.IsEnabled)
            {
                _animationLayer.BackgroundColor = _restingBackgroundColor.CGColor;
                _animationLayer.BorderColor = _borderColor.CGColor;

                if (_materialButton.ButtonType == MaterialButtonType.Elevated)
                {
                    this.Control.Elevate(_materialButton.Elevation.RestingElevation);
                }

                switch (_materialButton.ButtonType)
                {
                    case MaterialButtonType.Elevated:
                    case MaterialButtonType.Flat:
                        _materialButton.TextColor = _normalTextColor.ToColor();
                        break;

                    case MaterialButtonType.Text:
                    case MaterialButtonType.Outlined:
                        this.Control.Alpha = 1f;
                        break;
                }
            }
            else
            {
                _animationLayer.BackgroundColor = _disabledBackgroundColor.CGColor;
                _animationLayer.BorderColor = _disabledBorderColor.CGColor;

                if (_materialButton.ButtonType == MaterialButtonType.Elevated)
                {
                    this.Control.Elevate(0);
                }

                switch (_materialButton.ButtonType)
                {
                    case MaterialButtonType.Elevated:
                    case MaterialButtonType.Flat:
                        _materialButton.TextColor = _disabledTextColor.ToColor();
                        break;

                    case MaterialButtonType.Text:
                    case MaterialButtonType.Outlined:
                        this.Control.Alpha = 0.38f;
                        break;
                }
            }
        }

        private void UpdateText()
        {
            if (this.Control == null) return;

            var text = this.Control.Title(UIControlState.Normal);
            text = _materialButton.AllCaps ? text?.ToUpper() : text;

            var attributedString = new NSMutableAttributedString(text ?? string.Empty,
                font: this.Control.Font,
                foregroundColor: this.Control.TitleColor(UIControlState.Normal),
                kerning: (float)_materialButton.LetterSpacing);

            this.Control.SetAttributedTitle(attributedString, UIControlState.Normal);
            this.Control.SetAttributedTitle(attributedString, UIControlState.Focused);
            this.Control.SetAttributedTitle(attributedString, UIControlState.Highlighted);
            this.Control.SetAttributedTitle(attributedString, UIControlState.Selected);
        }

        private void UpdateTextSizing()
        {
            if (this.Control == null) return;

            if (string.IsNullOrEmpty(_materialButton.Text) || !_withIcon)
            {
                this.Control.TitleEdgeInsets = new UIEdgeInsets(0, 0, 0, 0);
                return;
            }

            // We have to set the button title insets to make the button look
            // like Android's material buttons. (icon on left, text is centralized)

            var textToMeasure = (NSString)(_materialButton.Text ?? "");

            var labelRect = textToMeasure.GetBoundingRect(
                new CGSize(this.Frame.Width - 40, nfloat.MaxValue),
                NSStringDrawingOptions.UsesLineFragmentOrigin,
                new UIStringAttributes() { Font = this.Control.Font },
                new NSStringDrawingContext()
            );

            var textWidth = (float)labelRect.Size.Width;
            var buttonWidth = (float)this.Control.Frame.Width;

            var inset = ((buttonWidth - textWidth) / 2) - 28;
            this.Control.TitleEdgeInsets = new UIEdgeInsets(0, inset, 0, -40);
        }
    }
}