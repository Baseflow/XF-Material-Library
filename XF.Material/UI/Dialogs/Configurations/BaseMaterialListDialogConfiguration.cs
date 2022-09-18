using Microsoft.Maui;

namespace XF.Material.Maui.UI.Dialogs.Configurations
{
    public abstract class BaseMaterialListDialogConfiguration : BaseMaterialModalDialogConfiguration
    {
        /// <summary>
        /// Gets or sets the title text color of the dialog.
        /// </summary>
        public Color TitleTextColor { get; set; } = Color.FromArgb("#DE000000");

        /// <summary>
        /// Gets or sets the title font family of the dialog.
        /// </summary>
        public string TitleFontFamily { get; set; } = Material.FontFamily.H6;

        /// <summary>
        /// Gets or sets the body text color.
        /// </summary>
        public Color TextColor { get; set; } = Color.FromArgb("#DE000000");

        /// <summary>
        /// Gets or sets the corner radius of the dialog.
        /// </summary>
        public override float CornerRadius { get; set; } = 4;

        /// <summary>
        /// Gets or sets the list text font family of the dialog.
        /// </summary>
        public string TextFontFamily { get; set; } = Material.FontFamily.Body1;
    }
}
