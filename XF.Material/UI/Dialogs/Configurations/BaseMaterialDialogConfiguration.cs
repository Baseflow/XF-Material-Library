using Microsoft.Maui;

namespace XF.Material.Maui.UI.Dialogs.Configurations
{
    /// <summary>
    /// Base class that provides properties for styling alert dialogs, loading dialogs, simple dialogs, and snackbars.
    /// </summary>
    public abstract class BaseMaterialDialogConfiguration : BaseMaterialModalDialogConfiguration
    {
        /// <summary>
        /// Gets or sets the message font family of the dialog.
        /// </summary>
        public virtual string MessageFontFamily { get; set; } = Material.FontFamily.Body1;

        /// <summary>
        /// Gets or sets the message text color of the dialog.
        /// </summary>
        public virtual Color MessageTextColor { get; set; } = Color.FromArgb("#99000000");
    }
}