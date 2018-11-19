using CoreAnimation;
using CoreGraphics;
using UIKit;

namespace XF.Material.iOS.Delegates
{
    /// <summary>
    /// A <see cref="UIGestureRecognizerDelegate"/> that when attached to a <see cref="UIGestureRecognizer"/>, generates a ripple-effect when interacted with.
    /// </summary>
    public class MaterialRippleGestureRecognizerDelegate : UIGestureRecognizerDelegate
    {
        private readonly CABasicAnimation _rippleAnimation;
        private readonly CABasicAnimation _fadeAnimation;
        private readonly CAShapeLayer _rippleLayer;

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialRippleGestureRecognizerDelegate"/>.
        /// </summary>
        /// <param name="rippleColor">The color of the ripple.</param>
        public MaterialRippleGestureRecognizerDelegate(CGColor rippleColor)
        {
            _rippleAnimation = CABasicAnimation.FromKeyPath("path");
            _rippleAnimation.Duration = 0.3;
            _rippleAnimation.FillMode = CAFillMode.Forwards;
            _rippleAnimation.RemovedOnCompletion = true;

            _fadeAnimation = CABasicAnimation.FromKeyPath("opacity");
            _fadeAnimation.Duration = 0.3;
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
            var view = touch.View;
            var location = touch.LocationInView(touch.View);
            var startPath = UIBezierPath.FromArc(location, 8f, 0, 360f, true);
            var endPath = UIBezierPath.FromArc(location, 64f, 0, 360f, true);

            _rippleLayer.Frame = view.Frame;
            _rippleLayer.CornerRadius = view.Layer.CornerRadius;
            _rippleAnimation.From = FromObject(startPath.CGPath);
            _rippleAnimation.To = FromObject(endPath.CGPath);
            view.Layer.InsertSublayer(_rippleLayer, 0);

            this.AnimateRipple();

            return false;
        }

        private void AnimateRipple()
        {
            UIView.Animate(0.3, () =>
            {
                _rippleLayer.AddAnimation(_rippleAnimation, "rippleAnimation");
                _rippleLayer.AddAnimation(_fadeAnimation, "rippleFadeAnim");
            });
        }
    }
}