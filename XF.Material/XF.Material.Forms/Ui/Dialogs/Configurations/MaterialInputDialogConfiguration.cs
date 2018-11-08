using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace XF.Material.Forms.UI.Dialogs.Configurations
{
    /// <summary>
    /// A class that provides properties specifically for styling input dialogs.
    /// </summary>
    public class MaterialInputDialogConfiguration : MaterialAlertDialogConfiguration
    {
        /// <summary>
        /// Gets or sets the maximum input length of the textfield.
        /// </summary>
        public int InputMaxLength { get; set; }

        /// <summary>
        /// Gets or sets the color of the textfield's placeholder.
        /// </summary>
        public Color InputPlaceholderColor { get; set; } = Color.FromHex("#99000000");

        /// <summary>
        /// Gets or sets the font family of the textfield's placeholder.
        /// </summary>
        public string InputPlaceholderFontFamily { get; set; }

        /// <summary>
        /// Gets or sets the color of the textfield's text.
        /// </summary>
        public Color InputTextColor { get; set; } = Color.FromHex("#D0000000");

        /// <summary>
        /// Gets or sets the font family of the textfield's text.
        /// </summary>
        public string InputTextFontFamily { get; set; }

        /// <summary>
        /// Gets or sets the input type of the textfield.
        /// </summary>
        public MaterialTextFieldInputType InputType { get; set; }
    }
}
