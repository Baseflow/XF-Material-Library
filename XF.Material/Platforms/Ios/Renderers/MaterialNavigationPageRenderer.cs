using System.Linq;
using System.Threading.Tasks;
using CoreGraphics;
using ObjCRuntime;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI;
using XF.Material.iOS.Renderers;

[assembly: ExportRenderer(typeof(MaterialNavigationPage), typeof(MaterialNavigationPageRenderer))]

namespace XF.Material.iOS.Renderers
{
    public class MaterialNavigationPageRenderer : NavigationRenderer
    {
        private MaterialNavigationPage _navigationPage;

        private void ChangeHasShadow(bool hasShadow)
        {
            if (NavigationBar == null || NavigationBar.Layer == null)
            {
                return;
            }

            if (hasShadow)
            {
                NavigationBar.Layer.MasksToBounds = false;
                NavigationBar.Layer.ShadowColor = UIColor.Black.CGColor;
                NavigationBar.Layer.ShadowOffset = new CGSize(0, 3f);
                NavigationBar.Layer.ShadowOpacity = 0.32f;
                NavigationBar.Layer.ShadowRadius = 3f;
            }
            else
            {
                NavigationBar.Layer.MasksToBounds = false;
                NavigationBar.Layer.ShadowColor = UIColor.Black.CGColor;
                NavigationBar.Layer.ShadowOffset = new CGSize(0f, 0f);
                NavigationBar.Layer.ShadowOpacity = 0f;
                NavigationBar.Layer.ShadowRadius = 0f;
            }
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _navigationPage = Element as MaterialNavigationPage;
                Delegate = new NavigationControllerDelegate(this, _navigationPage);
            }
        }

        public override UIViewController[] PopToRootViewController(bool animated)
        {
            _navigationPage.InternalPopToRoot(_navigationPage.RootPage);
            ChangeHasShadow(_navigationPage.RootPage);
            return base.PopToRootViewController(animated);
        }

        public override UIViewController PopViewController(bool animated)
        {
            var navStack = _navigationPage.Navigation.NavigationStack.ToList();

            if (navStack.Count - 1 - navStack.IndexOf(_navigationPage.CurrentPage) < 0)
            {
                return base.PopViewController(animated);
            }

            var currentPage = _navigationPage.CurrentPage;
            var previousPage = navStack[navStack.IndexOf(_navigationPage.CurrentPage) - 1];
            _navigationPage.InternalPagePop(previousPage, currentPage);
            ChangeHasShadow(previousPage);

            return base.PopViewController(animated);
        }

        protected override Task<bool> OnPushAsync(Page page, bool animated)
        {
            _navigationPage.InternalPagePush(page);

            ChangeHasShadow(page);

            return base.OnPushAsync(page, animated);
        }

        private void ChangeHasShadow(Page page)
        {
            var hasShadow = (bool)page.GetValue(MaterialNavigationPage.HasShadowProperty);

            ChangeHasShadow(hasShadow);
        }

        private class NavigationControllerDelegate : UINavigationControllerDelegate
        {
            private UINavigationController _navigationController;
            private MaterialNavigationPage _navigationPage;

            public NavigationControllerDelegate(UINavigationController uINavigationController, MaterialNavigationPage navigationPage)
            {
                _navigationController = uINavigationController;
                _navigationPage = navigationPage;
            }

            public override void WillShowViewController(UINavigationController navigationController, [Transient] UIViewController viewController, bool animated)
            {
                var coordinator = _navigationController?.TopViewController?.GetTransitionCoordinator();

                if (coordinator == null)
                {
                    return;
                }

                coordinator.NotifyWhenInteractionChanges((context) =>
                {
                    if (context.IsCancelled)
                    {
                        _navigationPage.ForceUpdateCurrentPage();
                    }
                });
            }
        }
    }
}