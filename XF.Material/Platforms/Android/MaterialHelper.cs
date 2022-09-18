﻿using System;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using AndroidX.Core.Content;
using AndroidX.Core.Graphics;
using AndroidX.Core.Graphics.Drawable;
using AndroidX.Core.View;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Platform;
using Color = Android.Graphics.Color;

namespace XF.Material.Droid
{
    internal static class MaterialHelper
    {
        private static DisplayMetrics DisplayMetrics => Material.Context.Resources.DisplayMetrics;

        internal static float ConvertDpToPx(double value)
        {
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)value, DisplayMetrics);
        }

        internal static float ConvertSpToPx(double value)
        {
            return TypedValue.ApplyDimension(ComplexUnitType.Sp, (float)value, DisplayMetrics);
        }

        internal static Color DarkenColor(this Color color)
        {
            const float factor = 0.75f;
            var a = Color.GetAlphaComponent(color);
            var r = Convert.ToInt32(Math.Round(Color.GetRedComponent(color) * factor));
            var g = Convert.ToInt32(Math.Round(Color.GetGreenComponent(color) * factor));
            var b = Convert.ToInt32(Math.Round(Color.GetBlueComponent(color) * factor));
            return Color.Argb(a,
                    Math.Min(r, 255),
                    Math.Min(g, 255),
                    Math.Min(b, 255));
        }

        internal static Color GetDisabledColor(this Color color)
        {
            const float disabledOpacity = 0.38f;
            var r = Color.GetRedComponent(color);
            var g = Color.GetGreenComponent(color);
            var b = Color.GetBlueComponent(color);
            return Color.Argb(Convert.ToInt32(Math.Round(Color.GetAlphaComponent(color) * disabledOpacity)), r, g, b);
        }

        internal static void Elevate(this Android.Views.View view, double elevation)
        {
            var elevationInPixels = ConvertDpToPx(elevation);
            ViewCompat.SetElevation(view, elevationInPixels);
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

        private static ColorStateList GetColorStates(Color activeColor)
        {
            var states = new[]
            {
                new[] { Android.Resource.Attribute.StatePressed },
                new[] { Android.Resource.Attribute.StateFocused, Android.Resource.Attribute.StateEnabled },
                new[] { Android.Resource.Attribute.StateEnabled },
                new[] { Android.Resource.Attribute.StateFocused },
                new int[] { }
            };

            var colors = new int[]
            {
                activeColor,
                activeColor,
                activeColor,
                activeColor,
                activeColor.ToColor().MultiplyAlpha((float)0.38).ToAndroid()
             };

            return new ColorStateList(states, colors);
        }
    }
}
