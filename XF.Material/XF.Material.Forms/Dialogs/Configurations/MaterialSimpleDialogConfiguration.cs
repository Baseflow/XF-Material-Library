using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XF.Material.Forms.Dialogs.Configurations
{
    /// <summary>
    /// A class that provides properties specifically for styling simple dialogs.
    /// </summary>
    public class MaterialSimpleDialogConfiguration : BaseMaterialDialogConfiguration
    {
        /// <summary>
        /// Gets or sets the title text color of the alert dialog.
        /// </summary>
        public Color TitleTextColor { get; set; } = Color.FromHex("#DE000000");

        /// <summary>
        /// Gets or sets the title font family of the alert dialog.
        /// </summary>
        public string TitleFontFamily { get; set; } = Material.FontConfiguration.H6;

        public override Color MessageTextColor { get; set; } = Color.FromHex("#DE000000#");
    }
}
