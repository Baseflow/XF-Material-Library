using CoreGraphics;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI;
using XF.Material.iOS.Renderers;

[assembly: ExportRenderer(typeof(MaterialIconButton), typeof(MaterialIconButtonRenderer))]
namespace XF.Material.iOS.Renderers
{
    public class MaterialIconButtonRenderer : ViewRenderer<MaterialIconButton, UIButton>
    {
        private MaterialCALayerHelper _helper;

        protected override void OnElementChanged(ElementChangedEventArgs<MaterialIconButton> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null)
            {
                this.SetNativeControl(new UIButton(UIButtonType.System));
                this.Control.Frame = new CGRect(0, 0, this.Element.WidthRequest, this.Element.HeightRequest);
            }

            if (e?.OldElement != null)
            {
                _helper.Clean();
                this.Control.TouchDown -= this.Control_Pressed;
                this.Control.TouchDragEnter -= this.Control_Pressed;
            }

            if (e?.NewElement != null)
            {
                _helper = new MaterialCALayerHelper(this.Element, this.Control);

                this.Control.TouchDown += this.Control_Pressed;
                this.Control.TouchDragEnter += this.Control_Pressed;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if(e?.PropertyName == nameof(VisualElement.Width) && this.Element.Width > 0)
            {
                this.SetupIcon(this.Element.Width / 2);
            }
        }

        private void SetupIcon(double imageSize)
        {
            using (var image = new UIImage(this.Element.Source.File).ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate))
            {
                UIGraphics.BeginImageContextWithOptions(image.Size, false, 0f);
                image.Draw(new CGRect(0, 0, imageSize, imageSize));

                using (var newImage = UIGraphics.GetImageFromCurrentImageContext())
                {
                    UIGraphics.EndImageContext();

                    this.Control.SetImage(newImage, UIControlState.Normal);
                    this.Control.SetImage(newImage, UIControlState.Disabled);
                    this.Control.SetImage(newImage, UIControlState.Highlighted);
                    this.Control.SetImage(newImage, UIControlState.Selected);
                    this.Control.TintColor = this.Element.TintColor.ToUIColor();
                }
            }

            _helper.UpdateLayer();
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