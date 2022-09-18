using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using Android.Views;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android.AppCompat;
using Microsoft.Maui.Controls.Platform;
using XF.Material.Droid.Renderers;
using XF.Material.Maui.UI;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

[assembly: ExportRenderer(typeof(MaterialNavigationPage), typeof(MaterialNavigationPageRenderer))]
namespace XF.Material.Droid.Renderers
{
    public class MaterialNavigationPageRenderer : NavigationPageRenderer
    {
        private MaterialNavigationPage _navigationPage;
        private MultiPage<Page> _multiPageParent;
        private Toolbar _toolbar;
        private Page _childPage;

        public MaterialNavigationPageRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _navigationPage = Element as MaterialNavigationPage;

                // TODO: _toolbar = ViewGroup.GetChildAt(0) as Toolbar;

                HandleParent(_navigationPage.Parent);

                HandleChildPage(_navigationPage.CurrentPage);
            }

            if (e?.OldElement != null)
            {
                if (_childPage != null)
                {
                    _childPage.PropertyChanged -= ChildPage_PropertyChanged;
                }

                if (_multiPageParent != null)
                {
                    _multiPageParent.CurrentPageChanged -= MultiPageParent_CurrentPageChanged;
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == NavigationPage.CurrentPageProperty.PropertyName)
            {
                HandleChildPage(_navigationPage.CurrentPage);
            }
            else if (e.PropertyName == nameof(_navigationPage.Parent))
            {
                HandleParent(_navigationPage.Parent);
            }
        }

        private void HandleParent(Element parent)
        {
            if (parent is MultiPage<Page>)
            {
                if (_multiPageParent != null)
                {
                    _multiPageParent.CurrentPageChanged -= MultiPageParent_CurrentPageChanged;
                }

                _multiPageParent = parent as MultiPage<Page>;

                if (_multiPageParent != null)
                {
                    _multiPageParent.CurrentPageChanged += MultiPageParent_CurrentPageChanged;
                }
            }
            else if (_multiPageParent != null)
            {
                _multiPageParent.CurrentPageChanged -= MultiPageParent_CurrentPageChanged;

                _multiPageParent = null;
            }
        }

        private void MultiPageParent_CurrentPageChanged(object sender, EventArgs e)
        {
            var multiPage = sender as MultiPage<Page>;

            if (multiPage == null)
            {
                return;
            }

            if (multiPage.CurrentPage is NavigationPage navPage)
            {
                ChangeStatusBarColor(navPage.CurrentPage);
            }
            else
            {
                ChangeStatusBarColor(multiPage.CurrentPage);
            }
        }

        private void HandleChildPage(Page page)
        {
            if (_childPage != null)
            {
                _childPage.PropertyChanged -= ChildPage_PropertyChanged;
            }

            _childPage = page;

            if (_childPage != null)
            {
                _childPage.PropertyChanged += ChildPage_PropertyChanged;
            }
        }

        private void ChildPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var page = sender as Page;

            if (page == null)
            {
                return;
            }

            if (e.PropertyName == MaterialNavigationPage.AppBarElevationProperty.PropertyName)
            {
                ChangeElevation(page);
            }
            else if (e.PropertyName == MaterialNavigationPage.StatusBarColorProperty.PropertyName)
            {
                ChangeStatusBarColor(page);
            }
        }

        protected override Task<bool> OnPopToRootAsync(Page page, bool animated)
        {
            _navigationPage.InternalPopToRoot(page);

            ChangeElevation(page);

            ChangeStatusBarColor(page);

            return base.OnPopToRootAsync(page, animated);
        }

        protected override Task<bool> OnPopViewAsync(Page page, bool animated)
        {
            var navStack = _navigationPage.Navigation.NavigationStack.ToList();

            if (navStack.Count - 1 - navStack.IndexOf(page) < 0)
            {
                return base.OnPopViewAsync(page, animated);
            }

            var previousPage = navStack[navStack.IndexOf(page) - 1];

            _navigationPage.InternalPagePop(previousPage, page);

            ChangeElevation(previousPage);

            ChangeStatusBarColor(previousPage);

            return base.OnPopViewAsync(page, animated);
        }

        protected override Task<bool> OnPushAsync(Page page, bool animated)
        {
            _navigationPage.InternalPagePush(page);

            ChangeElevation(page);

            if (_navigationPage.Parent is MultiPage<Page> parent)
            {
                if (parent.CurrentPage == _navigationPage)
                {
                    ChangeStatusBarColor(page);
                }
            }
            else
            {
                ChangeStatusBarColor(page);
            }

            return base.OnPushAsync(page, animated);
        }

        private void ChangeElevation(Page page)
        {
            var elevation = (double)page.GetValue(MaterialNavigationPage.AppBarElevationProperty);

            ChangeElevation(elevation);
        }

        public void ChangeElevation(double elevation)
        {
            if (elevation > 0)
            {
                _toolbar.Elevate(elevation);
            }
            else
            {
                _toolbar.Elevate(0);
            }
        }

        private void ChangeStatusBarColor(Page page)
        {
            var statusBarColor = (Color)page.GetValue(MaterialNavigationPage.StatusBarColorProperty);

            Maui.Material.PlatformConfiguration.ChangeStatusBarColor(statusBarColor.IsDefault() ? Maui.Material.Color.PrimaryVariant : statusBarColor);
        }
    }
}
