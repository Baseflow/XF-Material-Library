
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
                this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_REGULAR, fontFamily.Regular);
                this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_MEDIUM, fontFamily.Medium);
                this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_BOLD, fontFamily.Bold);
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