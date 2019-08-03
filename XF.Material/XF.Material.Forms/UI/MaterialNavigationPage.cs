﻿using System.ComponentModel;
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
        /// Attached property that is used by <see cref="Page"/>s to determine whether the app bar will draw a shadow.
        /// </summary>
        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create("HasShadow", typeof(bool), typeof(MaterialNavigationPage), true);

        /// <summary>
        /// Attached property that is used by <see cref="Page"/>s to determine the status bar color.
        /// </summary>
        public static readonly BindableProperty StatusBarColorProperty = BindableProperty.Create("StatusBarColor", typeof(Color), typeof(MaterialNavigationPage), Color.Default);

        private TitleLabel _customTitleView;
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
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void InternalPagePop(Page previousPage, Page poppedPage)
        {
            this.OnPagePop(previousPage, poppedPage);
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void InternalPagePush(Page page)
        {
            this.OnPagePush(page);
        }

        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanging(propertyName);

            if (propertyName == nameof(this.CurrentPage) && this.CurrentPage != null)
            {
                this.CurrentPage.PropertyChanged -= this.Page_PropertyChanged;
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if(propertyName == nameof(this.CurrentPage) && this.CurrentPage != null)
            {
                this.CurrentPage.PropertyChanged += this.Page_PropertyChanged;
            }
        }

        private void Page_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var page = sender as Page;

            if (e?.PropertyName == nameof(this.Title) && page?.GetValue(TitleViewProperty) is TitleLabel label)
            {
                label.Text = page.Title;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_firstLoad) return;
            ChangeStatusBarColor(this.RootPage);

            _firstLoad = true;
        }

        /// <summary>
        /// Called when a page is being popped.
        /// </summary>
        /// <param name="previousPage">The page that will re-appear.</param>
        /// <param name="poppedPage">The page that will be popped.</param>
        protected virtual void OnPagePop(Page previousPage, Page poppedPage)
        {
            if (previousPage.BackgroundColor.IsDefault)
            {
                previousPage.SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.BACKGROUND);
            }

            this.ChangeBarTextColor(previousPage);
            ChangeStatusBarColor(previousPage);
            this.ChangeBarBackgroundColor(previousPage);

            previousPage.SetValue(BackButtonTitleProperty, string.Empty);
        }

        /// <summary>
        /// Called when a page is being pushed.
        /// </summary>
        /// <param name="page">The page that is being pushed.</param>
        protected virtual void OnPagePush(Page page)
        {
            if (page.BackgroundColor.IsDefault)
            {
                page.SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.BACKGROUND);
            }

            this.ChangeFont(page);
            this.ChangeBarTextColor(page);
            ChangeStatusBarColor(page);
            this.ChangeBarBackgroundColor(page);

            page.SetValue(BackButtonTitleProperty, string.Empty);
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
            var barTextColor = (Color)page.GetValue(AppBarTitleTextColorProperty);

            if (page.GetValue(TitleViewProperty) is TitleLabel customTitleView)
            {
                if (barTextColor.IsDefault)
                {
                    this.BarTextColor = customTitleView.TextColor = Material.Color.OnPrimary;
                }
                else
                {
                    this.BarTextColor = customTitleView.TextColor = barTextColor;
                }
            }
            else
            {
                this.BarTextColor = barTextColor.IsDefault ? Material.Color.OnPrimary : barTextColor;
            }
        }

        private void ChangeFont(Page page)
        {
            var currentValue = page.GetValue(TitleViewProperty);

            if (currentValue != null)
            {
                return;
            }

            var textAlignment = (TextAlignment)page.GetValue(AppBarTitleTextAlignmentProperty);
            var fontFamily = (string)page.GetValue(AppBarTitleTextFontFamilyProperty);
            var fontSize = (double)page.GetValue(AppBarTitleTextFontSizeProperty);

            if (string.IsNullOrEmpty(fontFamily))
            {
                fontFamily = Material.FontFamily.H6;
            }

            _customTitleView = new TitleLabel();

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    _customTitleView.Margin = this.Navigation.NavigationStack.Count == 1 ? new Thickness(8, 0, 8, 0) : new Thickness(8, 0, 32, 0);
                    break;
                case Device.Android when this.Navigation.NavigationStack.Count > 1 && page.ToolbarItems.Count == 0:
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

        private static void ChangeStatusBarColor(Page page)
        {
            var statusBarColor = (Color)page.GetValue(StatusBarColorProperty);
            Material.PlatformConfiguration.ChangeStatusBarColor(statusBarColor.IsDefault ? Material.Color.PrimaryVariant : statusBarColor);
        }
    }

    internal class TitleLabel : MaterialLabel { }
}