using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XF.Material.Forms.UI
{
    [TypeConverter(typeof(MaterialColorTypeConverter))]
    public class MaterialColor
    {
        public static MaterialColor Default => new MaterialColor(Material.Color.Secondary, default(Color), default(Color));

        public Color EnabledColor { get; set; }

        public Color DisabledColor { get; set; }

        public Color PressedColor { get; set; }

        public MaterialColor() { }

        public MaterialColor(Color enabled, Color disabled, Color pressed)
        {
            this.EnabledColor = enabled;
            this.DisabledColor = disabled;
            this.PressedColor = pressed;
        }

        public static implicit operator MaterialColor(Color color)
        {
            if (color.IsDefault)
                return System.Drawing.Color.Empty;
            return new MaterialColor(color, Color.Default, Color.Default);
        }

        public static implicit operator System.Drawing.Color(MaterialColor color)
        {
            if (color.EnabledColor.IsDefault)
                return System.Drawing.Color.Empty;
            return System.Drawing.Color.FromArgb((byte)(color.EnabledColor.A), (byte)(color.EnabledColor.R), (byte)(color.EnabledColor.G), (byte)(color.EnabledColor.B));
        }

        public static implicit operator MaterialColor(System.Drawing.Color color)
        {
            if (color.IsEmpty)
                return MaterialColor.Default;
            return new MaterialColor(color, default(Color), default(Color));
        }
    }
}
