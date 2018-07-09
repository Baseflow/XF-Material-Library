using System.Linq;
using Xamarin.Forms;
using XF.Material.Resources.Typography;

namespace XF.Material.Effects
{
    public static class MaterialEffectsUtil
    {
        public static readonly BindableProperty LetterSpacingProperty = BindableProperty.Create("LetterSpacing", typeof(MaterialTypeScale), typeof(MaterialTypographyEffect), MaterialTypeScale.Default, propertyChanged: TypeScaleChanged);

        public static void SetLetterSpacing(BindableObject view, MaterialTypeScale typeScale)
        {
            view.SetValue(LetterSpacingProperty, typeScale);
        }

        public static MaterialTypeScale GetLetterSpacing(BindableObject view)
        {
            return (MaterialTypeScale)view.GetValue(LetterSpacingProperty);
        }

        private static void TypeScaleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;

            if (view == null)
            {
                return;
            }

            var typeScale = (MaterialTypeScale)newValue;
            var oldEffect = view.Effects.FirstOrDefault(e => e is MaterialTypographyEffect);

            if (oldEffect != null)
            {
                view.Effects.Remove(oldEffect);
            }

            view.Effects.Add(new MaterialTypographyEffect(typeScale));
        }
    }
}
