using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XF.Material.Resources;
using XF.Material.Resources.Typography;

namespace XF.Material.Effects
{
    public class MaterialTypographyEffect : BaseMaterialEffect
    {
        public MaterialTypographyEffect(MaterialTypeScale typeScale) : base("Material.TypographyEffect")
        {
            var key = $"Material.LetterSpacing.{typeScale.ToString()}";
            var value = Application.Current.Resources[key];
            var letterSpacing = Convert.ToDouble(value);

            this.TypeScale = typeScale;
            this.LetterSpacing = letterSpacing;
        }

        public double LetterSpacing { get; }

        public MaterialTypeScale TypeScale { get; }

        public override void ApplyEffect()
        {
            base.ApplyEffect();

            switch (this.TypeScale)
            {
                case MaterialTypeScale.Button:
                    this.MaterialElement.SetDynamicResource(Button.FontFamilyProperty, MaterialConstants.MATERIAL_FONTFAMILY_REGULAR);
                    this.MaterialElement.SetDynamicResource(Button.FontSizeProperty, MaterialConstants.MATERIAL_FONTSIZE_BUTTON);
                    break;
            }
        }
    }
}
