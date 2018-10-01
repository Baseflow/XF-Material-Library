using System;

namespace XF.Material.Forms.Views
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
