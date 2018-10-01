using Xamarin.Forms;

namespace XF.Material.Forms.Dialogs.Configurations
{
    /// <summary>
    /// A class that provides properties specifically for styling confirmation dialogs.
    /// </summary>
    public class MaterialConfirmationDialogConfiguration : BaseMaterialListDialogConfiguration
    {
        /// <summary>
        /// Gets or sets the button font family of the alert dialog.
        /// </summary>
        public string ButtonFontFamily { get; set; } = Material.FontFamily.Button;

        /// <summary>
        /// Gets or sets whether the button's label text of the alert dialog should all be capitalized or not.
        /// </summary>
        public bool ButtonAllCaps { get; set; } = true;

        /// <summary>
        /// Gets or sets the tint color of the dialog's buttons.
        /// </summary>
        public Color TintColor { get; set; } = Material.Color.Secondary;

        /// <summary>
        /// Gets or sets the color of this selection control when selected.
        /// </summary>
        public Color ControlSelectedColor { get; set; } = Material.Color.Secondary;

        /// <summary>
        /// Gets or sets the color of this selection control when unselected.
        /// </summary>
        public Color ControlUnselectedColor { get; set; } = Color.FromHex("#84000000");
    }
}
