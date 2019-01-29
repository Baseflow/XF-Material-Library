using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
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
        public new MaterialLabel Element => base.Element as MaterialLabel;

        public MaterialLabelRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if(e?.NewElement != null)
            {
                this.OnLetterSpacingChanged(this.Control, this.Element.LetterSpacing);
                this.OnLineHeightChanged(this.Control, this.Element.LineHeight);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if(e?.PropertyName == nameof(MaterialLabel.LetterSpacing))
            {
                this.OnLetterSpacingChanged(this.Control, this.Element.LetterSpacing);
            }

            if(e?.PropertyName == nameof(MaterialLabel.LineHeight))
            {
                this.OnLineHeightChanged(this.Control, this.Element.LineHeight);
            }
        }

        private void OnLetterSpacingChanged(TextView textView, double letterSpacing)
        {
            if(!Material.IsLollipop)
            {
                return;
            }

            textView.LetterSpacing = MaterialHelper.ConvertToSp(letterSpacing) / textView.TextSize;
        }

        private void OnLineHeightChanged(TextView textView, double lineHeight)
        {
            textView.SetLineSpacing(0f, (float)lineHeight);
        }
    }
}