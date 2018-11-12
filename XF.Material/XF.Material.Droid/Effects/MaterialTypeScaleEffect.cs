using Xamarin.Forms;
using XF.Material.Droid.Effects;

[assembly: ExportEffect(typeof(MaterialTypeScaleEffect), "TypeScaleEffect")]
namespace XF.Material.Droid.Effects
{
    public class MaterialTypeScaleEffect : BaseMaterialEffect<Forms.Effects.MaterialTypeScaleEffect>
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