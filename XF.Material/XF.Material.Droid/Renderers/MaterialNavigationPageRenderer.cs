using Android.Animation;
using Android.Content;
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
        private double _lastWidth;
        private double _lastHeight;

        public MaterialNavigationPageRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);

            if(e?.OldElement == null)
            {
                //this.Element.SizeChanged -= this.Element_SizeChanged;
            }

            if(e?.NewElement != null)
            {
                //this.Element.SizeChanged += this.Element_SizeChanged;
                _navigationPage = this.Element as MaterialNavigationPage;
                _toolbar = this.ViewGroup.GetChildAt(0) as Toolbar;

                if (_navigationPage.HasShadow)
                {
                    _toolbar.Elevate(8);
                }
            }
        }

        //private void Element_SizeChanged(object sender, System.EventArgs e)
        //{
        //    if (_lastWidth != this.Element.Width || _lastHeight != this.Element.Width && _navigationPage.HasShadow)
        //    {
        //        _lastWidth = this.Element.Width;
        //        _lastHeight = this.Element.Width;

        //        this.AddElevation();
        //    }
        //}

        //private void AddElevation()
        //{
        //    var stateListAnimator = new StateListAnimator();
        //    stateListAnimator.AddState(new int[0], ObjectAnimator.OfFloat(_toolbar, "elevation", MaterialHelper.ConvertToDp(8)));

        //    _toolbar.StateListAnimator = stateListAnimator;
        //}
    }
}