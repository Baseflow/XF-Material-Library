using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using XF.Material.Resources.Typography;

namespace XF.Material.Effects
{
    public static class MaterialEffectsUtil
    {
        public static readonly BindableProperty TypeScaleProperty = BindableProperty.Create("TypeScale", typeof(MaterialTypeScale), typeof(MaterialTypographyEffect), MaterialTypeScale.Body2, propertyChanged: TypeScaleChanged);

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

            if(view == null)
            {
                return;
            }

            var typeScale = (MaterialTypeScale)newValue;
            var oldEffect = view.Effects.FirstOrDefault(e => e is MaterialTypographyEffect);

            if(oldEffect != null)
            {
                view.Effects.Remove(oldEffect);
            }

            view.Effects.Add(new MaterialTypographyEffect(typeScale));

        }
    }
}
