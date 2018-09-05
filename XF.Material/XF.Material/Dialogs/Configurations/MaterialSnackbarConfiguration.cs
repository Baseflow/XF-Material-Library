using Xamarin.Forms;

namespace XF.Material.Dialogs.Configurations
{
    /// <summary>
    /// A class that provides properties specifically for styling <see cref="MaterialSnackbar"/>.
    /// </summary>
    public sealed class MaterialSnackbarConfiguration : BaseMaterialDialogConfiguration
    {
        /// <summary>
        /// Gets or sets the background color of the snackbar.
        /// </summary>
        public override Color BackgroundColor { get; set; } = Color.FromHex("#343434");

        /// <summary>
        /// Gets or sets whether the button's label text of the snackbar should all be capitalized or not.
        /// </summary>
        public bool ButtonAllCaps { get; set; } = true;

        /// <summary>
        /// Gets or sets the font family of the snackbar's button label.
        /// </summary>
        public string ButtonFontFamily { get; set; } = Material.FontConfiguration.Button;

        /// <summary>
        /// Gets or sets the font family of the snackbar's text message.
        /// </summary>
        public override string MessageFontFamily { get; set; } = Material.FontConfiguration.Body2;

        /// <summary>
        /// Gets or sets the color of the snackbar's message text.
        /// </summary>
        public override Color MessageTextColor { get; set; } = Color.FromHex("#DEFFFFFF");

        /// <summary>
        /// Gets or sets the tint color to be used for the snackbar's button.
        /// </summary>
        public override Color TintColor { get; set; } = Color.Yellow;

        /// <summary>
        /// Gets the scrim color of the snackbar.
        /// </summary>
        public override Color ScrimColor { get => Color.Transparent; }

        /// <summary>
        /// Gets the corner radius of the snackbar.
        /// </summary>
        public override float CornerRadius { get => 4; }
    }
}
