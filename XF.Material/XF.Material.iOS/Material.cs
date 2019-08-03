using Lottie.Forms.iOS.Renderers;

namespace XF.Material.iOS
{
    public static class Material
    {
        /// <summary>
        /// Initializes the core Material components for the iOS platform.
        /// </summary>
        public static void Init()
        {
            Rg.Plugins.Popup.Popup.Init();
            AnimationViewRenderer.Init();
        }
    }
}