using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Lottie.Forms.iOS.Renderers;
using UIKit;

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