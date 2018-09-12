using System.ComponentModel;
using Android.Content;
using Android.Graphics;
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

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);

            if(e?.NewElement != null)
            {
                _navigationPage = this.Element as MaterialNavigationPage;
                _toolbar = this.ViewGroup.GetChildAt(0) as Toolbar;

                if(_navigationPage.HasShadow)
                {
                    _toolbar.Elevate(8);
                }
            }
        }
    }
}