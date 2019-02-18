using CoreGraphics;
using System.Linq;
using System.Threading.Tasks;
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

        public void ChangeHasShadow(bool hasShadow)
        {
            if (hasShadow)
            {
                this.NavigationBar.Layer.MasksToBounds = false;
                this.NavigationBar.Layer.ShadowColor = UIColor.Black.CGColor;
                this.NavigationBar.Layer.ShadowOffset = new CGSize(0, 3f);
                this.NavigationBar.Layer.ShadowOpacity = 0.32f;
                this.NavigationBar.Layer.ShadowRadius = 3f;
            }

            else
            {
                this.NavigationBar.Layer.MasksToBounds = false;
                this.NavigationBar.Layer.ShadowColor = UIColor.Black.CGColor;
                this.NavigationBar.Layer.ShadowOffset = new CGSize(0f, 0f);
                this.NavigationBar.Layer.ShadowOpacity = 0f;
                this.NavigationBar.Layer.ShadowRadius = 0f;
            }
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _navigationPage = this.Element as MaterialNavigationPage;
            }
        }

        protected override Task<bool> OnPopViewAsync(Page page, bool animated)
        {
            var pop = base.OnPopViewAsync(page, animated);
            var navStack = _navigationPage.Navigation.NavigationStack.ToList();

            if (navStack.Count - 1 - navStack.IndexOf(_navigationPage.CurrentPage) < 0) return pop;
            var previousPage = navStack[navStack.IndexOf(_navigationPage.CurrentPage) - 1];
            _navigationPage.InternalPagePop(previousPage, page);
            this.ChangeHasShadow(previousPage);

            return pop;
        }

        protected override Task<bool> OnPushAsync(Page page, bool animated)
        {
            _navigationPage.InternalPagePush(page);

            this.ChangeHasShadow(page);

            return base.OnPushAsync(page, animated);
        }

        private void ChangeHasShadow(Page page)
        {
            var hasShadow = (bool)page.GetValue(MaterialNavigationPage.HasShadowProperty);

            this.ChangeHasShadow(hasShadow);
        }
    }
}