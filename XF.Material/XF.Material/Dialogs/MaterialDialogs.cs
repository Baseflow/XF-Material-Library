using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XF.Material.Dialogs;

namespace XF.Material
{
    public static class MaterialDialogs
    {
        public static Task AlertAsync(string message, string title = "Alert")
        {
            return MaterialDialog.AlertAsync(message, title);
        }

        public static Task<MaterialLoadingDialog> Loading(string message)
        {
            return MaterialLoadingDialog.Loading(message);
        }
    }
}
