using Foundation;
using UIKit;
using Xamarin.Forms;
using XF.Material.iOS.Effects;

[assembly: ResolutionGroupName("Material")]
[assembly: ExportEffect(typeof(MaterialTypographyEffect), "TypographyEffect")]
namespace XF.Material.iOS.Effects
{
    public class MaterialTypographyEffect : BaseMaterialEffect<XF.Material.Effects.MaterialTypographyEffect>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            if (this.Control is UILabel label)
            {
                label.AttributedText = new NSMutableAttributedString(label.Text, font: label.Font, foregroundColor: label.TextColor, kerning: (float)this.MaterialEffect.LetterSpacing);
            }

            else if (this.Control is UIButton button)
            {
                var attributedString = new NSMutableAttributedString(button.Title(UIControlState.Normal),
                    foregroundColor: button.TitleColor(UIControlState.Normal),
                    kerning: (float)this.MaterialEffect.LetterSpacing, font: button.Font);

                button.SetAttributedTitle(attributedString, UIControlState.Normal);
                button.SetAttributedTitle(attributedString, UIControlState.Focused);
                button.SetAttributedTitle(attributedString, UIControlState.Highlighted);
                button.SetAttributedTitle(attributedString, UIControlState.Selected);
            }
        }
    }
}