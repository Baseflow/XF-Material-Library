using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.Content;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Views;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(MaterialButton), typeof(MaterialButtonRenderer))]
namespace XF.Material.Droid.Renderers
{
    public class MaterialButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        private MaterialButton _materialButton;
        private bool _isLollipop;

        public MaterialButtonRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _materialButton = this.Element as MaterialButton;
                _isLollipop = Build.VERSION.SdkInt < BuildVersionCodes.Lollipop;
                this.Element.HeightRequest = this.Element.MinimumHeightRequest = _isLollipop ? 36 : 44;
                this.Control.Background = _isLollipop ? this.CreateStateListDrawable() : this.CreateRippleDrawable();
                this.Control.SetAllCaps(true);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e?.PropertyName == nameof(Button.BackgroundColor))
            {
                this.Control.Background = _isLollipop ? this.CreateStateListDrawable() : this.CreateRippleDrawable();
            }

            if(e?.PropertyName == nameof(Button.CornerRadius))
            {
                this.Control.Background = _isLollipop ? this.CreateStateListDrawable() : this.CreateRippleDrawable();
            }
        }

        private Drawable CreateStateListDrawable()
        {
            var normalColor = _materialButton.BackgroundColor.ToAndroid();
            var pressedColor = normalColor.DarkenColor();
            var cornerRadius = MaterialExtensions.ConvertDpToPx(_materialButton.CornerRadius);

            var normalStateShapeDrawable = new GradientDrawable();
            normalStateShapeDrawable.SetShape(ShapeType.Rectangle);
            normalStateShapeDrawable.SetCornerRadius(cornerRadius);
            normalStateShapeDrawable.SetColor(normalColor);

            var pressedStateShapeDrawable = new GradientDrawable();
            pressedStateShapeDrawable.SetShape(ShapeType.Rectangle);
            pressedStateShapeDrawable.SetCornerRadius(cornerRadius);
            pressedStateShapeDrawable.SetColor(pressedColor);

            var stateListDrawable = new StateListDrawable();
            stateListDrawable.AddState(new int[] { -Android.Resource.Attribute.StateEnabled }, normalStateShapeDrawable);
            stateListDrawable.AddState(new int[] { Android.Resource.Attribute.StatePressed }, pressedStateShapeDrawable);

            return stateListDrawable;
        }

        private Drawable CreateRippleDrawable()
        {
            var normalColor = _materialButton.BackgroundColor.ToAndroid();
            var cornerRadius = MaterialExtensions.ConvertDpToPx(_materialButton.CornerRadius);
            var rippleDrawable = ContextCompat.GetDrawable(Context, Resource.Drawable.drawable_ripple) as RippleDrawable;
            var insetDrawable = rippleDrawable.FindDrawableByLayerId(Resource.Id.inset_drawable) as InsetDrawable;
            var gradientDrawable = insetDrawable.Drawable as GradientDrawable;
            gradientDrawable.SetCornerRadius(cornerRadius);
            gradientDrawable.SetColor(normalColor);

            return rippleDrawable;
        }
    }

}