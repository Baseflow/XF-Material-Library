using System.ComponentModel;
using UIKit;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Compatibility;
using XF.Material.Maui.UI;
using XF.Material.iOS.GestureRecognizers;
using XF.Material.iOS.Renderers;
using Microsoft.Maui.Controls.Platform;

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
            _rippleColor = BackgroundColor.IsColorDark() ? Color.FromArgb("#52FFFFFF").ToUIColor() : Color.FromArgb("#52000000").ToUIColor();
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
