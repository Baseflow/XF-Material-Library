using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using XF.Material.Forms.Resources.Typography;

namespace XF.Material.Forms.Effects
{
    public class MaterialTypeScaleEffect : BaseMaterialEffect
    {
        public MaterialTypeScaleEffect(MaterialTypeScale typeScale) : this()
        {
            var key = $"Material.LetterSpacing.{typeScale.ToString()}";
            var value = Application.Current.Resources[key];
            var letterSpacing = Convert.ToDouble(value);

            this.TypeScale = typeScale;
            this.LetterSpacing = letterSpacing;
        }

        internal MaterialTypeScaleEffect() : base("Material.TypeScaleEffect") { }

        public double LetterSpacing { get; }

        public MaterialTypeScale TypeScale { get; }
    }
}
