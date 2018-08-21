using Xamarin.Forms;
using XF.Material.Resources;

namespace XF.Material.Dialogs.Configurations
{
    /// <summary>
    /// Base class that provides properties for styling <see cref="MaterialDialog"/> and <see cref="MaterialLoadingDialog"/>.
    /// </summary>
    public abstract class BaseMaterialDialogConfiguration
    {
        public Color AccentColor { get; set; } = Material.GetMaterialResource<Color>(MaterialConstants.MATERIAL_COLOR_SECONDARY);

        public Color BackgroundColor { get; set; } = Color.White;

        public Color ScrimColor { get; set; } = Color.FromHex("#51000000");

        public float CornerRadius { get; set; } = 2;
    }
}
