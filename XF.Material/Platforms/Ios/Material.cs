using Lottie.Forms.iOS.Renderers;
using Rg.Plugins.Popup;

namespace XF.Material.iOS
{
    public static class Material
    {
        /// <summary>
        /// Initializes the core Material components for the iOS platform.
        /// </summary>
        public static void Init()
        {
            Popup.Init();
            AnimationViewRenderer.Init();
        }
    }
}