using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using XF.Material.Forms.Resources;
using XF.Material.Forms.Views.Internals;

namespace XF.Material.Forms.Views
{
    /// <summary>
    /// A <see cref="NavigationPage"/> that applies a Material Design to a page. 
    /// </summary>
    public class MaterialNavigationPage : NavigationPage
    {
        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(MaterialNavigationPage), true);

        public static readonly BindableProperty StatusBarColorProperty = BindableProperty.Create("StatusBarColor", typeof(Color), typeof(MaterialNavigationPage), Color.Default);

        public static readonly BindableProperty AppBarColorProperty = BindableProperty.Create("AppBarColor", typeof(Color), typeof(MaterialNavigationPage), Color.Default);

        public static readonly BindableProperty AppBarTextColorProperty = BindableProperty.Create("AppBarTextColor", typeof(Color), typeof(MaterialNavigationPage), Color.Default);

        public static Color GetStatusBarColor(BindableObject view)
        {
            return (Color)view.GetValue(StatusBarColorProperty);
        }

        public static void SetStatusBarColor(BindableObject view, Color color)
        {
            view.SetValue(StatusBarColorProperty, color);
        }

        public static Color GetAppBarColor(BindableObject view)
        {
            return (Color)view.GetValue(AppBarColorProperty);
        }

        public static void SetAppBarColor(BindableObject view, Color color)
        {
            view.SetValue(AppBarColorProperty, color);
        }

        public static Color GetAppBarTextColor(BindableObject view)
        {
            return (Color)view.GetValue(AppBarTextColorProperty);
        }

        public static void SetAppBarTextColor(BindableObject view, Color color)
        {
            view.SetValue(AppBarTextColorProperty, color);
        }

        public bool HasShadow
        {
            get => (bool)this.GetValue(HasShadowProperty);
            set => this.SetValue(HasShadowProperty, value);
        }

        public MaterialNavigationPage(Page rootPage) : base(rootPage)
        {
            if (rootPage.BackgroundColor.IsDefault)
            {
                rootPage.SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.BACKGROUND);
            }

            this.ChangeStatusBarColor(rootPage);
            this.ChangeBarBackgroundColor(rootPage);
            this.ChangeBarTextColor(rootPage);
        }

        public virtual void OnPagePush(Page page)
        {
            if (page.BackgroundColor.IsDefault)
            {
                page.SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.BACKGROUND);
            }

            this.ChangeStatusBarColor(page);
            this.ChangeBarBackgroundColor(page);
            this.ChangeBarTextColor(page);
        }

        public virtual void OnPagePop(Page previsouPage)
        {
            if (previsouPage.BackgroundColor.IsDefault)
            {
                previsouPage.SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.BACKGROUND);
            }

            this.ChangeStatusBarColor(previsouPage);
            this.ChangeBarBackgroundColor(previsouPage);
            this.ChangeBarTextColor(previsouPage);
        }

        private void ChangeStatusBarColor(Page page)
        {
            var statusBarColor = (Color)page.GetValue(StatusBarColorProperty);
            Material.PlatformConfiguration.ChangeStatusBarColor(statusBarColor.IsDefault ? Material.Color.PrimaryVariant : statusBarColor);
        }

        private void ChangeBarBackgroundColor(Page page)
        {
            var barColor = (Color)page.GetValue(AppBarColorProperty);

            if (barColor.IsDefault)
            {
                this.SetDynamicResource(BarBackgroundColorProperty, MaterialConstants.Color.PRIMARY);
            }

            else
            {
                this.BarBackgroundColor = barColor;
            }
        }

        private void ChangeBarTextColor(Page page)
        {
            var barTextColor = (Color)page.GetValue(AppBarTextColorProperty);

            if (barTextColor.IsDefault)
            {
                this.SetDynamicResource(BarTextColorProperty, MaterialConstants.Color.ON_PRIMARY);
            }

            else
            {
                this.BarTextColor = barTextColor;
            }
        }
    }
}
