using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Support.V4.Graphics;
using Android.Support.V4.View;
using Android.Util;
using Android.Views.InputMethods;
using Android.Widget;
using System;

namespace XF.Material.Droid
{
    internal static class MaterialHelper
    {
        private static DisplayMetrics _displayMetrics => Material.Context.Resources.DisplayMetrics;

        internal static float ConvertToDp(double value)
        {
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)value, _displayMetrics);
        }

        internal static float ConvertToSp(double value)
        {
            return TypedValue.ApplyDimension(ComplexUnitType.Sp, (float)value, _displayMetrics);
        }

        internal static Color DarkenColor(this Color color)
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

        internal static void Elevate(this Android.Views.View view, int elevation)
        {
            var elevationInDp = ConvertToDp(elevation);
            ViewCompat.SetElevation(view, elevationInDp);
        }

        internal static Drawable GetDrawableCopy(this Drawable drawable)
        {
            return drawable?.GetConstantState().NewDrawable().Mutate();
        }

        internal static Drawable GetDrawableCopyFromResource(int resId)
        {
            return ContextCompat.GetDrawable(Material.Context, resId).GetConstantState().NewDrawable().Mutate();
        }

        internal static TDrawable GetDrawableCopyFromResource<TDrawable>(int resId) where TDrawable : Drawable
        {
            return ContextCompat.GetDrawable(Material.Context, resId).GetConstantState().NewDrawable().Mutate() as TDrawable;
        }

        internal static bool IsColorDark(this Color color)
        {
            return ColorUtils.CalculateLuminance(color) < 0.5;
        }

        internal static void ShowKeyboard(this EditText edittext)
        {
            var im = (Material.Context as Activity).GetSystemService(Context.InputMethodService) as InputMethodManager;
            im.ShowSoftInput(edittext, ShowFlags.Forced);
            im.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
        }
    }
}