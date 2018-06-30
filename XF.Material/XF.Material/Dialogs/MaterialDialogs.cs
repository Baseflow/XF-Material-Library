using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using XF.Material.Dialogs;

namespace XF.Material
{
    public static class MaterialDialogs
    {
        private static Task<IMaterialModalPage> _defaultMaterialDialog => Task.FromResult(default(IMaterialModalPage));

        public static async Task ShowAlertAsync(string message)
        {
            if(CanShowPopup<MaterialDialog>())
            {
                await MaterialDialog.AlertAsync(message);
            }
        }

        public static async Task ShowAlertAsync(string message, string title, string positiveButtonText = "OK", Action positiveAction = null, string negativeButtonText = "CANCEL", Action negativeAction = null)
        {
            if(CanShowPopup<MaterialDialog>())
            {
                await MaterialDialog.AlertAsync(message, title, positiveButtonText, positiveAction, negativeButtonText, negativeAction);
            }
        }

        public static Task<IMaterialModalPage> LoadingDialog(string message)
        {
            if(CanShowPopup<MaterialLoadingDialog>())
            {
                return MaterialLoadingDialog.Loading(message);
            }

            return _defaultMaterialDialog;
        }

        public static Task<IMaterialModalPage> LoadingSnackbar(string message)
        {
            if(CanShowPopup<MaterialSnackbar>())
            {
                return MaterialSnackbar.Loading(message);
            }

            return _defaultMaterialDialog;
        }

        public static async Task ShowSnackbarAsync(string message, int msDuration = 3000)
        {
            if(CanShowPopup<MaterialSnackbar>())
            {
                await MaterialSnackbar.ShowAsync(message, msDuration);
            }
        }

        public static async Task ShowSnackbarAsync(string message, string actionButtonText, Action primaryAction = null, Action hideAction = null, int msDuration = 3000)
        {
            if(CanShowPopup<MaterialSnackbar>())
            {
                await MaterialSnackbar.ShowAsync(message, actionButtonText, primaryAction, hideAction, msDuration);
            }
        }

        private static bool CanShowPopup<T>() where T : BaseMaterialModalPage
        {
            return !PopupNavigation.Instance.PopupStack.ToList().Exists(p => p is T);
        }
    }
}
