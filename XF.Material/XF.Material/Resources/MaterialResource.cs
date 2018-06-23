using Xamarin.Forms;

namespace XF.Material.Resources
{
    public class MaterialResource : BindableObject
    {
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(MaterialFontFamily), typeof(MaterialFontFamily));
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(MaterialColor), typeof(MaterialColor));

        public MaterialFontFamily FontFamily
        {
            get => (MaterialFontFamily)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public MaterialColor Color
        {
            get => (MaterialColor)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }
    }
}
