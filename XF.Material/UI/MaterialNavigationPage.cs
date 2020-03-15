using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        /// Attached property that is used by <see cref="Page"/>s to determine the app bar color.
        /// </summary>
        public static readonly BindableProperty AppBarColorProperty = BindableProperty.Create("AppBarColor", typeof(Color), typeof(MaterialNavigationPage), Color.Default);

        /// <summary>
        /// Attached property that is used by <see cref="Page"/>s to determine the app bar text alignment.
        /// </summary>
        public static readonly BindableProperty AppBarTitleTextAlignmentProperty = BindableProperty.Create("AppBarTitleTextAlignment", typeof(TextAlignment), typeof(MaterialNavigationPage), TextAlignment.Start);

        /// <summary>
        /// Attached property that is used by <see cref="Page"/>s to determine the app bar title text color.
        /// </summary>
        public static readonly BindableProperty AppBarTitleTextColorProperty = BindableProperty.Create("AppBarTitleTextColor", typeof(Color), typeof(MaterialNavigationPage), Color.Default);

        /// <summary>
        /// Attached property that is used by <see cref="Page"/>s to determine the app bar title text font family.
        /// </summary>
        public static readonly BindableProperty AppBarTitleTextFontFamilyProperty = BindableProperty.Create("AppBarTitleTextFontFamily", typeof(string), typeof(MaterialNavigationPage));

        /// <summary>
        /// Attached property that is used by <see cref="Page"/>s to determine the app bar title text font family.
        /// </summary>
        public static readonly BindableProperty AppBarTitleTextFontSizeProperty = BindableProperty.Create("AppBarTitleTextFontSize", typeof(double), typeof(MaterialNavigationPage), 24.0);

        /// <summary>
        /// Attached property that is used by <see cref="Page"/>s to determine the size of the shadow below the app bar.
        /// </summary>
        public static readonly BindableProperty AppBarElevationProperty = BindableProperty.Create("AppBarElevation", typeof(double), typeof(MaterialNavigationPage), 4.0);

        /// <summary>
        /// Attached property that is used by <see cref="Page"/>s to determine the status bar color.
        /// </summary>
        public static readonly BindableProperty StatusBarColorProperty = BindableProperty.Create("StatusBarColor", typeof(Color), typeof(MaterialNavigationPage), Color.Default);

        private TitleLabel _customTitleView;

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

            ChangeFont(rootPage);
            ChangeBarTextColor(rootPage);
            ChangeBarBackgroundColor(rootPage);
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
        public static TextAlignment GetAppBarTitleTextAlignment(BindableObject view)
        {
            return (TextAlignment)view.GetValue(AppBarTitleTextAlignmentProperty);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static Color GetAppBarTitleTextColor(BindableObject view)
        {
            return (Color)view.GetValue(AppBarTitleTextColorProperty);
        }

        public static string GetAppBarTitleTextFontFamily(BindableObject view)
        {
            return (string)view.GetValue(AppBarTitleTextFontFamilyProperty);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static double GetAppBarTitleTextFontSize(BindableObject view)
        {
            return (double)view.GetValue(AppBarTitleTextFontSizeProperty);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static double GetAppBarElevation(BindableObject view)
        {
            return (double)view.GetValue(AppBarElevationProperty);
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
        public static void SetAppBarTitleTextAlignment(BindableObject view, TextAlignment textAlignment)
        {
            view.SetValue(AppBarTitleTextAlignmentProperty, textAlignment);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static void SetAppBarTitleTextColor(BindableObject view, Color color)
        {
            view.SetValue(AppBarTitleTextColorProperty, color);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static void SetAppBarTitleTextFontFamily(BindableObject view, string fontFamily)
        {
            view.SetValue(AppBarTitleTextFontFamilyProperty, fontFamily);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static void SetAppBarTitleTextFontSize(BindableObject view, double value)
        {
            view.SetValue(AppBarTitleTextFontSizeProperty, value);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static void SetAppBarElevation(BindableObject view, double elevation)
        {
            view.SetValue(AppBarElevationProperty, elevation);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static void SetStatusBarColor(BindableObject view, Color color)
        {
            view.SetValue(StatusBarColorProperty, color);
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void InternalPopToRoot(Page rootPage)
        {
            OnPopToRoot(rootPage);
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void InternalPagePop(Page previousPage, Page poppedPage)
        {
            OnPagePop(previousPage, poppedPage);
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void InternalPagePush(Page page)
        {
            OnPagePush(page);
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ForceUpdateCurrentPage()
        {
            UpdatePage(CurrentPage);
        }

        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanging(propertyName);

            if (propertyName == nameof(CurrentPage) && CurrentPage != null)
            {
                CurrentPage.PropertyChanged -= Page_PropertyChanged;
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(CurrentPage) && CurrentPage != null)
            {
                CurrentPage.PropertyChanged += Page_PropertyChanged;
            }
        }

        private void Page_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var page = sender as Page;

            if (page == null)
            {
                return;
            }

            if (e.PropertyName == nameof(Title) && page.GetValue(TitleViewProperty) is TitleLabel label)
            {
                label.Text = page.Title;
            }
            else if (e.PropertyName == AppBarColorProperty.PropertyName)
            {
                ChangeBarBackgroundColor(page);
            }
            else if (e.PropertyName == AppBarTitleTextColorProperty.PropertyName)
            {
                ChangeBarTextColor(page);
            }
            else if (e.PropertyName == AppBarTitleTextFontFamilyProperty.PropertyName)
            {
                ChangeFont(page);
            }
            else if (e.PropertyName == AppBarTitleTextFontSizeProperty.PropertyName)
            {
                ChangeFont(page);
            }
            else if (e.PropertyName == AppBarTitleTextAlignmentProperty.PropertyName)
            {
                ChangeFont(page);
            }
        }

        /// <summary>
        /// Called when all pages are being popped.
        /// </summary>
        /// <param name="rootPage">The root page.</param>
        protected virtual void OnPopToRoot(Page rootPage)
        {
            UpdatePage(rootPage);

            rootPage.SetValue(BackButtonTitleProperty, string.Empty);
        }

        /// <summary>
        /// Called when a page is being popped.
        /// </summary>
        /// <param name="previousPage">The page that will re-appear.</param>
        /// <param name="poppedPage">The page that will be popped.</param>
        protected virtual void OnPagePop(Page previousPage, Page poppedPage)
        {
            UpdatePage(previousPage);

            previousPage.SetValue(BackButtonTitleProperty, string.Empty);
        }

        /// <summary>
        /// Called when a page is being pushed.
        /// </summary>
        /// <param name="page">The page that is being pushed.</param>
        protected virtual void OnPagePush(Page page)
        {
            UpdatePage(page);

            page.SetValue(BackButtonTitleProperty, string.Empty);
        }

        private void UpdatePage(Page page)
        {
            if (page.BackgroundColor.IsDefault)
            {
                page.SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.BACKGROUND);
            }

            ChangeFont(page);
            ChangeBarTextColor(page);
            ChangeBarBackgroundColor(page);
        }

        private void ChangeBarBackgroundColor(Page page)
        {
            var barColor = (Color)page.GetValue(AppBarColorProperty);

            if (barColor.IsDefault)
            {
                SetDynamicResource(BarBackgroundColorProperty, MaterialConstants.Color.PRIMARY);
            }
            else
            {
                BarBackgroundColor = barColor;
            }
        }

        private void ChangeBarTextColor(Page page)
        {
            var barTextColor = (Color)page.GetValue(AppBarTitleTextColorProperty);

            if (page.GetValue(TitleViewProperty) is TitleLabel customTitleView)
            {
                if (barTextColor.IsDefault)
                {
                    BarTextColor = customTitleView.TextColor = Material.Color.OnPrimary;
                }
                else
                {
                    BarTextColor = customTitleView.TextColor = barTextColor;
                }
            }
            else
            {
                BarTextColor = barTextColor.IsDefault ? Material.Color.OnPrimary : barTextColor;
            }
        }

        private void ChangeFont(Page page)
        {
            var currentTitleView = page.GetValue(TitleViewProperty);

            var textAlignment = (TextAlignment)page.GetValue(AppBarTitleTextAlignmentProperty);
            var fontFamily = (string)page.GetValue(AppBarTitleTextFontFamilyProperty);
            var fontSize = (double)page.GetValue(AppBarTitleTextFontSizeProperty);

            if (currentTitleView != null)
            {
                if (currentTitleView is TitleLabel titleLabelView)
                {
                    titleLabelView.HorizontalTextAlignment = textAlignment;
                    titleLabelView.FontFamily = fontFamily;
                    titleLabelView.FontSize = fontSize;
                }
                return;
            }

            if (string.IsNullOrEmpty(fontFamily))
            {
                fontFamily = Material.FontFamily.H6;
            }

            _customTitleView = new TitleLabel();

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    _customTitleView.Margin = Navigation.NavigationStack.Count == 1 ? new Thickness(8, 0, 8, 0) : new Thickness(8, 0, 32, 0);
                    break;
                case Device.Android when Navigation.NavigationStack.Count > 1 && page.ToolbarItems.Count == 0:
                    _customTitleView.Margin = new Thickness(0, 0, 72, 0);
                    break;
            }

            page.SetValue(TitleViewProperty, _customTitleView);

            _customTitleView.VerticalTextAlignment = TextAlignment.Center;
            _customTitleView.VerticalOptions = LayoutOptions.FillAndExpand;
            _customTitleView.HorizontalOptions = LayoutOptions.FillAndExpand;
            _customTitleView.HorizontalTextAlignment = textAlignment;
            _customTitleView.TypeScale = Forms.Resources.Typography.MaterialTypeScale.H6;
            _customTitleView.FontFamily = fontFamily;
            _customTitleView.FontSize = fontSize;
            _customTitleView.Text = page.Title;
        }
    }

    internal class TitleLabel : MaterialLabel { }
}
