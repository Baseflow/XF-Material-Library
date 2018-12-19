using System;
using System.Threading.Tasks;

namespace XF.Material.Forms.UI.Dialogs
{
    public interface IMaterialModalPage : IDisposable
    {
        /// <summary>
        /// Dismisses this modal dialog.
        /// </summary>
        [Obsolete("Please use DismissAsync()")]
        void Dismiss();

        /// <summary>
        /// Dismisses this modal dialog asynchronously.
        /// </summary>
        Task DismissAsync();
    }
}
