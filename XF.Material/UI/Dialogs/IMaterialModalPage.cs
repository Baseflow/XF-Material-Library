using System;
using System.Threading.Tasks;

namespace XF.Material.Maui.UI.Dialogs
{
    public interface IMaterialModalPage : IDisposable
    {
        /// <summary>
        /// Gets or sets the message text.
        /// </summary>
        string MessageText { get; set; }

        /// <summary>
        /// Dismisses this modal dialog asynchronously.
        /// </summary>
        Task DismissAsync();
    }
}
