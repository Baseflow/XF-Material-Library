using System;

namespace XF.Material.Forms.Views
{
    public class SelectedIndexChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Returns the index of the selected choice. Returns -1 if there was no selected choice.
        /// </summary>
        public int Index { get; }

        public SelectedIndexChangedEventArgs(int index)
        {
            this.Index = index;
        }
    }
}
