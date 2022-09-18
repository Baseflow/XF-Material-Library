using Microsoft.Maui;

namespace XF.Material.Maui.UI.Dialogs.Configurations
{
    /// <summary>
    /// Base class configuration of all modal dialogs.
    /// </summary>
    public abstract class BaseMaterialModalDialogConfiguration
    {
        /// <summary>
        /// Gets or sets the background color of the dialog.
        /// </summary>
        public virtual Color BackgroundColor { get; set; } = Colors.White;

        /// <summary>
        /// Gets or sets the corner radius of the dialog.
        /// </summary>
        public virtual float CornerRadius { get; set; } = 4f;

        /// <summary>
        /// Gets or sets the scrim color of the dialog.
        /// </summary>
        public virtual Color ScrimColor { get; set; } = Color.FromArgb("#51000000");

        /// <summary>
        /// Gets or sets the tint color of the dialog.
        /// </summary>
        public virtual Color TintColor { get; set; } = Material.Color.Secondary;

        /// <summary>
        /// Gets or sets the margin of the dialog.
        /// </summary>
        public virtual Thickness Margin { get; set; } = Material.GetResource<Thickness>("Material.Dialog.Margin");
    }
}
