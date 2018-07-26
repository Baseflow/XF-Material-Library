using Foundation;
using UIKit;
using Xamarin.Forms;
using XF.Material.iOS.Effects;

[assembly: ResolutionGroupName("Material")]
[assembly: ExportEffect(typeof(MaterialTypeScaleEffect), "TypeScaleEffect")]
namespace XF.Material.iOS.Effects
{
    public class MaterialTypeScaleEffect : BaseMaterialEffect<XF.Material.Effects.MaterialTypeScaleEffect>
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
                    kerning: (float)this.MaterialEffect.LetterSpacing);

                button.SetAttributedTitle(attributedString, UIControlState.Normal);
                button.SetAttributedTitle(attributedString, UIControlState.Focused);
                button.SetAttributedTitle(attributedString, UIControlState.Highlighted);
                button.SetAttributedTitle(attributedString, UIControlState.Selected);
            }
        }
    }
}