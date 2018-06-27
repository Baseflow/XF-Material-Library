using System.Runtime.CompilerServices;
using Xamarin.Forms;
using XF.Material.Resources;

namespace XF.Material.Views
{
    public class MaterialButton : Button
    {
        public static readonly BindableProperty AllCapsProperty = BindableProperty.Create(nameof(AllCaps), typeof(bool), typeof(MaterialButton), true);

        public bool AllCaps
        {
            get => (bool)GetValue(AllCapsProperty);
            set => SetValue(AllCapsProperty, value);
        }

        public MaterialButton()
        {
            this.Style = Application.Current.Resources[MaterialConstants.MATERIAL_STYLE_BUTTON] as Style;
        }
    }
}
