using System;
using System.ComponentModel;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.iOS.Renderers;
using XF.Material.Forms.Views;
using static XF.Material.Forms.Views.MaterialButton;

[assembly: ExportRenderer(typeof(MaterialButton), typeof(MaterialButtonRenderer))]
namespace XF.Material.iOS.Renderers
{
    public class MaterialButtonRenderer : ButtonRenderer
    {
        private const int PRESSED_ELEVATION = 8;
        private const int RESTING_ELEVATION = 2;
        private const float ENABLED_ALPHA = 1f;
        private const float DISABLED_ALPHA = 0.38f;
        private bool _withIcon;
        private MaterialButton _materialButton;
        private CABasicAnimation _shadowOffsetPressed;
        private CABasicAnimation _shadowRadiusPressed;
        private CABasicAnimation _shadowOffsetResting;
        private CABasicAnimation _shadowRadiusResting;
        private CABasicAnimation _colorPressed;
        private CABasicAnimation _colorResting;
        private UIColor _restingBackgroundColor;
        private UIColor _pressedBackgroundColor;
        private UIColor _rippleColor;

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (this.Control.Frame.Width < 64)
            {
                _materialButton.WidthRequest = 64;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

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
                _materialButton.HeightRequest -= 12;
                _materialButton.Margin = new Thickness(_materialButton.Margin.Left + 6, _materialButton.Margin.Top + 6, _materialButton.Margin.Right + 6, _materialButton.Margin.Bottom + 6);
                _withIcon = _materialButton.Image != null;

                if (_materialButton.AllCaps)
                {
                    _materialButton.Text = _materialButton.Text.ToUpper();
                }

                this.SetupIcon();
                this.SetupColors();
                this.UpdateButtonLayer();
                this.CreateStateAnimations();
                this.UpdateState();
                this.Control.TouchDown += this.Control_Pressed;
                this.Control.TouchDragEnter += this.Control_Pressed;
                this.Control.TouchUpInside += this.Control_Released;
                this.Control.TouchCancel += this.Control_Released;
                this.Control.TouchDragExit += this.Control_Released;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if(e.PropertyName == nameof(Button.IsEnabled) && _materialButton.ButtonType == MaterialButtonType.Elevated)
            {
                this.UpdateState();
            }

            if (e?.PropertyName == MaterialButton.MaterialButtonColorChanged || e?.PropertyName == nameof(MaterialButton.ButtonType))
            {
                this.SetupColors();
                this.UpdateButtonLayer();
            }

            if (e?.PropertyName == nameof(MaterialButton.Image))
            {
                this.SetupIcon();
                this.UpdateButtonLayer();
            }
        }

        private void UpdateState()
        {
            if(_materialButton.IsEnabled)
            {
                this.Control.Alpha = ENABLED_ALPHA;

                if(_materialButton.ButtonType == MaterialButtonType.Elevated)
                {
                    this.Control.Elevate(RESTING_ELEVATION);
                }
            }

            else
            {
                this.Control.Alpha = DISABLED_ALPHA;

                if (_materialButton.ButtonType == MaterialButtonType.Elevated)
                {
                    this.Control.Elevate(0);
                }
            }
        }

        private void SetupIcon()
        {
            if(_withIcon)
            {
                var image = UIImage.FromFile(_materialButton.Image.File);
                UIGraphics.BeginImageContextWithOptions(new CGSize(18, 18), false, 0f);
                image.Draw(new CGRect(0, 0, 18, 18));

                var newImage = UIGraphics.GetImageFromCurrentImageContext();
                UIGraphics.EndImageContext();

                this.Control.SetImage(newImage, UIControlState.Normal);
                this.Control.SetImage(newImage, UIControlState.Disabled);
                this.Control.TitleEdgeInsets = new UIEdgeInsets(0f, 0f, 0f, 0f);
                this.Control.ImageEdgeInsets = new UIEdgeInsets(0f, -5f, 0f, 0f);
                this.Control.TintColor = _materialButton.TextColor.ToUIColor();
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
                    this.Control.Layer.AddAnimation(_colorPressed, "backgroundColorPressed");
                });
            }

            else
            {
                await AnimateAsync(0.500, () => this.Control.Layer.AddAnimation(_colorPressed, "backgroundColorPressed"));
            }
        }

        private async void Control_Released(object sender, EventArgs e)
        {
            var colorAnim = this.Control.Layer.AnimationForKey("backgroundColorPressed");
            _colorResting.From = colorAnim.ValueForKeyPath(new NSString("backgroundColor"));

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
                    this.Control.Layer.AddAnimation(_colorResting, "backgroundColorResting");
                });
            }

            else
            {
                await AnimateAsync(0.500, () => this.Control.Layer.AddAnimation(_colorResting, "backgroundColorResting"));
            }
        }

        private void CreateContainedButtonLayer(bool elevated)
        {
            this.Control.BackgroundColor = _materialButton.BackgroundColor.ToUIColor();

            if (elevated)
            {
                this.Control.Elevate(RESTING_ELEVATION);
            }
        }

        private void CreateStateAnimations()
        {
            if (_materialButton.ButtonType == MaterialButtonType.Elevated)
            {
                _shadowOffsetPressed = CABasicAnimation.FromKeyPath("shadowOffset");
                _shadowOffsetPressed.Duration = 0.150;
                _shadowOffsetPressed.FillMode = CAFillMode.Forwards;
                _shadowOffsetPressed.RemovedOnCompletion = false;
                _shadowOffsetPressed.To = FromObject(new CGSize(0, PRESSED_ELEVATION));

                _shadowRadiusPressed = CABasicAnimation.FromKeyPath("shadowRadius");
                _shadowRadiusPressed.Duration = 0.150;
                _shadowRadiusPressed.FillMode = CAFillMode.Forwards;
                _shadowRadiusPressed.RemovedOnCompletion = false;
                _shadowRadiusPressed.To = NSNumber.FromFloat(PRESSED_ELEVATION);

                _shadowOffsetResting = CABasicAnimation.FromKeyPath("shadowOffset");
                _shadowOffsetResting.Duration = 0.150;
                _shadowOffsetResting.FillMode = CAFillMode.Forwards;
                _shadowOffsetResting.RemovedOnCompletion = false;
                _shadowOffsetResting.To = FromObject(new CGSize(0, RESTING_ELEVATION));

                _shadowRadiusResting = CABasicAnimation.FromKeyPath("shadowRadius");
                _shadowRadiusResting.Duration = 0.150;
                _shadowRadiusResting.FillMode = CAFillMode.Forwards;
                _shadowRadiusResting.RemovedOnCompletion = false;
                _shadowRadiusResting.To = NSNumber.FromFloat(RESTING_ELEVATION);
            }

            _colorPressed = CABasicAnimation.FromKeyPath("backgroundColor");
            _colorPressed.Duration = 0.5;
            _colorPressed.FillMode = CAFillMode.Forwards;
            _colorPressed.RemovedOnCompletion = false;
            _colorPressed.To = FromObject(_pressedBackgroundColor.CGColor);

            _colorResting = CABasicAnimation.FromKeyPath("backgroundColor");
            _colorResting.Duration = 0.5;
            _colorResting.FillMode = CAFillMode.Forwards;
            _colorResting.RemovedOnCompletion = false;
            _colorResting.To = FromObject(_restingBackgroundColor.CGColor);

            this.Control.AddGestureRecognizer(new UITapGestureRecognizer() { Delegate = new MaterialRippleGestureRecognizerDelegate(_rippleColor.CGColor) });
        }

        private void SetupColors()
        {
            if (_materialButton.ButtonType == MaterialButtonType.Elevated || _materialButton.ButtonType == MaterialButtonType.Flat)
            {
                _restingBackgroundColor = _materialButton.BackgroundColor.ToUIColor();
                _pressedBackgroundColor = _materialButton.BackgroundColor.ToUIColor().IsColorDark() ? _materialButton.BackgroundColor.ToUIColor().LightenColor() : _materialButton.BackgroundColor.ToUIColor().DarkenColor();
                _rippleColor = _materialButton.BackgroundColor.ToUIColor().IsColorDark() ? Color.FromHex("#52FFFFFF").ToUIColor() : Color.FromHex("#52000000").ToUIColor();
            }

            else
            {
                _restingBackgroundColor = UIColor.Clear;
                _rippleColor = _pressedBackgroundColor = Color.FromHex("#51000000").ToUIColor();
            }
        }

        private void UpdateButtonLayer()
        {
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

            if (_materialButton.ButtonType != MaterialButtonType.Text && _withIcon)
            {
                this.Control.ContentEdgeInsets = new UIEdgeInsets(4f, 12f, 4f, 16f);
            }

            else if (_materialButton.ButtonType != MaterialButtonType.Text && !_withIcon)
            {
                this.Control.ContentEdgeInsets = new UIEdgeInsets(4f, 16f, 4f, 16f);
            }

            else if (_materialButton.ButtonType == MaterialButtonType.Text)
            {
                this.Control.ContentEdgeInsets = new UIEdgeInsets(4f, 8f, 4f, 8f);
            }
        }

        private void CreateOutlinedButtonLayer()
        {
            this.Control.Layer.BorderColor = _materialButton.BorderColor.ToCGColor();
            this.Control.Layer.BorderWidth = (nfloat)_materialButton.BorderWidth;
        }

        private void CreateTextButtonLayer()
        {
            this.Control.Layer.BorderColor = UIColor.Clear.CGColor;
            this.Control.Layer.BorderWidth = 0f;
            this.Control.Layer.CornerRadius = _materialButton.CornerRadius;
        }

        private class MaterialRippleGestureRecognizerDelegate : UIGestureRecognizerDelegate
        {
            private readonly CABasicAnimation _rippleAnimation;
            private readonly CABasicAnimation _fadeAnimation;
            private readonly CAShapeLayer _rippleLayer;

            public MaterialRippleGestureRecognizerDelegate(CGColor rippleColor)
            {
                _rippleAnimation = CABasicAnimation.FromKeyPath("path");
                _rippleAnimation.Duration = 0.5;
                _rippleAnimation.FillMode = CAFillMode.Forwards;
                _rippleAnimation.RemovedOnCompletion = true;

                _fadeAnimation = CABasicAnimation.FromKeyPath("opacity");
                _fadeAnimation.Duration = 0.5;
                _fadeAnimation.FillMode = CAFillMode.Forwards;
                _fadeAnimation.RemovedOnCompletion = true;
                _fadeAnimation.From = FromObject(0.8f);
                _fadeAnimation.To = FromObject(0f);

                _rippleLayer = new CAShapeLayer();
                _rippleLayer.FillColor = rippleColor;
                _rippleLayer.MasksToBounds = true;
            }

            public override bool ShouldReceiveTouch(UIGestureRecognizer recognizer, UITouch touch)
            {
                if (touch.View is UIButton button)
                {
                    var location = touch.LocationInView(touch.View);
                    var startPath = UIBezierPath.FromArc(location, 4f, 0, 360f, true);
                    var endPath = UIBezierPath.FromArc(location, button.Frame.Width, 0, 360f, true);

                    _rippleLayer.Frame = button.Frame;
                    _rippleLayer.CornerRadius = button.Layer.CornerRadius;
                    _rippleAnimation.From = FromObject(startPath.CGPath);
                    _rippleAnimation.To = FromObject(endPath.CGPath);
                    button.Layer.InsertSublayer(_rippleLayer, 0);
                    this.AnimateRipple();
                }

                return !(touch.View is UIButton);
            }

            private void AnimateRipple()
            {
                Animate(0.5, () =>
                {
                    _rippleLayer.AddAnimation(_rippleAnimation, "rippleAnimation");
                    _rippleLayer.AddAnimation(_fadeAnimation, "rippleFadeAnim");
                }, () =>
                {
                    System.Diagnostics.Debug.WriteLine("Animation Completed");
                });
            }
        }
    }
}