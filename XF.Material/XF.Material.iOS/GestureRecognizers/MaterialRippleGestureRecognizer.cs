using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using XF.Material.iOS.Renderers;

namespace XF.Material.iOS.GestureRecognizers
{
    public class MaterialRippleGestureRecognizer : UITapGestureRecognizer, IUIGestureRecognizerDelegate
    {
        private readonly CABasicAnimation _rippleAnimation;
        private readonly CABasicAnimation _fadeAnimation;
        private readonly CABasicAnimation _backgroundFadeInAnimation;
        private readonly CABasicAnimation _backgroundFadeOutAnimation;
        private readonly CALayer _backgroundLayer;
        private readonly CAShapeLayer _rippleLayer;
        private readonly UIView _touchView;
        private bool _isStarted;

        public MaterialRippleGestureRecognizer(CGColor rippleColor, UIView view)
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
            _fadeAnimation.To = FromObject(0.0f);

            _backgroundFadeInAnimation = CABasicAnimation.FromKeyPath("opacity");
            _backgroundFadeInAnimation.Duration = 0.3;
            _backgroundFadeInAnimation.FillMode = CAFillMode.Forwards;
            _backgroundFadeInAnimation.RemovedOnCompletion = false;
            _backgroundFadeInAnimation.From = FromObject(0.0f);
            _backgroundFadeInAnimation.To = FromObject(0.20f);

            _backgroundFadeOutAnimation = CABasicAnimation.FromKeyPath("opacity");
            _backgroundFadeOutAnimation.Duration = 0.3;
            _backgroundFadeOutAnimation.FillMode = CAFillMode.Forwards;
            _backgroundFadeOutAnimation.RemovedOnCompletion = true;
            _backgroundFadeOutAnimation.From = FromObject(0.20f);
            _backgroundFadeOutAnimation.To = FromObject(0.0f);

            _rippleLayer = new CAShapeLayer();
            _rippleLayer.FillColor = rippleColor;
            _rippleLayer.MasksToBounds = true;

            _backgroundLayer = new CALayer();
            _backgroundLayer.BackgroundColor = rippleColor;
            _backgroundLayer.MasksToBounds = true;
            _backgroundLayer.Opacity = 0;

            _isStarted = false;
            _touchView = view;
            this.Delegate = this;
        }

        [Export("gestureRecognizer:shouldReceiveTouch:")]
        public new bool ShouldReceiveTouch(UIGestureRecognizer recognizer, UITouch touch)
        {
            if (touch.View is UIButton)
                return false;

            return true;
        }


        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            this.AnimateStart(touches.AnyObject as UITouch);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            this.AnimateComplete(touches.AnyObject as UITouch);
        }


        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            this.AnimateComplete(touches.AnyObject as UITouch);

        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            this.AnimateComplete(touches.AnyObject as UITouch);
        }


        private void AnimateStart(UITouch touch)
        {
            this.AnimateBackgroundFadeIn(touch);
            this.AnimateRipple(touch);

            _isStarted = true;
        }


        private void AnimateComplete(UITouch touch)
        {
            if (_isStarted)
            {
                this.AnimateBackgroundFadeOut(touch);
            }
        }


        private void AnimateBackgroundFadeIn(UITouch touch)
        {
            var view = _touchView;

            this.SetupAnimationLayer(_backgroundLayer, view, 3);

            _backgroundLayer.RemoveAllAnimations();
            UIView.Animate(0.8, () =>
            {
                _backgroundLayer.AddAnimation(_backgroundFadeInAnimation, "backgroundFadeInAnimation");
            });
        }

        private void AnimateBackgroundFadeOut(UITouch touch)
        {
            var view = _touchView;

            this.SetupAnimationLayer(_backgroundLayer, view, 3);

            _backgroundLayer.RemoveAllAnimations();
            UIView.Animate(0.8, () =>
            {
                _backgroundLayer.AddAnimation(_backgroundFadeOutAnimation, "backgroundFadeOutAnimation");
            });
        }

        private void AnimateRipple(UITouch touch)
        {
            var view = _touchView;

            var location = touch.LocationInView(view);
            var startPath = UIBezierPath.FromArc(location, 8f, 0, 360f, true);
            var endPath = UIBezierPath.FromArc(location, view.Frame.Width - 12, 0, 360f, true);

            this.SetupAnimationLayer(_rippleLayer, view, 4);

            _rippleAnimation.From = FromObject(startPath.CGPath);
            _rippleAnimation.To = FromObject(endPath.CGPath);
            UIView.Animate(0.3, () =>
            {
                _rippleLayer.AddAnimation(_rippleAnimation, "rippleAnimation");
                _rippleLayer.AddAnimation(_fadeAnimation, "rippleFadeAnim");
            });
        }


        /// <summary>
        /// Sets the <paramref name="layer"/>'s bounding frame based on the 
        /// View this gesture recognizer is attached to.
        /// </summary>
        /// <param name="layer">Layer.</param>
        /// <param name="view">View.</param>
        private void SetupAnimationLayer(CALayer layer, UIView view, int indexToInsertLayer)
        {
            if (view is MaterialCardRenderer)
                layer.Frame = new CGRect(0, 0, view.Frame.Width, view.Frame.Height);
            else
                layer.Frame = new CGRect(6, 6, view.Frame.Width - 12, view.Frame.Height - 12);

            layer.CornerRadius = view.Layer.CornerRadius;
            view.Layer.InsertSublayer(layer, indexToInsertLayer);

        }
    }
}
