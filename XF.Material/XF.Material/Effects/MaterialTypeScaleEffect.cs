using System;
using Xamarin.Forms;
using XF.Material.Resources.Typography;

namespace XF.Material.Effects
{
    public class MaterialTypeScaleEffect : BaseMaterialEffect
    {
        public MaterialTypeScaleEffect(MaterialTypeScale typeScale) : base("Material.TypeScaleEffect")
        {
            var key = $"Material.LetterSpacing.{typeScale.ToString()}";
            var value = Application.Current.Resources[key];
            var letterSpacing = Convert.ToDouble(value);

            this.TypeScale = typeScale;
            this.LetterSpacing = letterSpacing;
        }

        public double LetterSpacing { get; }

        public MaterialTypeScale TypeScale { get; }
    }
}
