using Xamarin.Forms;

namespace XF.Material.Forms.Utilities
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
