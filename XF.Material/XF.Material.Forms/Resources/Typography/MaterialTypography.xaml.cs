
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Material.Forms.Resources.Typography
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialTypography : ResourceDictionary
    {
        internal MaterialTypography(MaterialFontConfiguration fontFamily)
        {
            this.InitializeComponent();

            if (fontFamily != null)
            {
                this.TryAddStringResource(MaterialConstants.FontFamily.H1, fontFamily.H1);
                this.TryAddStringResource(MaterialConstants.FontFamily.H2, fontFamily.H2);
                this.TryAddStringResource(MaterialConstants.FontFamily.H3, fontFamily.H3);
                this.TryAddStringResource(MaterialConstants.FontFamily.H4, fontFamily.H4);
                this.TryAddStringResource(MaterialConstants.FontFamily.H5, fontFamily.H5);
                this.TryAddStringResource(MaterialConstants.FontFamily.H6, fontFamily.H6);
                this.TryAddStringResource(MaterialConstants.FontFamily.SUBTITLE1, fontFamily.Subtitle1);
                this.TryAddStringResource(MaterialConstants.FontFamily.SUBTITLE2, fontFamily.Subtitle2);
                this.TryAddStringResource(MaterialConstants.FontFamily.BODY1, fontFamily.Body1);
                this.TryAddStringResource(MaterialConstants.FontFamily.BODY2, fontFamily.Body2);
                this.TryAddStringResource(MaterialConstants.FontFamily.BUTTON, fontFamily.Button);
                this.TryAddStringResource(MaterialConstants.FontFamily.CAPTION, fontFamily.Caption);
                this.TryAddStringResource(MaterialConstants.FontFamily.OVERLINE, fontFamily.Overline);
            }
        }

        private void TryAddStringResource(string key, string value)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
            {
                return;
            }

            this.Add(key, value);
        }
    }
}