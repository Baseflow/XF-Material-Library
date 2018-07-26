using Lottie.Forms.iOS.Renderers;

namespace XF.Material.iOS
{
    public static class Material
    {
        public static void Init()
        {
            Rg.Plugins.Popup.Popup.Init();
            AnimationViewRenderer.Init();
        }
    }
}