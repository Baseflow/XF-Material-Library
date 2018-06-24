using Xamarin.Forms;
using XF.Material.Resources;

namespace XF.Material.Views
{
    public class MaterialButton : Button
    {
        public MaterialButton()
        {
            this.CornerRadius = 4;
            this.SetupMaterialDynamicResource();
        }

        private void SetupMaterialDynamicResource()
        {
            this.SetDynamicResource(BackgroundColorProperty, MaterialConstants.MATERIAL_COLOR_SECONDARY);
            this.SetDynamicResource(TextColorProperty, MaterialConstants.MATERIAL_COLOR_ONSECONDARY);
            this.SetDynamicResource(FontFamilyProperty, MaterialConstants.MATERIAL_FONTFAMILY_REGULAR);
        }
    }
}
