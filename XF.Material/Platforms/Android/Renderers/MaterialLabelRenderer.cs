using System.ComponentModel;
using Android.Content;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Forms.UI;

[assembly: ExportRenderer(typeof(MaterialLabel), typeof(MaterialLabelRenderer))]

namespace XF.Material.Droid.Renderers
{
    public class MaterialLabelRenderer : LabelRenderer
    {
        private new MaterialLabel Element => base.Element as MaterialLabel;

        public MaterialLabelRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement == null) return;

            this.OnLetterSpacingChanged(this.Control, this.Element.LetterSpacing);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e?.PropertyName)
            {
                case nameof(MaterialLabel.LetterSpacing):
                    this.OnLetterSpacingChanged(this.Control, this.Element.LetterSpacing);
                    break;
            }
        }

        private void OnLetterSpacingChanged(TextView textView, double letterSpacing)
        {
            if (!Material.IsLollipop)
            {
                return;
            }

            textView.LetterSpacing = MaterialHelper.ConvertToSp(letterSpacing) / textView.TextSize;
        }
    }
}