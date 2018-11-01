using System.Linq;
using Xamarin.Forms;
using XF.Material.Forms.Resources.Typography;

namespace XF.Material.Forms.Effects
{
    /// <summary>
    /// Static class with attached properties to apply Material typography effects.
    /// </summary>
    public static class MaterialTypographyEffect
    {
        /// <summary>
        /// Attached property for applying type scale effects to <see cref="Label"/> and <see cref="Button"/> views. Adds a <see cref="MaterialTypeScaleEffect"/> to the view.
        /// </summary>
        public static readonly BindableProperty TypeScaleProperty = BindableProperty.Create("TypeScale", typeof(MaterialTypeScale), typeof(VisualElement), MaterialTypeScale.None, propertyChanged: TypeScaleChanged);


        /// <summary>
        /// For binding use only.
        /// </summary>
        public static MaterialTypeScale GetTypeScale(BindableObject view)
        {
            return (MaterialTypeScale)view.GetValue(TypeScaleProperty);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static void SetTypeScale(BindableObject view, MaterialTypeScale typeScale)
        {
            view.SetValue(TypeScaleProperty, typeScale);
        }

        private static void TypeScaleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is View view))
            {
                return;
            }

            var typeScale = (MaterialTypeScale)newValue;
            var oldEffect = view.Effects.FirstOrDefault(e => e is MaterialTypeScaleEffect);

            if (oldEffect != null)
            {
                view.Effects.Remove(oldEffect);
            }

            view.Effects.Add(new MaterialTypeScaleEffect(typeScale));
        }
    }
}
