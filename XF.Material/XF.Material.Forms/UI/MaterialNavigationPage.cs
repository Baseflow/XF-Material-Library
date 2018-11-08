using Xamarin.Forms;
using XF.Material.Forms.Resources;

namespace XF.Material.Forms.UI
{
    /// <summary>
    /// A <see cref="NavigationPage"/> that applies Material theming to a page.
    /// </summary>
    public class MaterialNavigationPage : NavigationPage
    {
        /// <summary>
        /// Attached property that is used by <see cref="ContentPage"/>s to determine the app bar color.
        /// </summary>
        public static readonly BindableProperty AppBarColorProperty = BindableProperty.Create("AppBarColor", typeof(Color), typeof(MaterialNavigationPage), Color.Default);

        /// <summary>
        /// Attached property that is used by <see cref="ContentPage"/>s to determine whether the app bar text alignment.
        /// </summary>
        public static readonly BindableProperty AppBarTextAlignmentProperty = BindableProperty.Create("AppBarTextAlignment", typeof(TextAlignment), typeof(MaterialNavigationPage), TextAlignment.Start);

        /// <summary>
        /// Attached property that is used by <see cref="ContentPage"/>s to determine whether the app bar text color.
        /// </summary>
        public static readonly BindableProperty AppBarTextColorProperty = BindableProperty.Create("AppBarTextColor", typeof(Color), typeof(MaterialNavigationPage), Color.Default);

        /// <summary>
        /// Attached property that is used by <see cref="ContentPage"/>s to determine whether the app bar text font family.
        /// </summary>
        public static readonly BindableProperty AppBarTextFontFamilyProperty = BindableProperty.Create("AppBarTextFontFamily", typeof(string), typeof(MaterialNavigationPage));

        /// <summary>
        /// Attached property that is used by <see cref="ContentPage"/>s to determine whether the app bar text font family.
        /// </summary>
        public static readonly BindableProperty AppBarTextFontSizeProperty = BindableProperty.Create("AppBarTextFontSize", typeof(double), typeof(MaterialNavigationPage), 24.0);

        /// <summary>
        /// Backing field for the bindable property <see cref="HasShadow"/>.
        /// </summary>
        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create("HasShadow", typeof(bool), typeof(MaterialNavigationPage), true);

        /// <summary>
        /// Attached property that is used by <see cref="ContentPage"/>s to determine the status bar color.
        /// </summary>
        public static readonly BindableProperty StatusBarColorProperty = BindableProperty.Create("StatusBarColor", typeof(Color), typeof(MaterialNavigationPage), Color.Default);

        private Label _customTitleView;
        private bool _firstLoad;

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialNavigationPage"/>.
        /// </summary>
        /// <param name="rootPage">The root page.</param>
        public MaterialNavigationPage(Page rootPage) : base(rootPage)
        {
            if (rootPage.BackgroundColor.IsDefault)
            {
                rootPage.SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.BACKGROUND);
            }

            this.ChangeFont(rootPage);
            this.ChangeBarTextColor(rootPage);
            this.ChangeBarBackgroundColor(rootPage);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static Color GetAppBarColor(BindableObject view)
        {
            return (Color)view.GetValue(AppBarColorProperty);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static TextAlignment GetAppBarTextAlignment(BindableObject view)
        {
            return (TextAlignment)view.GetValue(AppBarTextAlignmentProperty);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static Color GetAppBarTextColor(BindableObject view)
        {
            return (Color)view.GetValue(AppBarTextColorProperty);
        }

        public static string GetAppBarTextFontFamily(BindableObject view)
        {
            return (string)view.GetValue(AppBarTextFontFamilyProperty);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static double GetAppBarTextFontSize(BindableObject view)
        {
            return (double)view.GetValue(AppBarTextFontSizeProperty);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static bool GetHasShadow(BindableObject view)
        {
            return (bool)view.GetValue(HasShadowProperty);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static Color GetStatusBarColor(BindableObject view)
        {
            return (Color)view.GetValue(StatusBarColorProperty);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static void SetAppBarColor(BindableObject view, Color color)
        {
            view.SetValue(AppBarColorProperty, color);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static void SetAppBarTextAlignment(BindableObject view, TextAlignment textAlignment)
        {
            view.SetValue(AppBarTextAlignmentProperty, textAlignment);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static void SetAppBarTextColor(BindableObject view, Color color)
        {
            view.SetValue(AppBarTextColorProperty, color);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static void SetAppBarTextFontFamily(BindableObject view, string fontFamily)
        {
            view.SetValue(AppBarTextFontFamilyProperty, fontFamily);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static void SetAppBarTextFontSize(BindableObject view, double value)
        {
            view.SetValue(AppBarTextFontSizeProperty, value);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static void SetHasShadow(BindableObject view, bool hasShadow)
        {
            view.SetValue(HasShadowProperty, hasShadow);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static void SetStatusBarColor(BindableObject view, Color color)
        {
            view.SetValue(StatusBarColorProperty, color);
        }

        /// <summary>
        /// Called when a page is being popped.
        /// </summary>
        /// <param name="previousPage">The page that will re-appear.</param>
        public virtual void OnPagePop(Page previousPage)
        {
            if (previousPage.BackgroundColor.IsDefault)
            {
                previousPage.SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.BACKGROUND);
            }

            this.ChangeBarTextColor(previousPage);
            this.ChangeStatusBarColor(previousPage);
            this.ChangeBarBackgroundColor(previousPage);

            previousPage.SetValue(BackButtonTitleProperty, string.Empty);
        }

        /// <summary>
        /// Called when a page is being pushed.
        /// </summary>
        /// <param name="page">The page that is being pushed.</param>
        public virtual void OnPagePush(Page page)
        {
            if (page.BackgroundColor.IsDefault)
            {
                page.SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.BACKGROUND);
            }

            this.ChangeFont(page);
            this.ChangeBarTextColor(page);
            this.ChangeStatusBarColor(page);
            this.ChangeBarBackgroundColor(page);

            page.SetValue(BackButtonTitleProperty, string.Empty);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!_firstLoad)
            {
                this.ChangeStatusBarColor(this.RootPage);

                _firstLoad = true;
            }
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
                this.BarTextColor = _customTitleView.TextColor = Material.Color.OnPrimary;
            }
            else
            {
                this.BarTextColor = _customTitleView.TextColor = barTextColor;
            }
        }

        private void ChangeFont(Page page)
        {
            var currentValue = page.GetValue(TitleViewProperty);

            if (currentValue != null)
            {
                return;
            }

            var textAlignment = (TextAlignment)page.GetValue(AppBarTextAlignmentProperty);
            var fontFamily = (string)page.GetValue(AppBarTextFontFamilyProperty);
            var fontSize = (double)page.GetValue(AppBarTextFontSizeProperty);

            if (string.IsNullOrEmpty(fontFamily))
            {
                fontFamily = Material.FontFamily.H6;
            }

            _customTitleView = new Label();

            if (Device.RuntimePlatform == Device.iOS)
            {
                if (this.Navigation.NavigationStack.Count == 1)
                {
                    _customTitleView.Margin = new Thickness(8, 0, 8, 0);
                }
                else
                {
                    _customTitleView.Margin = new Thickness(8, 0, 32, 0);
                }
            }

            if (Device.RuntimePlatform == Device.Android && this.Navigation.NavigationStack.Count > 1 && page.ToolbarItems.Count == 0)
            {
                _customTitleView.Margin = new Thickness(0, 0, 72, 0);
            }

            _customTitleView.VerticalTextAlignment = TextAlignment.Center;
            _customTitleView.VerticalOptions = LayoutOptions.FillAndExpand;
            _customTitleView.HorizontalOptions = LayoutOptions.FillAndExpand;
            _customTitleView.HorizontalTextAlignment = textAlignment;
            _customTitleView.SetDynamicResource(StyleProperty, "Material.TypeScale.H6");
            _customTitleView.FontFamily = fontFamily;
            _customTitleView.Text = page.Title;
            _customTitleView.FontSize = fontSize;

            page.SetValue(TitleViewProperty, _customTitleView);
        }

        private void ChangeStatusBarColor(Page page)
        {
            var statusBarColor = (Color)page.GetValue(StatusBarColorProperty);
            Material.PlatformConfiguration.ChangeStatusBarColor(statusBarColor.IsDefault ? Material.Color.PrimaryVariant : statusBarColor);
        }
    }
}