using Lottie.Forms.UWP.Renderers;
using Rg.Plugins.Popup;

namespace XF.Material.Uwp
{
    public static class Material
    {
        /// <summary>
        /// Initializes the core Material components for the UWP platform.
        /// </summary>
        public static void Init()
        {
            Popup.Init();
            AnimationViewRenderer.Init();
        }
    }
}
