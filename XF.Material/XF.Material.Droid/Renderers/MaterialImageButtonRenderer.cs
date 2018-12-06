using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using XF.Material.Droid.Renderers;
using XF.Material.Forms.UI;

[assembly: ExportRenderer(typeof(MaterialImageButton), typeof(MaterialImageButtonRenderer))]
namespace XF.Material.Droid.Renderers
{
    public class MaterialImageButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<MaterialImageButton, AppCompatImageButton>, Android.Views.View.IOnClickListener
    {
        public MaterialImageButtonRenderer(Context context) : base(context) { }

        public void OnClick(Android.Views.View v)
        {
            var displayDensity = this.Context.Resources.DisplayMetrics.Density;
            var position = new int[2];
            this.Control.GetLocationInWindow(position);
            this.OnClick(position[0] / displayDensity, position[1] / displayDensity);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<MaterialImageButton> e)
        {
            base.OnElementChanged(e);


            if (this.Control == null)
            {
                this.SetNativeControl(new AppCompatImageButton(this.Context));
                this.Control.SetOnClickListener(this);
            }

            if(e?.NewElement != null)
            {
                this.SetDrawable();
            }
        }

        protected virtual void OnClick(double x, double y)
        {
            this.Element.OnClick();
        }

        protected Drawable GetDrawable()
        {
            var fileName = this.Element.Source.File.Split('.').FirstOrDefault();
            var id = this.Resources.GetIdentifier(fileName, "drawable", Material.Context.PackageName);

            return MaterialHelper.GetDrawableCopyFromResource(id);
        }

        protected virtual void SetDrawable()
        {
            using (var drawable = this.GetDrawable())
            {
                this.Control.SetImageDrawable(drawable);
            }
        }
    }
}