using Xamarin.Forms;
using XF.Material.Forms.Resources;

namespace XF.Material.Forms.Views
{
    /// <summary>
    /// A <see cref="NavigationPage"/> that applies a Material Design to a page. 
    /// </summary>
    public class MaterialNavigationPage : NavigationPage
    {
        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(MaterialNavigationPage), true);

        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => this.SetValue(HasShadowProperty, value);
        }

        public MaterialNavigationPage(Page rootPage) : base(rootPage)
        {
            this.SetDynamicResource(BarBackgroundColorProperty, MaterialConstants.Color.PRIMARY);
            this.SetDynamicResource(BarTextColorProperty, MaterialConstants.Color.ON_PRIMARY);

            if (rootPage.BackgroundColor.IsDefault)
            {
                rootPage.SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.BACKGROUND);
                Material.PlatformConfiguration.ChangeStatusBarColor(Material.Color.PrimaryVariant);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.Pushed += this.MaterialNavigationPage_Pushed;
            this.Popped += this.MaterialNavigationPage_Popped;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            this.Pushed -= this.MaterialNavigationPage_Pushed;
            this.Popped -= this.MaterialNavigationPage_Popped;
        }


        protected virtual void OnRootPageSet(Page rootPage) { }

        protected virtual void OnPagePushed(Page page)
        {
            if (page?.BackgroundColor.IsDefault == true)
            {
                page.SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.BACKGROUND);
                Material.PlatformConfiguration.ChangeStatusBarColor(Material.Color.PrimaryVariant);
            }
        }

        protected virtual void OnPagePopped(Page page) { }

        private void MaterialNavigationPage_Pushed(object sender, NavigationEventArgs e)
        {
            this.OnPagePushed(e.Page);
        }

        private void MaterialNavigationPage_Popped(object sender, NavigationEventArgs e)
        {
            this.OnPagePopped(e.Page);
        }
    }
}
