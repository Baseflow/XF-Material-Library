using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Support.V4.Graphics;
using Android.Support.V4.Graphics.Drawable;
using Android.Support.V4.View;
using Android.Util;
using Android.Views.InputMethods;
using Android.Widget;
using System;
using Xamarin.Forms.Platform.Android;

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

        internal static Color GetDisabledColor(this Color color)
        {
            const float disabledOpacity = 0.38f;
            int r = Color.GetRedComponent(color);
            int g = Color.GetGreenComponent(color);
            int b = Color.GetBlueComponent(color);
            return Color.Argb(Convert.ToInt32(Math.Round(Color.GetAlphaComponent(color) * disabledOpacity)), r, g, b);
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

        internal static void TintDrawable(this Drawable drawable, Color tintColor)
        {
            DrawableCompat.SetTint(drawable, tintColor);
            DrawableCompat.SetTintList(drawable, GetColorStates(tintColor));
        }

        private static ColorStateList GetColorStates(Android.Graphics.Color activeColor)
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
                activeColor.ToColor().MultiplyAlpha(0.38).ToAndroid()
             };

            return new ColorStateList(states, colors);
        }
    }
}