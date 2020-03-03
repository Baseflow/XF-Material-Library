using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Forms.UI;
using Toolbar = Android.Support.V7.Widget.Toolbar;

[assembly: ExportRenderer(typeof(MaterialNavigationPage), typeof(MaterialNavigationPageRenderer))]

namespace XF.Material.Droid.Renderers
{
    public class MaterialNavigationPageRenderer : Xamarin.Forms.Platform.Android.AppCompat.NavigationPageRenderer
    {
        private MaterialNavigationPage _navigationPage;
        private Toolbar _toolbar;
        private Page _childPage;

        private Page ChildPage
        {
            set
            {
                if (_childPage == value)
                    return;

                if (_childPage != null)
                    _childPage.PropertyChanged -= ChildPage_PropertyChanged;

                _childPage = value;

                if (_childPage != null)
                    _childPage.PropertyChanged += ChildPage_PropertyChanged;
            }
        }

        public MaterialNavigationPageRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _navigationPage = Element as MaterialNavigationPage;

                _toolbar = ViewGroup.GetChildAt(0) as Toolbar;

                ChildPage = _navigationPage.CurrentPage;
            }

            if(e?.OldElement != null)
            {
                ChildPage = null;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == NavigationPage.CurrentPageProperty.PropertyName)
            {
                ChildPage = _navigationPage.CurrentPage;
            }
        }

        private void ChildPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!(sender is Page page))
                return;

            if (e.PropertyName == MaterialNavigationPage.AppBarElevationProperty.PropertyName)
            {
                ChangeElevation(page);
            }
        }

        protected override Task<bool> OnPopToRootAsync(Page page, bool animated)
        {
            _navigationPage.InternalPopToRoot(page);

            ChangeElevation(page);

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

            return base.OnPopViewAsync(page, animated);
        }

        protected override Task<bool> OnPushAsync(Page view, bool animated)
        {
            _navigationPage.InternalPagePush(view);

            ChangeElevation(view);

            return base.OnPushAsync(view, animated);
        }

        private void ChangeElevation(Page page)
        {
            var elevation = (double)page.GetValue(MaterialNavigationPage.AppBarElevationProperty);

            ChangeElevation(elevation);
        }

        public void ChangeElevation(double elevation)
        {
            if (elevation >= 0)
            {
                _toolbar.Elevate(elevation);
            }
            else
            {
                _toolbar.Elevate(0);
            }
        }
    }
}