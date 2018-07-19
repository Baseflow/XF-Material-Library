using Xamarin.Forms;
using XF.Material.Resources.Typography;

namespace XF.Material.Resources
{
    public class MaterialResource : BindableObject
    {
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(MaterialFontConfiguration), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(MaterialColorConfiguration), typeof(MaterialColorConfiguration));

        public MaterialFontConfiguration FontFamily
        {
            get => (MaterialFontConfiguration)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public MaterialColorConfiguration Color
        {
            get => (MaterialColorConfiguration)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }
    }
}
