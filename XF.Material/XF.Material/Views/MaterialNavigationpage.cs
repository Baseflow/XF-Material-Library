using Xamarin.Forms;
using XF.Material.Resources;

namespace XF.Material.Views
{
    public class MaterialNavigationPage : NavigationPage
    {
        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(MaterialNavigationPage), true);

        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        public MaterialNavigationPage(Page rootPage) : base(rootPage)
        {
            this.SetDynamicResource(BarBackgroundColorProperty, MaterialConstants.MATERIAL_COLOR_PRIMARY);
            this.SetDynamicResource(BarTextColorProperty, MaterialConstants.MATERIAL_COLOR_ONPRIMARY);
            this.SetDynamicResource(BackgroundColorProperty, MaterialConstants.MATERIAL_COLOR_BACKGROUND);
        }
    }
}
