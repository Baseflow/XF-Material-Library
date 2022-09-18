using System;
using System.Collections.Generic;

namespace XF.Material.Maui.UI
{
    public class SelectedIndicesChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Returns the indices of each selected choices.
        /// </summary>
        public IList<int> Indices { get; }

        public SelectedIndicesChangedEventArgs(IList<int> indices)
        {
            Indices = indices;
        }
    }
}
