using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
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

            if (e?.NewElement != null)
            {
                this.SetDrawable();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e?.PropertyName == nameof(VisualElement.Width) && this.Element.Width > 0)
            {
                this.SetImage(this.Element.Width / 2);
            }
        }

        private ColorStateList GetColorStates(Android.Graphics.Color activeColor)
        {
            var states = new int[][]
            {
                new int[] { Android.Resource.Attribute.StatePressed },
                new int[] { Android.Resource.Attribute.StateFocused, Android.Resource.Attribute.StateEnabled },
                new int[] { Android.Resource.Attribute.StateEnabled },
                new int[] { Android.Resource.Attribute.StateFocused },
                new int[] { }
            };

            var colors = new int[]
            {
                activeColor,
                activeColor,
                activeColor,
                activeColor,
                activeColor.ToColor().MultiplyAlpha(0.33).ToAndroid()
             };

            return new ColorStateList(states, colors);
        }

        private void SetDrawable()
        {
            if (Material.IsLollipop)
            {
                using (var drawable = MaterialHelper.GetDrawableCopyFromResource<RippleDrawable>(Resource.Drawable.drawable_ripple_image))
                {
                    drawable.SetColor(this.GetColorStates(this.Element.RippleColor.ToAndroid()));

                    this.Control.SetBackground(drawable);
                }
            }
        }

        private void SetImage(double imageSize)
        {
            var fileName = this.Element.Source.File.Split('.').FirstOrDefault();
            var id = this.Resources.GetIdentifier(fileName, "drawable", Material.Context.PackageName);
            var width = (int)MaterialHelper.ConvertToDp(imageSize);
            var height = (int)MaterialHelper.ConvertToDp(imageSize);

            using (var drawable = MaterialHelper.GetDrawableCopyFromResource(id))
            {
                drawable.SetBounds(0, 0, width, height);
                drawable.SetTint(this.Element.TintColor.ToAndroid());
                drawable.SetTintList(this.GetColorStates(this.Element.TintColor.ToAndroid()));

                this.Control.SetImageDrawable(drawable);
                this.Control.LayoutParameters = new LayoutParams((int)MaterialHelper.ConvertToDp(this.Element.Width), (int)MaterialHelper.ConvertToDp(this.Element.Height));
            }
        }
    }
}