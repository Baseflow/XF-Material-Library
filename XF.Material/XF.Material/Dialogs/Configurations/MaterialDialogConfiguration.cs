using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XF.Material.Resources;

namespace XF.Material.Dialogs.Configurations
{
    /// <summary>
    /// A class that provides properties specifically for styling <see cref="MaterialDialog"/>.
    /// </summary>
    public sealed class MaterialDialogConfiguration : BaseMaterialDialogConfiguration
    {
        public Color TitleTextColor { get; set; } = Color.FromHex("#DE000000");

        public Color MessageTextColor { get; set; } = Color.FromHex("#99000000");

        public string TitleFontFamily { get; set; } = Material.GetMaterialResource<string>(MaterialConstants.MATERIAL_FONTFAMILY_H6);

        public string MessageFontFamily { get; set; } = Material.GetMaterialResource<string>(MaterialConstants.MATERIAL_FONTFAMILY_BODY1);

        public string ButtonFontFamily { get; set; } = Material.GetMaterialResource<string>(MaterialConstants.MATERIAL_FONTFAMILY_BUTTON);

        public bool ButtonAllCaps { get; set; } = true;
    }
}
