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

        public NSMutableAttributedString AttributedString => this.Control?.AttributedText as NSMutableAttributedString;

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            this.CheckIfSingleLine();
            this.EnsureLineBreakMode();
            this.UpdateLetterSpacing(this.Control, this.Element.LetterSpacing);
        }

        private void CheckIfSingleLine()
        {
            if (this.Control == null || this.Control.Frame.Size.Height == 0)
            {
                return;
            }

            var textSize = new CGSize(this.Control.Frame.Size.Width, nfloat.MaxValue);
            var rHeight = this.Control.SizeThatFits(textSize).Height;
            var charSize = this.Control.Font.LineHeight;
            var lines = Convert.ToInt32(rHeight / charSize);

            if (lines == 1)
            {
                this.Element.LineHeight = 1;
            }
        }

        private void EnsureLineBreakMode()
        {
            if (this.Element == null || this.Control == null)
                return;

            switch (this.Element.LineBreakMode)
            {
                case Xamarin.Forms.LineBreakMode.NoWrap:
                    this.Control.LineBreakMode = UIKit.UILineBreakMode.Clip;
                    break;
                case Xamarin.Forms.LineBreakMode.WordWrap:
                    this.Control.LineBreakMode = UIKit.UILineBreakMode.WordWrap;
                    break;
                case Xamarin.Forms.LineBreakMode.CharacterWrap:
                    this.Control.LineBreakMode = UIKit.UILineBreakMode.CharacterWrap;
                    break;
                case Xamarin.Forms.LineBreakMode.HeadTruncation:
                    this.Control.LineBreakMode = UIKit.UILineBreakMode.HeadTruncation;
                    break;
                case Xamarin.Forms.LineBreakMode.TailTruncation:
                    this.Control.LineBreakMode = UIKit.UILineBreakMode.TailTruncation;
                    break;
                case Xamarin.Forms.LineBreakMode.MiddleTruncation:
                    this.Control.LineBreakMode = UIKit.UILineBreakMode.MiddleTruncation;
                    break;
                default:
                    break;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if(e?.NewElement != null)
            {
                this.EnsureLineBreakMode();
                this.UpdateLetterSpacing(this.Control, this.Element.LetterSpacing);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e?.PropertyName)
            {
                case nameof(MaterialLabel.LetterSpacing):
                case nameof(Label.Text):
                    this.UpdateLetterSpacing(this.Control, this.Element.LetterSpacing);
                    this.CheckIfSingleLine();
                    this.EnsureLineBreakMode();
                    break;
            }
        }

        private void UpdateLetterSpacing(UILabel uiLabel, double letterSpacing)
        {
            if (uiLabel == null || this.AttributedString == null) return;

            var range = new NSRange(0, uiLabel.Text?.Length ?? 0);
            var attr = new NSMutableAttributedString(this.Control.AttributedText);
            attr.AddAttribute(UIStringAttributeKey.KerningAdjustment, FromObject((float)letterSpacing), range);

            this.Control.AttributedText = attr;
        }
    }
}