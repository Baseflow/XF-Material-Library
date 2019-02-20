using System.Drawing;

namespace XF.Material.Core.Application.Theme
{
    public interface ITheme
    {
        Color NavigationBarBackgroundColor { get; }

        /// <summary>
        /// Root background color
        /// </summary>
        /// <value>The color of the background.</value>
        Color BackgroundColor { get; }

        /// <summary>
        /// Gets the color of the content background.
        /// </summary>
        /// <value>The color of the content background.</value>
        Color ContentBackgroundColor { get; }

        /// <summary>
        /// Gets the color of the content background when is selected.
        /// </summary>
        /// <value>The color of the content background selected.</value>
        Color ContentBackgroundSelectedColor { get; }

        Color TextColor { get; }

        Color TextBackgroundColor { get; }

        /// <summary>
        /// Button Color / Lines
        /// </summary>
        /// <value>The color of the accent.</value>
        Color AccentColor { get; }


        Color AccentPressedColor { get; }

        Color DisabledColor { get; }

        Color DialogBackgroundColor { get; }
    }
}
