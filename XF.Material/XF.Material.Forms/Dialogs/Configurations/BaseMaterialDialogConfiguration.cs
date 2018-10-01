using Xamarin.Forms;

namespace XF.Material.Forms.Dialogs.Configurations
{
    /// <summary>
    /// Base class that provides properties for styling alert dialogs, loading dialogs, simple dialogs, and snackbars.
    /// </summary>
    public abstract class BaseMaterialDialogConfiguration
    {
        /// <summary>
        /// Gets or sets the background color of the dialog.
        /// </summary>
        public virtual Color BackgroundColor { get; set; } = Color.White;

        /// <summary>
        /// Gets or sets the corner radius of the dialog.
        /// </summary>
        public virtual float CornerRadius { get; set; } = 2;

        /// <summary>
        /// Gets or sets the message font family of the dialog.
        /// </summary>
        public virtual string MessageFontFamily { get; set; } = Material.FontFamily.Body1;

        /// <summary>
        /// Gets or sets the message text color of the dialog.
        /// </summary>
        public virtual Color MessageTextColor { get; set; } = Color.FromHex("#99000000");

        /// <summary>
        /// Gets or sets the scrim color of the dialog.
        /// </summary>
        public virtual Color ScrimColor { get; set; } = Color.FromHex("#51000000");

        /// <summary>
        /// Gets or sets the tint color of the dialog.
        /// </summary>
        public virtual Color TintColor { get; set; } = Material.Color.Secondary;
    }
}
