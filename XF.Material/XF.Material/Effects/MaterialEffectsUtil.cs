using System.Linq;
using Xamarin.Forms;
using XF.Material.Resources.Typography;

namespace XF.Material.Effects
{
    public static class MaterialEffectsUtil
    {
        public static readonly BindableProperty TypeScaleProperty = BindableProperty.Create("TypeScale", typeof(MaterialTypeScale), typeof(MaterialTypeScaleEffect), MaterialTypeScale.None, propertyChanged: TypeScaleChanged);

        public static void SetTypeScale(BindableObject view, MaterialTypeScale typeScale)
        {
            view.SetValue(TypeScaleProperty, typeScale);
        }

        public static MaterialTypeScale GetTypeScale(BindableObject view)
        {
            return (MaterialTypeScale)view.GetValue(TypeScaleProperty);
        }

        private static void TypeScaleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;

            if (view == null)
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
