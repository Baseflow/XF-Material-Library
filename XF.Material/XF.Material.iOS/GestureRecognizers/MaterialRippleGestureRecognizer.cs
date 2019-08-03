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
        private readonly CABasicAnimation _backgroundFadeInHoldAnimation;
        private readonly CABasicAnimation _backgroundFadeOutAfterHoldAnimation;
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
            _backgroundFadeInAnimation.BeginTime = 0;
            _backgroundFadeInAnimation.Duration = 0.3f;
            _backgroundFadeInAnimation.FillMode = CAFillMode.Forwards;
            _backgroundFadeInAnimation.RemovedOnCompletion = false;
            _backgroundFadeInAnimation.From = FromObject(0.0f);
            _backgroundFadeInAnimation.To = FromObject(0.20f);

            _backgroundFadeInHoldAnimation = CABasicAnimation.FromKeyPath("opacity");
            _backgroundFadeInHoldAnimation.BeginTime = 0.3f;
            _backgroundFadeInHoldAnimation.Duration = 1.2f;
            _backgroundFadeInHoldAnimation.FillMode = CAFillMode.Forwards;
            _backgroundFadeInHoldAnimation.RemovedOnCompletion = false;
            _backgroundFadeInHoldAnimation.From = FromObject(0.20f);
            _backgroundFadeInHoldAnimation.To = FromObject(0.20f);

            _backgroundFadeOutAfterHoldAnimation = CABasicAnimation.FromKeyPath("opacity");
            _backgroundFadeOutAfterHoldAnimation.BeginTime = 1.5f;
            _backgroundFadeOutAfterHoldAnimation.Duration = 0.2f;
            _backgroundFadeOutAfterHoldAnimation.FillMode = CAFillMode.Forwards;
            _backgroundFadeOutAfterHoldAnimation.RemovedOnCompletion = false;
            _backgroundFadeOutAfterHoldAnimation.From = FromObject(0.20f);
            _backgroundFadeOutAfterHoldAnimation.To = FromObject(0.0f);

            _backgroundFadeOutAnimation = CABasicAnimation.FromKeyPath("opacity");
            _backgroundFadeOutAnimation.Duration = 0.3;
            _backgroundFadeOutAnimation.FillMode = CAFillMode.Forwards;
            _backgroundFadeOutAnimation.RemovedOnCompletion = true;
            _backgroundFadeOutAnimation.From = FromObject(0.20f);
            _backgroundFadeOutAnimation.To = FromObject(0.0f);

            _rippleLayer = new CAShapeLayer {FillColor = rippleColor, MasksToBounds = true};

            _backgroundLayer = new CALayer {BackgroundColor = rippleColor, MasksToBounds = true, Opacity = 0};

            _isStarted = false;
            _touchView = view;
            this.Delegate = this;
        }

        [Export("gestureRecognizer:shouldReceiveTouch:")]
        public bool ShouldReceiveTouch(UIGestureRecognizer recognizer, UITouch touch)
        {
            return !(touch.View is UIButton);
        }


        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            this.AnimateStart(touches.AnyObject as UITouch);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            this.AnimateComplete();
        }


        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            this.AnimateComplete();

        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            this.AnimateComplete();
        }



        private void AnimateStart(UITouch touch)
        {
            this.AnimateBackgroundFadeIn();
            this.AnimateRipple(touch);

            _isStarted = true;
        }

        

        private void AnimateComplete()
        {
            if (_isStarted)
            {
                this.AnimateBackgroundFadeOut();
            }
        }


        private void AnimateBackgroundFadeIn()
        {
            var view = _touchView;

            SetupAnimationLayer(_backgroundLayer, view, 3);


            _backgroundLayer.RemoveAllAnimations();

            // Apparently the UITapGestureRecognizer is disabled after
            // 1.5 seconds and none of the Touches* events are triggered
            // for us to fade the backgroundLayer, so we have to chain
            // animations to ensure that we automatically fade out
            // the background layer after 1.5 seconds.
            //
            CAAnimationGroup group = new CAAnimationGroup();
            group.Duration = 1.7;
            group.RepeatCount = 1;
            group.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.Linear);
            group.Animations = new CAAnimation[] { 
                _backgroundFadeInAnimation, 
                _backgroundFadeInHoldAnimation, 
                _backgroundFadeOutAfterHoldAnimation };

            _backgroundLayer.AddAnimation(group, "backgroundFadeInAnimation");
        }

        private void AnimateBackgroundFadeOut()
        {
            var view = _touchView;

            SetupAnimationLayer(_backgroundLayer, view, 3);

            _backgroundLayer.RemoveAllAnimations();
            _backgroundLayer.AddAnimation(_backgroundFadeOutAnimation, "backgroundFadeOutAnimation");
        }

        private void AnimateRipple(UITouch touch)
        {
            var view = _touchView;

            var location = touch.LocationInView(view);
            var startPath = UIBezierPath.FromArc(location, 8f, 0, 360f, true);
            var endPath = UIBezierPath.FromArc(location, view.Frame.Width - 12, 0, 360f, true);

            SetupAnimationLayer(_rippleLayer, view, 4);

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
        /// <param name="indexToInsertLayer"></param>
        private static void SetupAnimationLayer(CALayer layer, UIView view, int indexToInsertLayer)
        {
            layer.Frame = view is MaterialCardRenderer ? new CGRect(0, 0, view.Frame.Width, view.Frame.Height) : 
                new CGRect(6, 6, view.Frame.Width - 12, view.Frame.Height - 12);

            layer.CornerRadius = view.Layer.CornerRadius;
            view.Layer.InsertSublayer(layer, indexToInsertLayer);

        }
    }
}
