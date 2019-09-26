using System;
using UIKit;

namespace XF.Material.iOS
{
    internal static class DeviceHelper
    {
        internal static bool IsiOS13OrNewer
        {
            get
            {
                return UIDevice.CurrentDevice.CheckSystemVersion(13, 0);
            }
        }
    }
}
