using Android.Graphics;
using Android.Support.V4.View;
using System;

namespace XF.Material.Droid.Renderers
{
    public static class MaterialExtensions
    {
        public static float ConvertDpToPx(this int dp)
        {
            return dp * Material.Context.Resources.DisplayMetrics.Density;
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
    }
}