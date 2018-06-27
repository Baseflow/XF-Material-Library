using System;
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

        //public override void ApplyEffect()
        //{
        //    switch (this.TypeScale)
        //    {
        //        case MaterialTypeScale.Body1:
        //            this.MaterialElement.SetDynamicResource(Label.FontFamilyProperty, MaterialConstants.MATERIAL_FONTFAMILY_REGULAR);
        //            this.MaterialElement.SetDynamicResource(Label.FontSizeProperty, MaterialConstants.MATERIAL_FONTSIZE_BODY1);
        //            break;
        //        case MaterialTypeScale.Body2:
        //            this.MaterialElement.SetDynamicResource(Label.FontFamilyProperty, MaterialConstants.MATERIAL_FONTFAMILY_REGULAR);
        //            this.MaterialElement.SetDynamicResource(Label.FontSizeProperty, MaterialConstants.MATERIAL_FONTSIZE_BODY2);
        //            break;
        //        case MaterialTypeScale.Button:
        //            //this.MaterialElement.SetDynamicResource(Button.FontFamilyProperty, MaterialConstants.MATERIAL_FONTFAMILY_MEDIUM);
        //            //this.MaterialElement.SetDynamicResource(Button.FontSizeProperty, MaterialConstants.MATERIAL_FONTSIZE_BUTTON);
        //            break;
        //    }
        //}
    }
}
