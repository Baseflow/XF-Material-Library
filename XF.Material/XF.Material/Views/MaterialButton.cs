using Xamarin.Forms;
using XF.Material.Resources;

namespace XF.Material.Views
{
    public class MaterialButton : Button
    {
        public MaterialButton()
        {
            this.Style = Application.Current.Resources[MaterialConstants.MATERIAL_STYLE_BUTTON] as Style;
        }
    }
}
