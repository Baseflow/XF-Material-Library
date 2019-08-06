using System;
using System.Collections.Generic;
using System.Text;

namespace XF.Material.Forms.UI.Internals
{
    public class NullableDateChangedEventArgs : EventArgs
    {
        public NullableDateChangedEventArgs(DateTime? oldDate, DateTime? newDate)
        {
            this.OldDate = oldDate;
            this.NewDate = newDate;
        }

        public DateTime? OldDate { get; set; }

        public DateTime? NewDate { get; set; }
    }
}
