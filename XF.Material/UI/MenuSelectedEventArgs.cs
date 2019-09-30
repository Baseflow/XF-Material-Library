using System;
using XF.Material.Forms.Models;

namespace XF.Material.Forms.UI
{
    /// <inheritdoc />
    /// <summary>
    /// Contains event data when <see cref="E:XF.Material.Forms.UI.MaterialMenuButton.MenuSelected" /> is invoked.
    /// </summary>
    public class MenuSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// The result of the selection.
        /// </summary>
        public MaterialMenuResult Result { get; }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of <see cref="T:XF.Material.Forms.UI.MenuSelectedEventArgs" />.
        /// </summary>
        /// <param name="result">The result of the selection.</param>
        public MenuSelectedEventArgs(MaterialMenuResult result)
        {
            this.Result = result;
        }
    }
}
