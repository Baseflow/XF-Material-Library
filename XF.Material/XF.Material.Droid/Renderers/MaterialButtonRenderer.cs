using Android.Animation;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid.Renderers;
using XF.Material.Views;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(MaterialButton), typeof(MaterialButtonRenderer))]
namespace XF.Material.Droid.Renderers
{
    public class MaterialButtonRenderer : ButtonRenderer
    {
        private MaterialButton _materialButton;

        public MaterialButtonRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _materialButton = this.Element as MaterialButton;
                this.CreateDrawable();
                this.Control.SetAllCaps(true);
            }
        }

        private void CreateDrawable()
        {
            var normalBackgroundColor = _materialButton.BackgroundColor != null ? _materialButton.BackgroundColor.ToAndroid() : ((Xamarin.Forms.Color)VisualElement.BackgroundColorProperty.DefaultValue).ToAndroid();
            var pressedBackgroundColor = this.DarkenColor(normalBackgroundColor);
            var cornerRadius = MaterialExtensions.ConvertDpToPx(_materialButton.CornerRadius);
            var normalStateShapeDrawable = new GradientDrawable();
            normalStateShapeDrawable.SetShape(ShapeType.Rectangle);
            normalStateShapeDrawable.SetCornerRadius(cornerRadius);
            normalStateShapeDrawable.SetStroke(Convert.ToInt32(_materialButton.BorderWidth), _materialButton.BorderColor.ToAndroid());
            normalStateShapeDrawable.SetColor(normalBackgroundColor);

            if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
            {
                var drawable = new StateListDrawable();
                var pressedStateShapeDrawable = new GradientDrawable();
                pressedStateShapeDrawable.SetShape(ShapeType.Rectangle);
                pressedStateShapeDrawable.SetCornerRadius(cornerRadius);
                pressedStateShapeDrawable.SetStroke(Convert.ToInt32(_materialButton.BorderWidth), _materialButton.BorderColor.ToAndroid());
                pressedStateShapeDrawable.SetColor(pressedBackgroundColor);
                drawable.AddState(new int[] { -Android.Resource.Attribute.StateEnabled }, normalStateShapeDrawable);
                drawable.AddState(new int[] { Android.Resource.Attribute.StatePressed }, pressedStateShapeDrawable);

                this.Control.Background = drawable;
            }

            else
            {
                var ripple = Xamarin.Forms.Color.FromRgba(255, 255, 255, 80).ToAndroid();
                var colorStateList = new Android.Content.Res.ColorStateList(new int[][] { new int[] { } }, new int[] { pressedBackgroundColor });
                this.Control.Background = new RippleDrawable(colorStateList, normalStateShapeDrawable, null);
            }
        }

        private Color DarkenColor(int color)
        {
            const float factor = 0.8f;
            int a = Color.GetAlphaComponent(color);
            int r = Convert.ToInt32(Math.Round(Color.GetRedComponent(color) * factor));
            int g = Convert.ToInt32(Math.Round(Color.GetGreenComponent(color) * factor));
            int b = Convert.ToInt32(Math.Round(Color.GetBlueComponent(color) * factor));
            return Color.Argb(a,
                    Math.Min(r, 255),
                    Math.Min(g, 255),
                    Math.Min(b, 255));
        }
    }

    public class MaterialButtonOutlineProvider : ViewOutlineProvider
    {
        private readonly float _cornerRadius;

        public MaterialButtonOutlineProvider(float cornerRadius)
        {
            _cornerRadius = cornerRadius;
        }

        public override void GetOutline(Android.Views.View view, Outline outline)
        {
            outline.SetRoundRect(0, 0, view.Width, view.Height, _cornerRadius);
        }
    }
}