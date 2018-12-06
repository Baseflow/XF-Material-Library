using CoreGraphics;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI;
using XF.Material.iOS.Renderers;
using static XF.Material.Forms.UI.MaterialButton;

[assembly: ExportRenderer(typeof(MaterialButton), typeof(MaterialButtonRenderer))]

namespace XF.Material.iOS.Renderers
{
    public class MaterialButtonRenderer : ButtonRenderer
    {
        private MaterialCALayerHelper _helper;
        private MaterialButton _materialButton;
        private bool _withIcon;

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (this.Control.Frame.Width < 64)
            {
                _materialButton.WidthRequest = 64;
            }

            _helper.UpdateLayer();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e?.OldElement != null)
            {
                _helper.Clean();
            }

            if (e?.NewElement != null)
            {
                _materialButton = this.Element as MaterialButton;
                _withIcon = _materialButton.Image != null;
                _helper = new MaterialCALayerHelper(this.Element as IMaterialButton, this.Control);

                if (_materialButton.AllCaps)
                {
                    _materialButton.Text = _materialButton.Text?.ToUpper();
                }

                this.SetupIcon();
                this.UpdateButtonLayer();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e?.PropertyName == nameof(MaterialButton.Image))
            {
                this.SetupIcon();
                this.UpdateButtonLayer();
            }

            if (e?.PropertyName == nameof(MaterialButton.AllCaps))
            {
                _materialButton.Text = _materialButton.AllCaps ? _materialButton.Text.ToUpper() : _materialButton.Text.ToLower();
            }
        }

        private void SetupIcon()
        {
            if (_withIcon)
            {
                using (var image = new UIImage(this.Element.Image.File).ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate))
                {
                    UIGraphics.BeginImageContextWithOptions(new CGSize(18, 18), false, 0f);
                    image.Draw(new CGRect(0, 0, 18, 18));

                    using (var newImage = UIGraphics.GetImageFromCurrentImageContext())
                    {
                        UIGraphics.EndImageContext();

                        this.Control.SetImage(newImage, UIControlState.Normal);
                        this.Control.SetImage(newImage, UIControlState.Disabled);
                        this.Control.TitleEdgeInsets = new UIEdgeInsets(0f, 0f, 0f, 0f);
                        this.Control.ImageEdgeInsets = new UIEdgeInsets(0f, -6f, 0f, 0f);
                        this.Control.TintColor = _materialButton.TextColor.ToUIColor();
                    }
                }
            }
        }

        private void UpdateButtonLayer()
        {
            if (_materialButton.ButtonType != MaterialButtonType.Text && _withIcon)
            {
                this.Control.ContentEdgeInsets = new UIEdgeInsets(4f, 12f, 4f, 16f);
            }
            else if (_materialButton.ButtonType != MaterialButtonType.Text && !_withIcon)
            {
                this.Control.ContentEdgeInsets = new UIEdgeInsets(4f, 16f, 4f, 16f);
            }
            else if (_materialButton.ButtonType == MaterialButtonType.Text)
            {
                this.Control.ContentEdgeInsets = new UIEdgeInsets(4f, 12f, 4f, 12f);
            }
        }
    }
}