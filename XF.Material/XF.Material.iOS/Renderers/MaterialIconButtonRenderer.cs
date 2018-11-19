using CoreGraphics;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI;
using XF.Material.iOS.Delegates;
using XF.Material.iOS.Renderers;

[assembly: ExportRenderer(typeof(MaterialIconButton), typeof(MaterialIconButtonRenderer))]
namespace XF.Material.iOS.Renderers
{
    public class MaterialIconButtonRenderer : ViewRenderer<MaterialIconButton, UIButton>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<MaterialIconButton> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null)
            {
                this.SetNativeControl(new UIButton(UIButtonType.System));
            }

            if (e?.OldElement != null)
            {
                this.Control.TouchDown -= this.Control_Pressed;
                this.Control.TouchDragEnter -= this.Control_Pressed;
                
                foreach(var gestureRecognizer in this.Control.GestureRecognizers)
                {
                    this.Control.RemoveGestureRecognizer(gestureRecognizer);
                }
            }

            if (e?.NewElement != null)
            {
                if(this.Element.WidthRequest + this.Element.HeightRequest <= 0)
                {
                    this.Element.WidthRequest = 48;
                    this.Element.HeightRequest = 48;
                }

                this.SetupLayer(24f);
                this.SetupIcon(24f);
                this.Control.Layer.BorderColor = UIColor.Clear.CGColor;
                this.Control.Layer.BorderWidth = 0f;
                this.Control.TouchDown += this.Control_Pressed;
                this.Control.TouchDragEnter += this.Control_Pressed;
                this.Control.AddGestureRecognizer(new UITapGestureRecognizer() { Delegate = new MaterialRippleGestureRecognizerDelegate(this.Element.RippleColor.ToCGColor()) });
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if(e?.PropertyName == nameof(VisualElement.Width) && this.Element.Width > 0)
            {
                this.SetupLayer(this.Element.Width / 2);
                this.SetupIcon(this.Element.Width / 2);
            }
        }

        private void SetupLayer(double cornerRadius)
        {
            this.Control.Layer.CornerRadius = (nfloat)cornerRadius;
        }

        private void SetupIcon(double imageSize)
        {
            using (var image = new UIImage(this.Element.Source.File).ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate))
            {
                UIGraphics.BeginImageContextWithOptions(new CGSize(imageSize, imageSize), false, 0f);
                image.Draw(new CGRect(0, 0, imageSize, imageSize));

                using (var newImage = UIGraphics.GetImageFromCurrentImageContext())
                {
                    UIGraphics.EndImageContext();

                    this.Control.CurrentImage?.Dispose();
                    this.Control.SetImage(newImage, UIControlState.Normal);
                    this.Control.SetImage(newImage, UIControlState.Disabled);
                    this.Control.TintColor = this.Element.TintColor.ToUIColor();
                }
            }
        }

        private void Control_Pressed(object sender, EventArgs e)
        {
            this.OnClick();
        }

        protected virtual void OnClick()
        {
            this.Element.OnClick();
        }
    }
}