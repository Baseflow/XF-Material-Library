using Xamarin.Forms;
using XF.Material.Forms.Resources.Typography;

namespace XF.Material.Forms.Resources
{
    public class MaterialConfiguration : BindableObject
    {
        public static readonly BindableProperty FontConfigurationProperty = BindableProperty.Create(nameof(FontConfiguration), typeof(MaterialFontConfiguration), typeof(MaterialFontConfiguration));
        public static readonly BindableProperty ColorConfigurationProperty = BindableProperty.Create(nameof(ColorConfiguration), typeof(MaterialColorConfiguration), typeof(MaterialColorConfiguration));

        public MaterialFontConfiguration FontConfiguration
        {
            get => (MaterialFontConfiguration)GetValue(FontConfigurationProperty);
            set => SetValue(FontConfigurationProperty, value);
        }

        public MaterialColorConfiguration ColorConfiguration
        {
            get => (MaterialColorConfiguration)GetValue(ColorConfigurationProperty);
            set => SetValue(ColorConfigurationProperty, value);
        }
    }
}
