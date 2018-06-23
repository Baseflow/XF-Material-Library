using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XF.Material.Dialogs;

namespace XF.Material
{
    public static class MaterialDialogs
    {
        public static void Alert(string message, string title = "Alert")
        {
            MaterialDialog.Alert(message, title);
        }

        public static async Task AlertAsync(string message, string title = "Alert")
        {
            await MaterialDialog.AlertAsync(message, title);
        }
    }
}
