using CoreGraphics;
using System.Linq;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.Views;
using XF.Material.Forms.Views.Internals;
using XF.Material.iOS.Renderers;

[assembly: ExportRenderer(typeof(MaterialNavigationPage), typeof(MaterialNavigationPageRenderer))]
namespace XF.Material.iOS.Renderers
{
    public class MaterialNavigationPageRenderer : NavigationRenderer
    {
        private MaterialNavigationPage _navigationPage;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e?.OldElement != null)
            {
                (this.Element as IMaterialElementConfiguration)?.ElementChanged(false);
            }

            if (e?.NewElement != null)
            {
                _navigationPage = this.Element as MaterialNavigationPage;
                this.NavigationBar.Frame = new CGRect(0, 0, this.View.Frame.Size.Width, 56f);

                if (_navigationPage.HasShadow)
                {
                    this.NavigationBar.Layer.MasksToBounds = false;
                    this.NavigationBar.Layer.ShadowColor = UIColor.Black.CGColor;
                    this.NavigationBar.Layer.ShadowOffset = new CGSize(0, 3f);
                    this.NavigationBar.Layer.ShadowOpacity = 0.32f;
                    this.NavigationBar.Layer.ShadowRadius = 3f;
                }

                (this.Element as IMaterialElementConfiguration)?.ElementChanged(true);
            }
        }

        public override void PushViewController(UIViewController viewController, bool animated)
        {
            base.PushViewController(viewController, animated);

            viewController.NavigationItem.BackBarButtonItem = new UIBarButtonItem(string.Empty, UIBarButtonItemStyle.Plain, null);
        }

        public override UIViewController PopViewController(bool animated)
        {
            var navStack = _navigationPage.Navigation.NavigationStack.ToList();

            if (navStack.Count - 1 - navStack.IndexOf(_navigationPage.CurrentPage) >= 0)
            {
                var previousPage = navStack[navStack.IndexOf(_navigationPage.CurrentPage) - 1];
                _navigationPage.OnPagePop(previousPage);
            }

            return base.PopViewController(animated);
        }

        protected override Task<bool> OnPushAsync(Page page, bool animated)
        {
            _navigationPage.OnPagePush(page);

            return base.OnPushAsync(page, animated);
        }
    }
}