using Xamarin.Forms;
using XF.Material.Forms.Resources.Typography;

namespace XF.Material.Forms.Resources
{
    /// <inheritdoc />
    /// <summary>
    /// Class that provides Material theme configuration that will be applied in the current App.
    /// </summary>
    public class MaterialConfiguration : BindableObject
    {
        /// <summary>
        /// Backing field for the bindable property <see cref="FontConfiguration"/>.
        /// </summary>
        public static readonly BindableProperty FontConfigurationProperty = BindableProperty.Create(nameof(FontConfiguration), typeof(MaterialFontConfiguration), typeof(MaterialFontConfiguration));

        /// <summary>
        /// Backing field for the bindable property <see cref="ColorConfiguration"/>.
        /// </summary>
        public static readonly BindableProperty ColorConfigurationProperty = BindableProperty.Create(nameof(ColorConfiguration), typeof(MaterialColorConfiguration), typeof(MaterialColorConfiguration));

        /// <summary>
        /// Gets or sets the font configuration of the theme.
        /// </summary>
        public MaterialFontConfiguration FontConfiguration
        {
            get => (MaterialFontConfiguration)this.GetValue(FontConfigurationProperty);
            set => this.SetValue(FontConfigurationProperty, value);
        }

        /// <summary>
        /// Gets or sets the color configuration of the theme.
        /// </summary>
        public MaterialColorConfiguration ColorConfiguration
        {
            get => (MaterialColorConfiguration)this.GetValue(ColorConfigurationProperty);
            set => this.SetValue(ColorConfigurationProperty, value);
        }
    }
}
