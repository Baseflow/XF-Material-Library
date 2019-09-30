using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI;
using XF.Material.iOS.GestureRecognizers;
using XF.Material.iOS.Renderers;

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

            if (e?.NewElement == null)
            {
                return;
            }

            _materialCard = Element as MaterialCard;
            if (_materialCard != null)
            {
                this.Elevate(_materialCard.Elevation);
            }

            SetupColors();
            SetIsClickable();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e?.PropertyName == nameof(MaterialCard.Elevation) || e?.PropertyName == nameof(VisualElement.BackgroundColor))
            {
                this.Elevate(_materialCard.Elevation);
            }

            switch (e?.PropertyName)
            {
                // For some reason the Elevation will get messed up when the background
                // color is modified. So this fixes it.
                //
                case nameof(MaterialCard.BackgroundColor):
                    SetupColors();
                    SetIsClickable();
                    this.Elevate(_materialCard.Elevation);
                    break;
                case nameof(MaterialCard.IsClickable):
                    SetIsClickable();
                    break;
            }
        }

        private void SetupColors()
        {
            _rippleColor = BackgroundColor.IsColorDark() ? Color.FromHex("#52FFFFFF").ToUIColor() : Color.FromHex("#52000000").ToUIColor();
        }

        private void SetIsClickable()
        {
            var clickable = _materialCard.IsClickable;
            if (clickable)
            {
                if (_rippleGestureRecognizerDelegate == null)
                {
                    _rippleGestureRecognizerDelegate = new MaterialRippleGestureRecognizer(_rippleColor.CGColor, this);
                }
                else
                {
                    RemoveGestureRecognizer(_rippleGestureRecognizerDelegate);
                }

                AddGestureRecognizer(_rippleGestureRecognizerDelegate);
            }
            else if (_rippleGestureRecognizerDelegate != null)
            {
                RemoveGestureRecognizer(_rippleGestureRecognizerDelegate);
            }
        }
    }
}