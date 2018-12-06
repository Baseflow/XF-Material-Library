using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using XF.Material.iOS.Delegates;
using static XF.Material.Forms.UI.MaterialButton;

namespace XF.Material.iOS.Renderers
{
    internal class MaterialCALayerAnimationHelper
    {
        private const int PRESSED_ELEVATION = 8;
        private const int RESTING_ELEVATION = 2;
        private readonly MaterialButtonType _materialButtonType;
        private readonly UIButton _uiButton;
        private CABasicAnimation _shadowOffsetPressed;
        private CABasicAnimation _shadowRadiusPressed;
        private CABasicAnimation _shadowOffsetResting;
        private CABasicAnimation _shadowRadiusResting;
        private CABasicAnimation _colorPressed;
        private CABasicAnimation _colorResting;
        private CGColor _normalColor;
        private CGColor _pressedColor;

        public MaterialCALayerAnimationHelper(UIButton uiButton,  MaterialButtonType materialButtonType)
        {
            _uiButton = uiButton;
            _materialButtonType = materialButtonType;
        }

        public void UpdateElevation(bool elevated)
        {
            _uiButton.Elevate( elevated ? RESTING_ELEVATION : 0);
        }

        public async Task ButtonPressed(CALayer animationLayer)
        {
            if (animationLayer == null)
            {
                return;
            }

            await UIView.AnimateAsync(0.150, () =>
            {
                _uiButton.Layer.AddAnimation(_shadowOffsetPressed, "shadowOffsetPressed");
                _uiButton.Layer.AddAnimation(_shadowRadiusPressed, "shadowRadiusPressed");
                animationLayer.AddAnimation(_colorPressed, "backgroundColorPressed");
            });
        }

        public async Task ButtonReleased(CALayer animationLayer)
        {
            if(animationLayer == null)
            {
                return;
            }

            //var colorAnim = animationLayer.AnimationForKey("backgroundColorPressed");
            //_colorResting.From = colorAnim.ValueForKeyPath(new NSString("backgroundColor"));

            await UIView.AnimateAsync(0.150, () =>
            {
                _uiButton.Layer.AddAnimation(_shadowOffsetResting, "shadowOffsetResting");
                _uiButton.Layer.AddAnimation(_shadowRadiusResting, "shadowRadiusResting");
                animationLayer.AddAnimation(_colorResting, "backgroundColorResting");
            });
        }

        private void CreateStateAnimations()
        {
            var pressedElevation = _materialButtonType == MaterialButtonType.Elevated ? PRESSED_ELEVATION : 0;
            var restingElevation = _materialButtonType == MaterialButtonType.Elevated ? RESTING_ELEVATION : 0;

            _shadowOffsetPressed = CABasicAnimation.FromKeyPath("shadowOffset");
            _shadowOffsetPressed.Duration = 0.150;
            _shadowOffsetPressed.FillMode = CAFillMode.Forwards;
            _shadowOffsetPressed.RemovedOnCompletion = false;
            _shadowOffsetPressed.To = NSObject.FromObject(new CGSize(0, pressedElevation));

            _shadowRadiusPressed = CABasicAnimation.FromKeyPath("shadowRadius");
            _shadowRadiusPressed.Duration = 0.150;
            _shadowRadiusPressed.FillMode = CAFillMode.Forwards;
            _shadowRadiusPressed.RemovedOnCompletion = false;
            _shadowRadiusPressed.To = NSNumber.FromFloat(pressedElevation);

            _shadowOffsetResting = CABasicAnimation.FromKeyPath("shadowOffset");
            _shadowOffsetResting.Duration = 0.150;
            _shadowOffsetResting.FillMode = CAFillMode.Forwards;
            _shadowOffsetResting.RemovedOnCompletion = false;
            _shadowOffsetResting.To = NSObject.FromObject(new CGSize(0, restingElevation));

            _shadowRadiusResting = CABasicAnimation.FromKeyPath("shadowRadius");
            _shadowRadiusResting.Duration = 0.150;
            _shadowRadiusResting.FillMode = CAFillMode.Forwards;
            _shadowRadiusResting.RemovedOnCompletion = false;
            _shadowRadiusResting.To = NSNumber.FromFloat(restingElevation);
            _colorPressed = CABasicAnimation.FromKeyPath("backgroundColor");
            _colorPressed.Duration = 0.3;
            _colorPressed.FillMode = CAFillMode.Forwards;
            _colorPressed.RemovedOnCompletion = false;
            _colorPressed.To = NSObject.FromObject(_pressedColor);

            _colorResting = CABasicAnimation.FromKeyPath("backgroundColor");
            _colorResting.Duration = 0.3;
            _colorResting.FillMode = CAFillMode.Forwards;
            _colorResting.RemovedOnCompletion = false;
            _colorResting.To = NSObject.FromObject(UIColor.Clear.CGColor);

            _uiButton.AddGestureRecognizer(new UITapGestureRecognizer() { Delegate = new MaterialRippleGestureRecognizerDelegate(_pressedColor) });
        }

        public void UpdateColors(CGColor normalColor, CGColor pressedColor)
        {
            _normalColor = normalColor;
            _pressedColor = pressedColor;

            this.CreateStateAnimations();
        }
    }
}