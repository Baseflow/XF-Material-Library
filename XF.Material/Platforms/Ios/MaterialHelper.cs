using System;
using CoreGraphics;
using UIKit;

namespace XF.Material.iOS
{
    internal static class MaterialHelper
    {
        internal static UIColor BlendColor(UIColor color1, UIColor color2, float alpha)
        {
            alpha = Math.Min(1f, Math.Max(0f, alpha));
            color1.GetRGBA(out var r1, out var g1, out var b1, out var a1);
            color2.GetRGBA(out var r2, out var g2, out var b2, out var a2);
            var r = (nfloat)Math.Min(r1 + r2, 1);
            var g = (nfloat)Math.Min(g1 + g2, 1);
            var b = (nfloat)Math.Min(b1 + b2, 1);

            return new UIColor(r, g, b, alpha);
        }

        internal static UIColor DarkenColor(this UIColor color)
        {
            color.GetRGBA(out var red, out var green, out var blue, out var alpha);

            return UIColor.FromRGBA((float)Math.Max(red - 0.2, 0), (float)Math.Max(green - 0.2, 0), (float)Math.Max(blue - 0.2, 0), alpha);
        }

        internal static void Elevate(this UIView view, int elevation)
        {
            view.Layer.MasksToBounds = false;
            view.Layer.ShadowColor = UIColor.Black.CGColor;
            view.Layer.ShadowOffset = new CGSize(0, (nfloat)elevation);
            view.Layer.ShadowOpacity = 0.24f;
            view.Layer.ShadowRadius = Math.Abs(elevation);
        }

        internal static bool IsColorDark(this UIColor color)
        {
            color.GetRGBA(out var red, out var green, out var blue, out var alpha);
            var brightness = ((red * 299) + (green * 587) + (blue * 144)) / 1000;

            return brightness <= 0.5;
        }

        internal static bool IsColorDark(this CGColor color)
        {
            var components = color.Components;
            var brightness = ((components[0] * 299) + (components[1] * 587) + (components[2] * 144)) / 1000;

            return brightness <= 0.5;
        }

        internal static UIColor LightenColor(this UIColor color)
        {
            color.GetRGBA(out var red, out var green, out var blue, out var alpha);

            return UIColor.FromRGBA((float)Math.Min(red + 0.3, 1.0), (float)Math.Min(green + 0.3, 1.0), (float)Math.Min(blue + 0.3, 1.0), alpha);
        }

        internal static CGColor GetDisabledColor(this CGColor color)
        {
            const float disabledOpacity = 0.38f;
            var components = color.Components;
            var r = components[0];
            var g = components[1];
            var b = components[2];

            return new CGColor(r, g, b, disabledOpacity);
        }

        internal static UIColor GetDisabledColor(this UIColor color)
        {
            color.GetRGBA(out var r, out var g, out var b, out var a);

            a = 0.38f;

            return new UIColor(r, g, b, a);
        }

        internal static UIColor MixColor(this UIColor color1, UIColor color2)
        {
            color1.GetRGBA(out var r1, out var g1, out var b1, out var a1);
            color2.GetRGBA(out var r2, out var g2, out var b2, out var a2);

            return new UIColor((nfloat)Math.Min((r1 + r2) / 2, 1), (nfloat)Math.Min((g1 + g2) / 2, 1), (nfloat)Math.Min((b1 + b2) / 2, 1), (nfloat)Math.Min((a1 + a2) / 2, 1));
        }
    }
}