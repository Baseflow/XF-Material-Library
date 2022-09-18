using Microsoft.Maui;

namespace XF.Material.Maui.Utilities
{
    /// <summary>
    /// Interface which provides methods that can be used to change platform-specific configurations.
    /// </summary>
    public interface IMaterialUtility
    {
        /// <summary>
        /// Changes the color of the status bar.
        /// </summary>
        /// <param name="color">The new color of the status bar.</param>
        void ChangeStatusBarColor(Color color);
    }
}
