using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI;
using XF.Material.iOS.Renderers;
using System.ComponentModel;
using XF.Material.iOS.Delegates;
using UIKit;
using XF.Material.iOS.GestureRecognizers;
using System;

[assembly: ExportRenderer(typeof(MaterialCard), typeof(MaterialCardRenderer))]
namespace XF.Material.iOS.Renderers
{
    public class MaterialCardRenderer : FrameRenderer
    {
        private MaterialCard _materialCard;

        private UIColor _rippleColor;
        private UITapGestureRecognizer _rippleGestureRecognizerDelegate = null;

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _materialCard = this.Element as MaterialCard;
                this.Elevate(_materialCard.Elevation);

                this.SetupColors();
                this.SetIsClickable();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e?.PropertyName == nameof(MaterialCard.Elevation) || e?.PropertyName == nameof(VisualElement.BackgroundColor))
            {
                this.Elevate(_materialCard.Elevation);
            }

            // For some reason the Elevation will get messed up when the background
            // color is modified. So this fixes it.
            //
            if (e?.PropertyName == nameof(MaterialCard.BackgroundColor))
            {
                this.SetupColors();
                this.SetIsClickable();
                this.Elevate(_materialCard.Elevation);
            }

            if (e?.PropertyName == nameof(MaterialCard.IsClickable))
            {
                this.SetIsClickable();
            }
        }

        private void SetupColors()
        {
            _rippleColor = this.BackgroundColor.IsColorDark() ? Color.FromHex("#52FFFFFF").ToUIColor() : Color.FromHex("#52000000").ToUIColor();
        }

        protected void SetIsClickable()
        {
            bool clickable = _materialCard.IsClickable;
            if (clickable)
            {
                if (_rippleGestureRecognizerDelegate == null)
                {
                    _rippleGestureRecognizerDelegate = new MaterialRippleGestureRecognizer(_rippleColor.CGColor, this);
                }
                else
                    this.RemoveGestureRecognizer(_rippleGestureRecognizerDelegate);

                this.AddGestureRecognizer(_rippleGestureRecognizerDelegate);
            }
            else
                if (_rippleGestureRecognizerDelegate != null)
                this.RemoveGestureRecognizer(_rippleGestureRecognizerDelegate);
        }
    }
}