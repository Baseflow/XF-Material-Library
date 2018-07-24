using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Support.V4.Graphics;
using Android.Support.V4.View;
using Android.Util;
using System;

namespace XF.Material.Droid
{
    public static class MaterialHelper
    {
        private static DisplayMetrics _displayMetrics => Material.Context.Resources.DisplayMetrics;

        public static float ConvertToDp(double value)
        {
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)value, _displayMetrics);
        }

        public static float ConvertToSp(double value)
        {
            return TypedValue.ApplyDimension(ComplexUnitType.Sp, (float)value, _displayMetrics);
        }

        public static void Elevate(this Android.Views.View view, int elevation)
        {
            var elevationInDp = ConvertToDp(elevation);
            ViewCompat.SetElevation(view, elevationInDp);
        }

        public static Color DarkenColor(this Color color)
        {
            const float factor = 0.75f;
            int a = Color.GetAlphaComponent(color);
            int r = Convert.ToInt32(Math.Round(Color.GetRedComponent(color) * factor));
            int g = Convert.ToInt32(Math.Round(Color.GetGreenComponent(color) * factor));
            int b = Convert.ToInt32(Math.Round(Color.GetBlueComponent(color) * factor));
            return Color.Argb(a,
                    Math.Min(r, 255),
                    Math.Min(g, 255),
                    Math.Min(b, 255));
        }

        public static bool IsColorDark(this Color color)
        {
            return ColorUtils.CalculateLuminance(color) < 0.5;
        }

        public static TDrawable GetDrawableCopyFromResource<TDrawable>(int resId) where TDrawable : Drawable
        {
            return ContextCompat.GetDrawable(Material.Context, resId).GetConstantState().NewDrawable().Mutate() as TDrawable;
        }
    }
}