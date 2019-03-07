using System;
using System.Threading.Tasks;

namespace XF.Material.Forms.UI.Dialogs
{
    public interface IMaterialModalPage : IDisposable
    {
        /// <summary>
        /// Sets the message text.
        /// </summary>
        /// <param name="text">Text.</param>
        void SetMessageText(string text);

        /// <summary>
        /// Dismisses this modal dialog asynchronously.
        /// </summary>
        Task DismissAsync();
    }
}
