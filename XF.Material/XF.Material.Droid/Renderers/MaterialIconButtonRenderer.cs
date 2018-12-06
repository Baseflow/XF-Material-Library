using Android.Content;
using Android.Support.V7.Widget;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Forms.UI;

[assembly: ExportRenderer(typeof(MaterialIconButton), typeof(MaterialIconButtonRenderer))]

namespace XF.Material.Droid.Renderers
{
    public class MaterialIconButtonRenderer : ViewRenderer<MaterialIconButton, AppCompatImageButton>, Android.Views.View.IOnClickListener
    {
        private MaterialDrawableHelper _helper;

        public MaterialIconButtonRenderer(Context context) : base(context)
        {
        }

        public void OnClick(Android.Views.View v)
        {
            var displayDensity = this.Context.Resources.DisplayMetrics.Density;
            var position = new int[2];
            this.Control.GetLocationInWindow(position);
            this.OnClick(position[0] / displayDensity, position[1] / displayDensity);
        }

        protected virtual void OnClick(double x, double y)
        {
            this.Element.OnClick();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<MaterialIconButton> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null)
            {
                this.SetNativeControl(new AppCompatImageButton(this.Context));
                this.Control.SetOnClickListener(this);
            }

            if (e?.OldElement != null)
            {
                _helper?.Clean();
            }

            if (e?.NewElement != null)
            {
                _helper = new MaterialDrawableHelper(this.Element, this.Control);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if ((e?.PropertyName == nameof(VisualElement.Width) && this.Element.Width > 0) || e?.PropertyName == nameof(MaterialIconButton.Source))
            {
                this.SetImage();
            }
        }

        private void SetImage()
        {
            if (this.Element.Source == null || string.IsNullOrEmpty(this.Element.Source.File))
            {
                return;
            }

            var fileName = this.Element.Source.File.Split('.').FirstOrDefault();
            var id = this.Resources.GetIdentifier(fileName, "drawable", Material.Context.PackageName);

            using (var drawable = MaterialHelper.GetDrawableCopyFromResource(id))
            {
                if (!this.Element.TintColor.IsDefault)
                {
                    drawable.TintDrawable(this.Element.TintColor.ToAndroid());
                }

                this.Control.SetImageDrawable(drawable);
                this.Control.LayoutParameters = new LayoutParams((int)MaterialHelper.ConvertToDp(this.Element.Width), (int)MaterialHelper.ConvertToDp(this.Element.Height));

                _helper.UpdateDrawable();
            }
        }
    }
}