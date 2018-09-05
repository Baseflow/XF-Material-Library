using Foundation;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using XF.Material.iOS.Effects;

[assembly: ResolutionGroupName("Material")]
[assembly: ExportEffect(typeof(MaterialTypeScaleEffect), "TypeScaleEffect")]
namespace XF.Material.iOS.Effects
{
    internal class MaterialTypeScaleEffect : BaseMaterialEffect<XF.Material.Forms.Effects.MaterialTypeScaleEffect>
    {
        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if ((this.Element is Label || this.Element is Button) && args.PropertyName == "Text")
            {
                this.SetLetterSpacing();
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            this.SetLetterSpacing();
        }

        private void SetLetterSpacing()
        {
            if (this.Control is UILabel label)
            {
                var xfLabel = this.Element as Label;
                label.AttributedText = new NSMutableAttributedString(label.Text ?? string.Empty, 
                    font: UIFont.FromName(xfLabel.FontFamily, (float)xfLabel.FontSize), 
                    foregroundColor: label.TextColor, 
                    kerning: (float)this.MaterialEffect.LetterSpacing);
            }

            else if (this.Control is UIButton button)
            {
                var xfButton = this.Element as Button;
                var attributedString = new NSMutableAttributedString(button.Title(UIControlState.Normal) ?? string.Empty, 
                    font: UIFont.FromName(xfButton.FontFamily, (float)xfButton.FontSize),
                    foregroundColor: button.TitleColor(UIControlState.Normal),
                    kerning: (float)this.MaterialEffect.LetterSpacing);

                button.SetAttributedTitle(attributedString, UIControlState.Normal);
                button.SetAttributedTitle(attributedString, UIControlState.Focused);
                button.SetAttributedTitle(attributedString, UIControlState.Highlighted);
                button.SetAttributedTitle(attributedString, UIControlState.Selected);
            }
        }
    }
}