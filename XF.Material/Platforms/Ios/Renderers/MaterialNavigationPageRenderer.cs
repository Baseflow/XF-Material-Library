using System;
using System.ComponentModel;
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
        private Page _child;

        private Page ChildPage
        {
            set
            {
                if (_child == value)
                    return;

                if (_child != null)
                    _child.PropertyChanged -= ChildPage_PropertyChanged;

                _child = value;

                if (_child != null)
                    _child.PropertyChanged += ChildPage_PropertyChanged;
            }
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _navigationPage = Element as MaterialNavigationPage;

                _navigationPage.PropertyChanged += MaterialNavigationPage_PropertyChanged;

                ChildPage = _navigationPage.CurrentPage;

                Delegate = new NavigationControllerDelegate(this, _navigationPage);
            }

            if (e?.OldElement != null)
            {
                _navigationPage.PropertyChanged -= MaterialNavigationPage_PropertyChanged;

                ChildPage = null;
            }
        }

        private void MaterialNavigationPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
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

        public override UIViewController[] PopToRootViewController(bool animated)
        {
            _navigationPage.InternalPopToRoot(_navigationPage.RootPage);
            ChangeElevation(_navigationPage.RootPage);
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
            ChangeElevation(previousPage);

            return base.PopViewController(animated);
        }

        protected override Task<bool> OnPushAsync(Page page, bool animated)
        {
            _navigationPage.InternalPagePush(page);

            ChangeElevation(page);

            return base.OnPushAsync(page, animated);
        }

        private void ChangeElevation(Page page)
        {
            var elevation = (double)page.GetValue(MaterialNavigationPage.AppBarElevationProperty);

            ChangeElevation(elevation);
        }

        private void ChangeElevation(double elevation)
        {
            if (NavigationBar == null || NavigationBar.Layer == null)
            {
                return;
            }

            if (elevation > 0)
            {
                NavigationBar.Layer.MasksToBounds = false;
                NavigationBar.Layer.ShadowColor = UIColor.Black.CGColor;
                NavigationBar.Layer.ShadowOffset = new CGSize(0, (nfloat)elevation);
                NavigationBar.Layer.ShadowOpacity = 0.24f;
                NavigationBar.Layer.ShadowRadius = (nfloat)elevation;
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

        class NavigationControllerDelegate : UINavigationControllerDelegate
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
                    return;

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