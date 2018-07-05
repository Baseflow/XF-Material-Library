using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Support.V4.Graphics;
using Android.Support.V4.View;
using Android.Util;
using System;

namespace XF.Material.Droid
{
    public static class MaterialUtilities
    {
        private static DisplayMetrics _displayMetrics => Material.Context.Resources.DisplayMetrics;

        public static float ConvertDpToPx(double dp)
        {
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)dp, _displayMetrics);
        }

        public static float ConvertToSp(double value)
        {
            var dp = ConvertDpToPx(value);
            return dp / _displayMetrics.ScaledDensity;
        }

        public static void Elevate(this Android.Views.View view, int elevation)
        {
            ViewCompat.SetElevation(view, ConvertDpToPx(elevation));
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