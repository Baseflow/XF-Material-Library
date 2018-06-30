using System;
using System.Collections.Generic;
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
using XF.Material.Droid.Effects;

[assembly: ResolutionGroupName("Material")]
[assembly: ExportEffect(typeof(MaterialTypographyEffect), "TypographyEffect")]
namespace XF.Material.Droid.Effects
{
    public class MaterialTypographyEffect : BaseMaterialEffect<XF.Material.Effects.MaterialTypographyEffect>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            if (!Material.IsLollipop) { return; }

            if(this.Control is Android.Widget.TextView textView)
            {
                textView.LetterSpacing = (float)this.MaterialEffect.LetterSpacing;
            }
        }
    }
}