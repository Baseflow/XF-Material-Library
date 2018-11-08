using System;

namespace XF.Material.Forms.UI
{
    public class SelectedChangedEventArgs : EventArgs
    {
        public bool IsSelected { get; }

        public SelectedChangedEventArgs(bool isSelected)
        {
            this.IsSelected = isSelected;
        }
    }
}
