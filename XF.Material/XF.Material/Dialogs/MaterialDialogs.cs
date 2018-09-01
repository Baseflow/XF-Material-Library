using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using XF.Material.Dialogs;
using XF.Material.Dialogs.Configurations;

namespace XF.Material.Dialogs
{
    /// <summary>
    /// Static class for showing dialogs and snackbar.
    /// </summary>
    public static class MaterialDialogs
    {
        /// <summary>
        /// Shows an alert dialog for acknowledgement. It only has a single, dismissive action used for acknowledgement.
        /// </summary>
        /// <param name="message">The message of the alert dialog.</param>
        /// <param name="acknowledgementText">The text of the acknowledgement button.</param>
        /// <param name="configuration">The style of the alert dialog.</param>
        public static async Task ShowAlertAsync(string message, string acknowledgementText = "Ok", MaterialAlertDialogConfiguration configuration = null)
        {
            if (CanShowPopup<MaterialDialog>())
            {
                await MaterialDialog.AlertAsync(message, acknowledgementText, configuration);
            }
        }

        /// <summary>
        /// Shows an alert dialog for acknowledgement. It only has a single, dismissive action used for acknowledgement.
        /// </summary>
        /// <param name="message">The message of the alert dialog.</param>
        /// <param name="title">The title of the alert dialog.</param>
        /// <param name="acknowledgementText">The text of the acknowledgement button.</param>
        /// <param name="configuration">The style of the alert dialog.</param>
        public static async Task ShowAlertAsync(string message, string title, string acknowledgementText = "Ok", MaterialAlertDialogConfiguration configuration = null)
        {
            if (CanShowPopup<MaterialDialog>())
            {
                await MaterialDialog.AlertAsync(message, title, acknowledgementText, configuration);
            }
        }

        /// <summary>
        /// Shows an alert dialog for confirmation.
        /// </summary>
        /// <param name="message">The message of the alert dialog.</param>
        /// <param name="confirmingText">The text of the confirmation button.</param>
        /// <param name="confirmingAction">The action that will run when the confirmation button is clicked.</param>
        /// <param name="dismissiveText">The text of the dismissive button</param>
        /// <param name="configuration">The style of the alert dialog.</param>
        public static async Task ShowAlertAsync(string message, string confirmingText, Action confirmingAction, string dismissiveText = "Cancel", MaterialAlertDialogConfiguration configuration = null)
        {
            if (CanShowPopup<MaterialDialog>())
            {
                await MaterialDialog.AlertAsync(message, null, confirmingText, confirmingAction, dismissiveText, configuration);
            }
        }

        /// <summary>
        /// Shows an alert dialog for confirmation.
        /// </summary>
        /// <param name="message">The message of the alert dialog.</param>
        /// <param name="title">The title of the alert dialog.</param>
        /// <param name="confirmingText">The text of the confirmation button.</param>
        /// <param name="confirmingAction">The action that will run when the confirmation button is clicked.</param>
        /// <param name="dismissiveText">The text of the dismissive button</param>
        /// <param name="configuration">The style of the alert dialog.</param>
        public static async Task ShowAlertAsync(string message, string title, string confirmingText, Action confirmingAction, string dismissiveText = "Cancel", MaterialAlertDialogConfiguration configuration = null)
        {
            if (CanShowPopup<MaterialDialog>())
            {
                await MaterialDialog.AlertAsync(message, title, confirmingText, confirmingAction, dismissiveText, configuration);
            }
        }

        /// <summary>
        /// Shows a dialog indicating a running task. Use this in a using block.
        /// </summary>
        /// <param name="message">The message of the dialog.</param>
        /// <param name="configuration">The style of the loading dialog.</param>
        public static async Task<MaterialLoadingDialog> ShowLoadingDialogAsync(string message, MaterialLoadingDialogConfiguration configuration = null)
        {
            if (CanShowPopup<MaterialLoadingDialog>())
            {
                return await MaterialLoadingDialog.Loading(message, configuration);
            }

            return await Task.FromResult(default(MaterialLoadingDialog));
        }

        /// <summary>
        /// Shows a snackbar indicating a running task. Use this in a using block.
        /// </summary>
        /// <param name="message">The message of the snackbar.</param>
        /// <param name="configuration">The style of the snackbar.</param>
        public static Task<MaterialSnackbar> ShowLoadingSnackbarAsync(string message, MaterialSnackbarConfiguration configuration = null)
        {
            if (CanShowPopup<MaterialSnackbar>())
            {
                return MaterialSnackbar.Loading(message, configuration);
            }

            return Task.FromResult(default(MaterialSnackbar));
        }

        /// <summary>
        /// Shows a snackbar with a specified duration.
        /// </summary>
        /// <param name="message">The message of the snackbar.</param>
        /// <param name="msDuration">The duration, in milliseconds, before the snackbar is dismissed.</param>
        /// <param name="configuration">The style of the snackbar.</param>
        public static async Task ShowSnackbarAsync(string message, int msDuration = MaterialSnackbar.DURATION_LONG, MaterialSnackbarConfiguration configuration = null)
        {
            if (CanShowPopup<MaterialSnackbar>())
            {
                await MaterialSnackbar.ShowAsync(message, msDuration, configuration);
            }
        }

        /// <summary>
        /// Shows a snackbar with associated actions.
        /// </summary>
        /// <param name="message">The message of the snackbar.</param>
        /// <param name="actionButtonText">The label text of the snackbar's button.</param>
        /// <param name="primaryAction">The action that will run when the snackbar's button is clicked.</param>
        /// <param name="hideAction">The action that will run when the snackbar is dismissed.</param>
        /// <param name="msDuration">The duration, in milliseconds, before the snackbar is dismissed.</param>
        /// <param name="configuration">The style of the snackbar.</param>
        public static async Task ShowSnackbarAsync(string message, string actionButtonText, Action primaryAction = null, Action hideAction = null, int msDuration = MaterialSnackbar.DURATION_LONG, MaterialSnackbarConfiguration configuration = null)
        {
            if (CanShowPopup<MaterialSnackbar>())
            {
                await MaterialSnackbar.ShowAsync(message, actionButtonText, primaryAction, hideAction, msDuration, configuration);
            }
        }

        /// <summary>
        /// Sets the global styles for <see cref="MaterialDialog"/> and <see cref="MaterialLoadingDialog"/>.
        /// </summary>
        /// <param name="dialogConfiguration">Global style for <see cref="MaterialDialog"/>.</param>
        /// <param name="loadingDialogConfiguration">Global style for <see cref="MaterialLoadingDialog"/>.</param>
        /// <param name="snackbarConfiguration">Global style for <see cref="MaterialSnackbar"/>.</param>
        public static void SetGlobalStyles(MaterialAlertDialogConfiguration dialogConfiguration, MaterialLoadingDialogConfiguration loadingDialogConfiguration, MaterialSnackbarConfiguration snackbarConfiguration)
        {
            MaterialDialog.GlobalConfiguration = dialogConfiguration;
            MaterialLoadingDialog.GlobalConfiguration = loadingDialogConfiguration;
            MaterialSnackbar.GlobalConfiguration = snackbarConfiguration;
        }

        private static bool CanShowPopup<T>() where T : BaseMaterialModalPage
        {
            return !PopupNavigation.Instance.PopupStack.ToList().Exists(p => p is T);
        }
    }
}
