using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using XF.Material.Dialogs;
using XF.Material.Dialogs.Configurations;

namespace XF.Material
{
    public static class MaterialDialogs
    {
        private static Task<IMaterialModalPage> _defaultMaterialDialog => Task.FromResult(default(IMaterialModalPage));

        /// <summary>
        /// Shows an alert dialog with a specified message and title. It only has a single, dismissive action used for acknowledgement.
        /// </summary>
        /// <param name="message">The message of the alert dialog.</param>
        /// <returns></returns>
        public static async Task ShowAlertAsync(string message)
        {
            if (CanShowPopup<MaterialDialog>())
            {
                await MaterialDialog.AlertAsync(message);
            }
        }

        /// <summary>
        /// Shows an alert dialog with a specified message and title. It only has a single, dismissive action used for acknowledgement.
        /// </summary>
        /// <param name="message">The message of the alert dialog.</param>
        /// <param name="configuration">The style of the alert dialog.</param>
        /// <returns></returns>
        public static async Task ShowAlertAsync(string message, MaterialDialogConfiguration configuration)
        {
            if (CanShowPopup<MaterialDialog>())
            {
                await MaterialDialog.AlertAsync(message, configuration);
            }
        }

        /// <summary>
        /// Shows an alert dialog with a specified message and title. It only has a single, dismissive action used for acknowledgement.
        /// </summary>
        /// <param name="message">The message of the alert dialog.</param>
        /// <param name="title">The title of the alert dialog.</param>
        /// <returns></returns>
        public static async Task ShowAlertAsync(string message, string title)
        {
            if (CanShowPopup<MaterialDialog>())
            {
                await MaterialDialog.AlertAsync(message, title);
            }
        }

        /// <summary>
        /// Shows an alert dialog with a specified message and title. It only has a single, dismissive action used for acknowledgement.
        /// </summary>
        /// <param name="message">The message of the alert dialog.</param>
        /// <param name="title">The title of the alert dialog.</param>
        /// <param name="configuration">The style of the alert dialog.</param>
        /// <returns></returns>
        public static async Task ShowAlertAsync(string message, string title, MaterialDialogConfiguration configuration)
        {
            if (CanShowPopup<MaterialDialog>())
            {
                await MaterialDialog.AlertAsync(message, title, configuration);
            }
        }

        public static Task<IMaterialModalPage> LoadingDialog(string message)
        {
            if (CanShowPopup<MaterialLoadingDialog>())
            {
                return MaterialLoadingDialog.Loading(message);
            }

            return _defaultMaterialDialog;
        }

        public static Task<IMaterialModalPage> LoadingSnackbar(string message)
        {
            if (CanShowPopup<MaterialSnackbar>())
            {
                return MaterialSnackbar.Loading(message);
            }

            return _defaultMaterialDialog;
        }

        public static async Task ShowSnackbarAsync(string message, int msDuration = 3000)
        {
            if (CanShowPopup<MaterialSnackbar>())
            {
                await MaterialSnackbar.ShowAsync(message, msDuration);
            }
        }

        public static async Task ShowSnackbarAsync(string message, string actionButtonText, Action primaryAction = null, Action hideAction = null, int msDuration = 3000)
        {
            if (CanShowPopup<MaterialSnackbar>())
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
