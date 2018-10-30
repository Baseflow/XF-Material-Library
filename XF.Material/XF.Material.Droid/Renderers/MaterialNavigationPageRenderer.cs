using System.Threading.Tasks;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Forms.Views;
using System.Linq;
using XF.Material.Forms.Views.Internals;
using Toolbar = Android.Support.V7.Widget.Toolbar;

[assembly: ExportRenderer(typeof(MaterialNavigationPage), typeof(MaterialNavigationPageRenderer))]
namespace XF.Material.Droid.Renderers
{
    public class MaterialNavigationPageRenderer : Xamarin.Forms.Platform.Android.AppCompat.NavigationPageRenderer
    {
        private MaterialNavigationPage _navigationPage;

        public MaterialNavigationPageRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _navigationPage = this.Element as MaterialNavigationPage;
                var toolbar = this.ViewGroup.GetChildAt(0) as Toolbar;

                if (_navigationPage.HasShadow)
                {
                    toolbar.Elevate(8);
                }
            }
        }

        protected override Task<bool> OnPushAsync(Page view, bool animated)
        {
            _navigationPage.OnPagePush(view);

            return base.OnPushAsync(view, animated);
        }

        protected override Task<bool> OnPopViewAsync(Page page, bool animated)
        {
            var navStack = _navigationPage.Navigation.NavigationStack.ToList();

            if(navStack.Count - 1 - navStack.IndexOf(page) >= 0)
            {
                var previousPage = navStack[navStack.IndexOf(page) - 1];
                _navigationPage.OnPagePop(previousPage);
            }


            return base.OnPopViewAsync(page, animated);
        }
    }
}