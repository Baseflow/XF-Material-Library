using Xamarin.Forms;
using XF.Material.Resources;

namespace XF.Material.Dialogs.Configurations
{
    /// <summary>
    /// A class that provides properties specifically for styling <see cref="MaterialDialog"/>.
    /// </summary>
    public class MaterialAlertDialogConfiguration : BaseMaterialDialogConfiguration
    {
        /// <summary>
        /// Gets or sets the title text color of the alert dialog.
        /// </summary>
        public Color TitleTextColor { get; set; } = Color.FromHex("#DE000000");

        /// <summary>
        /// Gets or sets the title font family of the alert dialog.
        /// </summary>
        public string TitleFontFamily { get; set; } = Material.GetMaterialResource<string>(MaterialConstants.FontFamily.H6);

        /// <summary>
        /// Gets or sets the button font family of the alert dialog.
        /// </summary>
        public string ButtonFontFamily { get; set; } = Material.GetMaterialResource<string>(MaterialConstants.FontFamily.BUTTON);

        /// <summary>
        /// Gets or sets whether the button's label text of the alert dialog should all be capitalized or not.
        /// </summary>
        public bool ButtonAllCaps { get; set; } = true;
    }
}
