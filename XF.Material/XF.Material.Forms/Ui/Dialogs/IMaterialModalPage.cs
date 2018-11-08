using System;

namespace XF.Material.Forms.UI.Dialogs
{
    public interface IMaterialModalPage : IDisposable
    {
        /// <summary>
        /// Dismisses this modal dialog.
        /// </summary>
        void Dismiss();
    }
}
