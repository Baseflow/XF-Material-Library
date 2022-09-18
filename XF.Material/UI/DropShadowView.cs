using Microsoft.Maui;

namespace XF.Material.Maui.UI
{
    public class DropShadowView : View
    {
        public static readonly BindableProperty OffsetYProperty
          = BindableProperty.Create(nameof(OffsetY), typeof(double), typeof(DropShadowView), 2.0);

        public double OffsetY
        {
            get { return (double)GetValue(OffsetYProperty); }
            set { SetValue(OffsetYProperty, value); }
        }

        public static readonly BindableProperty OffsetXProperty
            = BindableProperty.Create(nameof(OffsetX), typeof(double), typeof(DropShadowView), 0.0);

        public double OffsetX
        {
            get { return (double)GetValue(OffsetXProperty); }
            set { SetValue(OffsetXProperty, value); }
        }

        public static readonly BindableProperty BlurRadiusProperty
            = BindableProperty.Create(nameof(BlurRadius), typeof(double), typeof(DropShadowView), 36.0);

        public double BlurRadius
        {
            get { return (double)GetValue(BlurRadiusProperty); }
            set { SetValue(BlurRadiusProperty, value); }
        }

        public static readonly BindableProperty CornerRadiusProperty
            = BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(DropShadowView), 4.0);

        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly BindableProperty SurfaceColorProperty
         = BindableProperty.Create(nameof(SurfaceColor), typeof(Color), typeof(DropShadowView), default(Color));

        public Color SurfaceColor
        {
            get { return (Color)GetValue(SurfaceColorProperty); }
            set { SetValue(SurfaceColorProperty, value); }
        }
    }
}
