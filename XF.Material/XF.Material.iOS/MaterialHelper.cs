using CoreGraphics;
using System;
using UIKit;

namespace XF.Material.iOS
{
    public static class MaterialHelper
    {
        public static void Elevate(this UIView view, int elevation)
        {
            view.Layer.MasksToBounds = false;
            view.Layer.ShadowColor = UIColor.Black.CGColor;
            view.Layer.ShadowOffset = new CGSize(0, (nfloat)elevation);
            view.Layer.ShadowOpacity = 0.24f;
            view.Layer.ShadowRadius = Math.Abs(elevation);
        }

        public static bool IsColorDark(this UIColor color)
        {
            var components = color.CGColor.Components;
            var brightness = ((components[0] * 299) + (components[1] * 587) + (components[2] * 144)) / 1000;

            return brightness <= 0.5;
        }

        public static bool IsColorDark(this CGColor color)
        {
            var components = color.Components;
            var brightness = ((components[0] * 299) + (components[1] * 587) + (components[2] * 144)) / 1000;

            return brightness <= 0.5;
        }

        public static UIColor BlendColor(UIColor color1, UIColor color2, float alpha)
        {
            alpha = Math.Min(1f, Math.Max(0f, alpha));
            color1.GetRGBA(out nfloat r1, out nfloat g1, out nfloat b1, out nfloat a1);
            color2.GetRGBA(out nfloat r2, out nfloat g2, out nfloat b2, out nfloat a2);
            var r = (nfloat)Math.Min(r1 + r2, 1);
            var g = (nfloat)Math.Min(g1 + g2, 1);
            var b = (nfloat)Math.Min(b1 + b2, 1);

            return new UIColor(r, g, b, alpha);
        }

        public static UIColor LightenColor(this UIColor color)
        {
            color.GetRGBA(out nfloat red, out nfloat green, out nfloat blue, out nfloat alpha);

            return UIColor.FromRGBA((float)Math.Min(red + 0.3, 1.0), (float)Math.Min(green + 0.3, 1.0), (float)Math.Min(blue + 0.3, 1.0), alpha);
        }

        public static UIColor DarkenColor(this UIColor color)
        {
            color.GetRGBA(out nfloat red, out nfloat green, out nfloat blue, out nfloat alpha);

            return UIColor.FromRGBA((float)Math.Max(red - 0.2, 0), (float)Math.Max(green - 0.2, 0), (float)Math.Max(blue - 0.2, 0), alpha);
        }
    }
}