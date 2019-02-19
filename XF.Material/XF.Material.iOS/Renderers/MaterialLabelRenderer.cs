using CoreGraphics;
using Foundation;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI;
using XF.Material.iOS.Renderers;

[assembly: ExportRenderer(typeof(MaterialLabel), typeof(MaterialLabelRenderer))]

namespace XF.Material.iOS.Renderers
{
    public class MaterialLabelRenderer : LabelRenderer
    {
        public new MaterialLabel Element => base.Element as MaterialLabel;

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (this.Control.Frame.Size.Height == 0)
            {
                return;
            }

            var textSize = new CGSize(this.Control.Frame.Size.Width, nfloat.MaxValue);
            var rHeight = this.Control.SizeThatFits(textSize).Height;
            var charSize = this.Control.Font.LineHeight;
            var lines = Convert.ToInt32(rHeight / charSize);

            if (lines == 1)
            {
                this.OnLineHeightChanged(this.Control, 0);
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement == null) return;
            OnLetterSpacingChanged(this.Control, this.Element.LetterSpacing);
            this.OnLineHeightChanged(this.Control, this.Element.LineHeight);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e?.PropertyName)
            {
                case nameof(MaterialLabel.LetterSpacing):
                    OnLetterSpacingChanged(this.Control, this.Element.LetterSpacing);
                    break;
                case nameof(MaterialLabel.LineHeight):
                    this.OnLineHeightChanged(this.Control, this.Element.LineHeight);
                    break;
            }
        }

        private static void OnLetterSpacingChanged(UILabel uiLabel, double letterSpacing)
        {
            var attributedString = (NSMutableAttributedString) uiLabel?.AttributedText;

            if (attributedString == null) return;
            var nsKern = new NSString("NSKern");
            var nsObject = FromObject(letterSpacing);
            var nsRange = new NSRange(0, uiLabel.Text?.Length ?? 0);

            attributedString.AddAttribute(nsKern, nsObject, nsRange);
        }

        private void OnLineHeightChanged(UILabel uiLabel, double lineSpacing)
        {
            var attributedString = (NSMutableAttributedString) uiLabel?.AttributedText;

            if (attributedString == null) return;
            var attributeRange = new NSRange(0, uiLabel.Text.Length);
            var pAttribute = new NSMutableParagraphStyle();

            if (Math.Abs(lineSpacing) < float.MinValue)
            {
                pAttribute.LineSpacing = 0;
            }
            else
            {
                pAttribute.LineSpacing = (nfloat)((this.Element.FontSize * lineSpacing) - this.Element.FontSize);
            }

            attributedString.SetAttributes(new NSDictionary<NSString, NSObject>(UIStringAttributeKey.ParagraphStyle, pAttribute), attributeRange);
        }
    }
}