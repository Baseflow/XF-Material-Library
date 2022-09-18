using System;

namespace XF.Material.Maui.UI
{
    public class SelectedChangedEventArgs : EventArgs
    {
        public bool IsSelected { get; }

        public SelectedChangedEventArgs(bool isSelected)
        {
            IsSelected = isSelected;
        }
    }
}
