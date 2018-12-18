using CoreAnimation;
using CoreGraphics;
using UIKit;

namespace XF.Material.iOS.Delegates
{
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
            _fadeAnimation.From = FromObject(0.66f);
            _fadeAnimation.To = FromObject(0.32f);

            _rippleLayer = new CAShapeLayer();
            _rippleLayer.FillColor = rippleColor;
            _rippleLayer.MasksToBounds = true;
        }

        public override bool ShouldReceiveTouch(UIGestureRecognizer recognizer, UITouch touch)
        {
            var view = touch.View;
            var location = touch.LocationInView(touch.View);
            var startPath = UIBezierPath.FromArc(location, 8f, 0, 360f, true);
            var endPath = UIBezierPath.FromArc(location, view.Frame.Width - 12, 0, 360f, true);

            _rippleLayer.Frame = new CGRect(6, 6, view.Frame.Width - 12, view.Frame.Height - 12);
            _rippleLayer.CornerRadius = view.Layer.CornerRadius;
            _rippleAnimation.From = FromObject(startPath.CGPath);
            _rippleAnimation.To = FromObject(endPath.CGPath);
            view.Layer.InsertSublayer(_rippleLayer, 3);

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