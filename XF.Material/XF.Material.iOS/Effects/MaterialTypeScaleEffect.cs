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
                label.AttributedText = new NSMutableAttributedString(label.Text ?? string.Empty, 
                    font: label.Font, 
                    foregroundColor: label.TextColor, 
                    kerning: (float)this.MaterialEffect.LetterSpacing);
            }

            else if (this.Control is UIButton button)
            {
                var attributedString = new NSMutableAttributedString(button.Title(UIControlState.Normal) ?? string.Empty, 
                    font: button.Font,
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