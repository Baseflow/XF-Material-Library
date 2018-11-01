using Android.Content;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Forms.Views;
using Toolbar = Android.Support.V7.Widget.Toolbar;

[assembly: ExportRenderer(typeof(MaterialNavigationPage), typeof(MaterialNavigationPageRenderer))]

namespace XF.Material.Droid.Renderers
{
    public class MaterialNavigationPageRenderer : Xamarin.Forms.Platform.Android.AppCompat.NavigationPageRenderer
    {
        private MaterialNavigationPage _navigationPage;
        private Toolbar _toolbar;

        public MaterialNavigationPageRenderer(Context context) : base(context) { }

        public void ChangeHasShadow(bool hasShadow)
        {
            if(hasShadow)
            {
                _toolbar.Elevate(8);
            }

            else
            {
                _toolbar.Elevate(0);
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _navigationPage = this.Element as MaterialNavigationPage;
                _toolbar = this.ViewGroup.GetChildAt(0) as Toolbar;
            }
        }

        protected override Task<bool> OnPopViewAsync(Page page, bool animated)
        {
            var navStack = _navigationPage.Navigation.NavigationStack.ToList();

            if (navStack.Count - 1 - navStack.IndexOf(page) >= 0)
            {
                var previousPage = navStack[navStack.IndexOf(page) - 1];
                _navigationPage.OnPagePop(previousPage);
                this.ChangeHasShadow(previousPage);
            }

            return base.OnPopViewAsync(page, animated);
        }

        protected override Task<bool> OnPushAsync(Page view, bool animated)
        {
            _navigationPage.OnPagePush(view);

            this.ChangeHasShadow(view);

            return base.OnPushAsync(view, animated);
        }

        private void ChangeHasShadow(Page page)
        {
            var hasShadow = (bool)page.GetValue(MaterialNavigationPage.HasShadowProperty);

            this.ChangeHasShadow(hasShadow);
        }
    }
}