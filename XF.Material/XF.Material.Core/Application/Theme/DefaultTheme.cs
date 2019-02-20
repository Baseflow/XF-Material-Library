using System.Drawing;

namespace XF.Material.Core.Application.Theme
{
    public class DefaultTheme : ITheme
    {
        public Color BackgroundColor { get; } = Color.Black;

        public Color[] MenuGradientColors => new Color[] { Color.FromArgb(60, 60, 60), Color.FromArgb(50, 50, 50) };

        public Color NavigationBarBackgroundColor { get; } = Color.FromArgb(29, 29, 29);

        public Color ContentBackgroundColor { get; } = Color.FromArgb(46, 46, 46);

        public Color ContentBackgroundSelectedColor => WithAlpha(ContentBackgroundColor, 0.75f);

        public Color TextColor { get; } = Color.White;

        public Color AccentColor => Color.FromArgb(231, 58, 93);

        public Color DisabledColor { get; } = Color.Gray;

        public Color AccentPressedColor => WithAlpha(AccentColor, 0.5f);

        public Color DialogBackgroundColor { get; } = Color.FromArgb(109, 109, 109);

        public Color TextBackgroundColor => WithAlpha(Color.FromArgb(14, 14, 14), 0.2f);

        private static Color WithAlpha(Color color, float alpha)
        {
            return Color.FromArgb((int)(alpha * 255), color.R, color.G, color.B);
        }
    }
}
