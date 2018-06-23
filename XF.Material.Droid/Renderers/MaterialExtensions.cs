using Android.Support.V4.View;

namespace XF.Material.Droid.Renderers
{
    public static class MaterialExtensions
    {
        public static float ConvertDpToPx(this int dp)
        {
            return dp * Android.App.Application.Context.Resources.DisplayMetrics.Density;
        }

        public static void Elevate(this Android.Views.View view, int elevation)
        {
            ViewCompat.SetElevation(view, ConvertDpToPx(elevation));
        }
    }
}