using Xamarin.Forms;
using Xamarin.Forms.Internals;
using XF.Material.Droid.Effects;

[assembly: ResolutionGroupName("Material")]
[assembly: ExportEffect(typeof(MaterialTypeScaleEffect), "TypeScaleEffect")]
namespace XF.Material.Droid.Effects
{
    public class MaterialTypeScaleEffect : BaseMaterialEffect<XF.Material.Effects.MaterialTypeScaleEffect>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            if (!Material.IsLollipop)
            {
                return;
            }

            if (this.Control is Android.Widget.TextView textView)
            {
                var rawLetterSpacing = this.MaterialEffect.LetterSpacing / textView.TextSize;
                textView.LetterSpacing = MaterialHelper.ConvertToSp(rawLetterSpacing);
            }
        }
    }
}