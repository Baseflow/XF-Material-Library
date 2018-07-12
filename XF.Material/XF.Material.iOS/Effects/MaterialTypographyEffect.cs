using Xamarin.Forms;
using Xamarin.Forms.Internals;
using XF.Material.iOS.Effects;
using UIKit;
using Foundation;

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
                var text = label.Text;
                var attributedString = new NSMutableAttributedString(text);
                var nsKern = new NSString("NSKern");
                var spacing = NSObject.FromObject(this.MaterialEffect.LetterSpacing);
                var range = new NSRange(0, text.Length);

                attributedString.AddAttribute(nsKern, spacing, range);
                label.AttributedText = attributedString;

            }

            else if(this.Control is UIButton button)
            {
                var text = button.Title(UIControlState.Normal);
                var attributedString = new NSMutableAttributedString(text);
                var nsKern = new NSString("NSKern");
                var spacing = NSObject.FromObject(this.MaterialEffect.LetterSpacing);
                var range = new NSRange(0, text.Length);

                attributedString.AddAttribute(nsKern, spacing, range);
                button.SetAttributedTitle(attributedString, UIControlState.Normal);
                button.SetAttributedTitle(attributedString, UIControlState.Focused);
                button.SetAttributedTitle(attributedString, UIControlState.Highlighted);
                button.SetAttributedTitle(attributedString, UIControlState.Selected);
            }
        }
    }
}