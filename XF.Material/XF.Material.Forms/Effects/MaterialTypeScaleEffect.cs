using System;
using Xamarin.Forms;
using XF.Material.Forms.Resources.Typography;

namespace XF.Material.Forms.Effects
{
    /// <summary>
    /// A Material effect that can be attached to <see cref="Label"/> and <see cref="Button"/> views to apply Material typography to their text.
    /// </summary>
    public class MaterialTypeScaleEffect : BaseMaterialEffect
    {
        /// <summary>
        /// Creates a new instance of <see cref="MaterialTypeScaleEffect"/>.
        /// </summary>
        /// <param name="typeScale">The type scale to apply.</param>
        public MaterialTypeScaleEffect(MaterialTypeScale typeScale) : base("Material.TypeScaleEffect")
        {
            var key = $"Material.LetterSpacing.{typeScale.ToString()}";
            var value = Application.Current.Resources[key];
            var letterSpacing = Convert.ToDouble(value);

            this.TypeScale = typeScale;
            this.LetterSpacing = letterSpacing;
        }

        /// <summary>
        /// Gets the letter spacing of the text.
        /// </summary>
        public double LetterSpacing { get; }

        /// <summary>
        /// Gets the type scale applied to the text.
        /// </summary>
        public MaterialTypeScale TypeScale { get; }
    }
}
