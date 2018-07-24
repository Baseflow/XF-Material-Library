
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Material.Resources.Typography
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialTypography : ResourceDictionary
    {
        internal MaterialTypography(MaterialFontConfiguration fontFamily) : this()
        {
            if (fontFamily != null)
            {
                this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_H1, fontFamily.H1);
                this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_H2, fontFamily.H2);
                this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_H3, fontFamily.H3);
                this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_H4, fontFamily.H4);
                this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_H5, fontFamily.H5);
                this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_H6, fontFamily.H6);
                this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_SUBTITLE1, fontFamily.Subtitle1);
                this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_SUBTITLE2, fontFamily.Subtitle2);
                this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_BODY1, fontFamily.Body1);
                this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_BODY2, fontFamily.Body2);
                this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_BUTTON, fontFamily.Button);
                this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_CAPTION, fontFamily.Caption);
                this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_OVERLINE, fontFamily.Overline);
            }
        }

        internal MaterialTypography()
        {
            InitializeComponent();
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