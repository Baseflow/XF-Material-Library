﻿using System;

namespace XF.Material.Maui.UI.Internals
{
    public class NullableDateChangedEventArgs : EventArgs
    {
        public NullableDateChangedEventArgs(DateTime? oldDate, DateTime? newDate)
        {
            OldDate = oldDate;
            NewDate = newDate;
        }

        public DateTime? OldDate { get; set; }

        public DateTime? NewDate { get; set; }
    }
}
