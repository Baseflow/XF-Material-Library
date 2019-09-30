using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Material.Forms.Resources.Typography
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialTypography : ResourceDictionary
    {
        internal MaterialTypography(MaterialFontConfiguration fontFamily)
        {
            InitializeComponent();

            if (fontFamily == null)
            {
                return;
            }

            TryAddStringResource(MaterialConstants.FontFamily.H1, fontFamily.H1);
            TryAddStringResource(MaterialConstants.FontFamily.H2, fontFamily.H2);
            TryAddStringResource(MaterialConstants.FontFamily.H3, fontFamily.H3);
            TryAddStringResource(MaterialConstants.FontFamily.H4, fontFamily.H4);
            TryAddStringResource(MaterialConstants.FontFamily.H5, fontFamily.H5);
            TryAddStringResource(MaterialConstants.FontFamily.H6, fontFamily.H6);
            TryAddStringResource(MaterialConstants.FontFamily.SUBTITLE1, fontFamily.Subtitle1);
            TryAddStringResource(MaterialConstants.FontFamily.SUBTITLE2, fontFamily.Subtitle2);
            TryAddStringResource(MaterialConstants.FontFamily.BODY1, fontFamily.Body1);
            TryAddStringResource(MaterialConstants.FontFamily.BODY2, fontFamily.Body2);
            TryAddStringResource(MaterialConstants.FontFamily.BUTTON, fontFamily.Button);
            TryAddStringResource(MaterialConstants.FontFamily.CAPTION, fontFamily.Caption);
            TryAddStringResource(MaterialConstants.FontFamily.OVERLINE, fontFamily.Overline);
        }

        private void TryAddStringResource(string key, string value)
        {
            Add(key, value);
        }
    }
}