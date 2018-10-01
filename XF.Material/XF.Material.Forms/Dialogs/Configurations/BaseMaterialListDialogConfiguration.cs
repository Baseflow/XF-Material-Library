using Xamarin.Forms;

namespace XF.Material.Forms.Dialogs.Configurations
{
    public abstract class BaseMaterialListDialogConfiguration
    {
        /// <summary>
        /// Gets or sets the title text color of the alert dialog.
        /// </summary>
        public Color TitleTextColor { get; set; } = Color.FromHex("#DE000000");

        /// <summary>
        /// Gets or sets the title font family of the alert dialog.
        /// </summary>
        public string TitleFontFamily { get; set; } = Material.FontFamily.H6;

        public Color TextColor { get; set; } = Color.FromHex("#DE000000");

        /// <summary>
        /// Gets or sets the scrim color of the dialog.
        /// </summary>
        public Color ScrimColor { get; set; } = Color.FromHex("#51000000");

        /// <summary>
        /// Gets or sets the background color of the dialog.
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.White;

        /// <summary>
        /// Gets or sets the corner radius of the dialog.
        /// </summary>
        public float CornerRadius { get; set; } = 2;

        /// <summary>
        /// Gets or sets the list text font family of the dialog.
        /// </summary>
        public string TextFontFamily { get; set; } = Material.FontFamily.Body1;
    }
}
