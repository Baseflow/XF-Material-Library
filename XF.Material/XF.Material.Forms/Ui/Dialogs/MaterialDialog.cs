using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace XF.Material.Forms.UI.Dialogs
{
    /// <summary>
    /// Class for showing dialogs and snackbar.
    /// </summary>
    public sealed class MaterialDialog : IMaterialDialog
    {
        private static readonly Lazy<IMaterialDialog> MaterialDialogInstance = new Lazy<IMaterialDialog>(() => new MaterialDialog());

        internal MaterialDialog()
        {
        }

        public static IMaterialDialog Instance => MaterialDialogInstance.Value;

        public Task AlertAsync(
            string message,
            MaterialAlertDialogConfiguration configuration = null)
        {
            return MaterialAlertDialog.AlertAsync(message, configuration: configuration);
        }

        public Task AlertAsync(
            string message,
            string title,
            MaterialAlertDialogConfiguration configuration = null)
        {
            return MaterialAlertDialog.AlertAsync(message, title, configuration);
        }

        public Task AlertAsync(
            string message,
            string title,
            string acknowledgementText,
            MaterialAlertDialogConfiguration configuration = null)
        {
            return MaterialAlertDialog.AlertAsync(message, title, acknowledgementText, configuration);
        }

        public Task<bool?> ConfirmAsync(
            string message,
            string title = null,
            string confirmingText = "Ok",
            string dismissiveText = "Cancel",
            MaterialAlertDialogConfiguration configuration = null)
        {
            return MaterialAlertDialog.ConfirmAsync(message, title, confirmingText, dismissiveText, configuration);
        }

        public Task<string> InputAsync(
            string title = null,
            string message = null,
            string inputText = null,
            string inputPlaceholder = "Enter input",
            string confirmingText = "Ok",
            string dismissiveText = "Cancel",
            MaterialInputDialogConfiguration configuration = null)
        {
            return MaterialInputDialog.Show(title, message, inputText, inputPlaceholder, confirmingText, dismissiveText, configuration);
        }

        public Task<IMaterialModalPage> LoadingDialogAsync(
            string message,
            MaterialLoadingDialogConfiguration configuration = null)
        {
            return MaterialLoadingDialog.Loading(message, configuration);
        }

        public Task<IMaterialModalPage> LoadingSnackbarAsync(
            string message,
            MaterialSnackbarConfiguration configuration = null)
        {
            return MaterialSnackbar.Loading(message, configuration);
        }

        public Task<int> SelectActionAsync(
            IList<string> actions,
            MaterialSimpleDialogConfiguration configuration = null)
        {
            return MaterialSimpleDialog.ShowAsync(null, actions, configuration);
        }

        public Task<int> SelectActionAsync(
            string title,
            IList<string> actions,
            MaterialSimpleDialogConfiguration configuration = null)
        {
            return MaterialSimpleDialog.ShowAsync(title, actions, configuration);
        }

        public async Task<int> SelectChoiceAsync(
            string title,
            IList<string> choices,
            string confirmingText = "Ok",
            string dismissiveText = "Cancel",
            MaterialConfirmationDialogConfiguration configuration = null)
        {
            return (int)await MaterialConfirmationDialog.ShowSelectChoiceAsync(title, choices, confirmingText, dismissiveText, configuration);
        }

        public async Task<int> SelectChoiceAsync(
            string title,
            IList<string> choices,
            int selectedIndex,
            string confirmingText = "Ok",
            string dismissiveText = "Cancel",
            MaterialConfirmationDialogConfiguration configuration = null)
        {
            return (int)await MaterialConfirmationDialog.ShowSelectChoiceAsync(title, choices, selectedIndex, confirmingText, dismissiveText, configuration);
        }

        public async Task<int[]> SelectChoicesAsync(
            string title,
            IList<string> choices,
            string confirmingText = "Ok",
            string dismissiveText = "Cancel",
            MaterialConfirmationDialogConfiguration configuration = null)
        {
            return (int[])await MaterialConfirmationDialog.ShowSelectChoicesAsync(title, choices, confirmingText, dismissiveText, configuration);
        }

        public async Task<int[]> SelectChoicesAsync(
            string title,
            IList<string> choices,
            IList<int> selectedIndices,
            string confirmingText = "Ok",
            string dismissiveText = "Cancel",
            MaterialConfirmationDialogConfiguration configuration = null)
        {
            return (int[])await MaterialConfirmationDialog.ShowSelectChoicesAsync(title, choices, selectedIndices, confirmingText, dismissiveText, configuration);
        }

        public void SetGlobalStyles(
            MaterialAlertDialogConfiguration dialogConfiguration = null,
            MaterialLoadingDialogConfiguration loadingDialogConfiguration = null,
            MaterialSnackbarConfiguration snackbarConfiguration = null,
            MaterialSimpleDialogConfiguration simpleDialogConfiguration = null,
            MaterialConfirmationDialogConfiguration confirmationDialogConfiguration = null,
            MaterialInputDialogConfiguration inputDialogConfiguration = null,
            MaterialAlertDialogConfiguration customContentDialogConfiguration = null)
        {
            MaterialAlertDialog.GlobalConfiguration = dialogConfiguration;
            MaterialLoadingDialog.GlobalConfiguration = loadingDialogConfiguration;
            MaterialSnackbar.GlobalConfiguration = snackbarConfiguration;
            MaterialSimpleDialog.GlobalConfiguration = simpleDialogConfiguration;
            MaterialConfirmationDialog.GlobalConfiguration = confirmationDialogConfiguration;
            MaterialInputDialog.GlobalConfiguration = inputDialogConfiguration;
            MaterialDialogFragment.GlobalConfiguration = customContentDialogConfiguration;
        }

        public Task<bool?> ShowCustomContentAsync(
            View view,
            string message,
            string title = null,
            string confirmingText = "Ok",
            string dismissiveText = "Cancel",
            MaterialAlertDialogConfiguration configuration = null)
        {
            return MaterialDialogFragment.ShowAsync(view, message, title, confirmingText, dismissiveText, configuration);
        }

        public Task SnackbarAsync(
            string message,
            int msDuration = MaterialSnackbar.DurationLong,
            MaterialSnackbarConfiguration configuration = null)
        {
            return MaterialSnackbar.ShowAsync(message, msDuration, configuration);
        }

        public Task<bool> SnackbarAsync(
            string message,
            string actionButtonText,
            int msDuration = MaterialSnackbar.DurationLong,
            MaterialSnackbarConfiguration configuration = null)
        {
            return MaterialSnackbar.ShowAsync(message, actionButtonText, msDuration, configuration);
        }
    }
}