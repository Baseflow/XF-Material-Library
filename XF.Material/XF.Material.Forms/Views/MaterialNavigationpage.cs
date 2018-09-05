using Xamarin.Forms;
using XF.Material.Forms.Resources;

namespace XF.Material.Forms.Views
{
    /// <summary>
    /// A <see cref="NavigationPage"/> that adds a shadow to the toolbar. 
    /// </summary>
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
            this.SetDynamicResource(BarBackgroundColorProperty, MaterialConstants.Color.PRIMARY);
            this.SetDynamicResource(BarTextColorProperty, MaterialConstants.Color.ONPRIMARY);
            this.Pushed += this.MaterialNavigationPage_Pushed;

            if(rootPage.BackgroundColor.IsDefault)
            {
                rootPage.SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.BACKGROUND);
            }
        }

        private void MaterialNavigationPage_Pushed(object sender, NavigationEventArgs e)
        {
            if (e?.Page != null && e.Page.BackgroundColor.IsDefault)
            {
                e.Page.SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.BACKGROUND);
            }
        }
    }
}
