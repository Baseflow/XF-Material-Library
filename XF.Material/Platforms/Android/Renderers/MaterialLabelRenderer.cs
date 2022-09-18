using System.ComponentModel;
using Android.Content;
using Android.Widget;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using XF.Material.Droid.Renderers;
using XF.Material.Maui.UI;

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

            if (e?.NewElement == null)
            {
                return;
            }

            OnLetterSpacingChanged(Control, Element.LetterSpacing);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e?.PropertyName)
            {
                case nameof(MaterialLabel.LetterSpacing):
                    OnLetterSpacingChanged(Control, Element.LetterSpacing);
                    break;
            }
        }

        private void OnLetterSpacingChanged(TextView textView, double letterSpacing)
        {
            if (!Material.IsLollipop)
            {
                return;
            }

            textView.LetterSpacing = MaterialHelper.ConvertSpToPx(letterSpacing) / textView.TextSize;
        }
    }
}
