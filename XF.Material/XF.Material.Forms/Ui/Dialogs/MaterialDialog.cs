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

        /// <summary>
        /// The current instance to use for showing modal dialogs.
        /// </summary>
        public static IMaterialDialog Instance => MaterialDialogInstance.Value;

        /// <inheritdoc />
        /// <summary>
        /// Shows an alert dialog for acknowledgement. It only has a single, dismissive action used for acknowledgement.
        /// </summary>
        /// <param name="message">The message of the alert dialog.</param>
        /// <param name="configuration">The style of the alert dialog.</param>
        public Task AlertAsync(string message, MaterialAlertDialogConfiguration configuration = null)
        {
            return MaterialAlertDialog.AlertAsync(message, configuration: configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Shows an alert dialog for acknowledgement. It only has a single, dismissive action used for acknowledgement.
        /// </summary>
        /// <param name="message">The message of the alert dialog.</param>
        /// <param name="title">The title of the alert dialog.</param>
        /// <param name="configuration">The style of the alert dialog.</param>
        public Task AlertAsync(string message, string title, MaterialAlertDialogConfiguration configuration = null)
        {
            return MaterialAlertDialog.AlertAsync(message, title, configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Shows an alert dialog for acknowledgement. It only has a single, dismissive action used for acknowledgement.
        /// </summary>
        /// <param name="message">The message of the alert dialog.</param>
        /// <param name="title">The title of the alert dialog.</param>
        /// <param name="acknowledgementText">The text of the acknowledgement button.</param>
        /// <param name="configuration">The style of the alert dialog.</param>
        public Task AlertAsync(string message, string title, string acknowledgementText, MaterialAlertDialogConfiguration configuration = null)
        {
            return MaterialAlertDialog.AlertAsync(message, title, acknowledgementText, configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Shows an alert dialog for confirmation. Returns true when the confirm button was clicked, false if the dismiss button was clicked, and null if the alert dialog was dismissed.
        /// </summary>
        /// <param name="message">The message of the alert dialog.</param>
        /// <param name="title">The title of the alert dialog.</param>
        /// <param name="confirmingText">The text of the confirmation button.</param>
        /// <param name="dismissiveText">The text of the dismissive button</param>
        /// <param name="configuration">The style of the alert dialog.</param>
        public Task<bool?> ConfirmAsync(string message, string title = null, string confirmingText = "Ok", string dismissiveText = "Cancel", MaterialAlertDialogConfiguration configuration = null)
        {
            return MaterialAlertDialog.ConfirmAsync(message, title, confirmingText, dismissiveText, configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Shows a confirmation dialog that allow users to input text. If confirmed, returns the text value of the textfield. If canceled or dismissed, returns <see cref="F:System.String.Empty" />.
        /// </summary>
        /// <param name="title">The title of the confirmation dialog.</param>
        /// <param name="message">The message of the confirmation dialog.</param>
        /// <param name="inputText">The initial text of the textfield.</param>
        /// <param name="inputPlaceholder">The placeholder of the textfield.</param>
        /// <param name="confirmingText">The text of the confirmation button.</param>
        /// <param name="dismissiveText">The text of the dismissive button</param>
        /// <param name="configuration">The style of the confirmation dialog.</param>
        public Task<string> InputAsync(string title = null, string message = null, string inputText = null, string inputPlaceholder = "Enter input", string confirmingText = "Ok", string dismissiveText = "Cancel", MaterialInputDialogConfiguration configuration = null)
        {
            return MaterialInputDialog.Show(title, message, inputText, inputPlaceholder, confirmingText, dismissiveText, configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Shows a dialog indicating a running task.
        /// </summary>
        /// <param name="message">The message of the dialog.</param>
        /// <param name="configuration">The style of the loading dialog.</param>
        public Task<IMaterialModalPage> LoadingDialogAsync(string message, MaterialLoadingDialogConfiguration configuration = null)
        {
            return MaterialLoadingDialog.Loading(message, configuration);
        }

        /// <summary>
        /// Shows a snackbar indicating a running task.
        /// </summary>
        /// <param name="message">The message of the snackbar.</param>
        /// <param name="configuration">The style of the snackbar.</param>
        public Task<IMaterialModalPage> LoadingSnackbarAsync(string message, MaterialSnackbarConfiguration configuration = null)
        {
            return MaterialSnackbar.Loading(message, configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Shows a simple dialog that allows the user to select one of listed actions. Returns the index of the selected action.
        /// </summary>
        /// <param name="actions">The list of actions.</param>
        /// <param name="configuration">The style of the simple dialog.</param>
        /// <exception cref="T:System.ArgumentNullException" />
        public Task<int> SelectActionAsync(IList<string> actions, MaterialSimpleDialogConfiguration configuration = null)
        {
            return MaterialSimpleDialog.ShowAsync(null, actions, configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Shows a simple dialog that allows the user to select one of listed actions. Returns the index of the selected action.
        /// </summary>
        /// <param name="title">The title of the dialog.</param>
        /// <param name="actions">The list of actions.</param>
        /// <param name="configuration">The style of the simple dialog.</param>
        /// <exception cref="T:System.ArgumentNullException" />
        public Task<int> SelectActionAsync(string title, IList<string> actions, MaterialSimpleDialogConfiguration configuration = null)
        {
            return MaterialSimpleDialog.ShowAsync(title, actions, configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Shows a confirmation dialog that allows the user to select one of the listed choices. Returns the index of the selected choice. If none was selected, returns -1.
        /// </summary>
        /// <param name="title">The title of the confirmation dialog. This parameter must not be null or empty.</param>
        /// <param name="choices">The list of choices the user will choose from.</param>
        /// <param name="configuration">The style of the confirmation dialog.</param>
        /// <exception cref="T:System.ArgumentNullException" />
        public async Task<int> SelectChoiceAsync(string title, IList<string> choices, MaterialConfirmationDialogConfiguration configuration = null)
        {
            return (int)await MaterialConfirmationDialog.ShowSelectChoiceAsync(title, choices, configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Shows a confirmation dialog that allows the user to select one of the listed choices. Returns the index of the selected choice. If none was selected, returns -1.
        /// </summary>
        /// <param name="title">The title of the confirmation dialog. This parameter must not be null or empty.</param>
        /// <param name="choices">The list of choices the user will choose from.</param>
        /// <param name="selectedIndex">The currently selected index.</param>
        /// <param name="configuration">The style of the confirmation dialog.</param>
        /// <exception cref="T:System.ArgumentNullException" />
        /// <exception cref="T:System.IndexOutOfRangeException" />
        public async Task<int> SelectChoiceAsync(string title, IList<string> choices, int selectedIndex, MaterialConfirmationDialogConfiguration configuration = null)
        {
            return (int)await MaterialConfirmationDialog.ShowSelectChoiceAsync(title, choices, selectedIndex, configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Shows a confirmation dialog that allows the user to select any of the listed choices. Returns the indices of the selected choices. If none was selected, returns an empty array.
        /// </summary>
        /// <param name="title">The title of the confirmation dialog. This parameter must not be null or empty.</param>
        /// <param name="choices">The list of choices the user will choose from.</param>
        /// <param name="configuration">The style of the confirmation dialog.</param>
        /// <exception cref="T:System.ArgumentNullException" />
        public async Task<int[]> SelectChoicesAsync(string title, IList<string> choices, MaterialConfirmationDialogConfiguration configuration = null)
        {
            return (int[])await MaterialConfirmationDialog.ShowSelectChoicesAsync(title, choices, configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Shows a confirmation dialog that allows the user to select any of the listed choices. Returns the indices of the selected choices. If none was selected, returns an empty array.
        /// </summary>
        /// <param name="title">The title of the confirmation dialog. This parameter must not be null or empty.</param>
        /// <param name="choices">The list of choices the user will choose from.</param>
        /// <param name="selectedIndices">The currently selected indices.</param>
        /// <param name="configuration">The style of the confirmation dialog.</param>
        /// <exception cref="T:System.ArgumentNullException" />
        /// <exception cref="T:System.IndexOutOfRangeException" />
        public async Task<int[]> SelectChoicesAsync(string title, IList<string> choices, List<int> selectedIndices, MaterialConfirmationDialogConfiguration configuration = null)
        {
            return (int[])await MaterialConfirmationDialog.ShowSelectChoicesAsync(title, choices, selectedIndices, configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the global styles for <see cref="T:XF.Material.Forms.UI.Dialogs.MaterialAlertDialog" />, <see cref="T:XF.Material.Forms.UI.Dialogs.MaterialLoadingDialog" />, <see cref="T:XF.Material.Forms.UI.Dialogs.MaterialSimpleDialog" />, <see cref="T:XF.Material.Forms.UI.Dialogs.MaterialConfirmationDialog" />, and <see cref="T:XF.Material.Forms.UI.Dialogs.MaterialSnackbar" />. Parameters can be null.
        /// </summary>
        /// <param name="dialogConfiguration">Global style for <see cref="T:XF.Material.Forms.UI.Dialogs.MaterialAlertDialog" />.</param>
        /// <param name="loadingDialogConfiguration">Global style for <see cref="T:XF.Material.Forms.UI.Dialogs.MaterialLoadingDialog" />.</param>
        /// <param name="snackbarConfiguration">Global style for <see cref="T:XF.Material.Forms.UI.Dialogs.MaterialSnackbar" />.</param>
        /// <param name="simpleDialogConfiguration">Global style for <see cref="T:XF.Material.Forms.UI.Dialogs.MaterialSimpleDialog" />.</param>
        /// <param name="confirmationDialogConfiguration">Global style for <see cref="T:XF.Material.Forms.UI.Dialogs.MaterialConfirmationDialog" />.</param>
        /// <param name="inputDialogConfiguration">Global style for <see cref="T:XF.Material.Forms.UI.Dialogs.MaterialInputDialog" />.</param>
        /// <param name="customContentDialogConfiguration"></param>
        public void SetGlobalStyles(MaterialAlertDialogConfiguration dialogConfiguration = null, MaterialLoadingDialogConfiguration loadingDialogConfiguration = null, MaterialSnackbarConfiguration snackbarConfiguration = null, MaterialSimpleDialogConfiguration simpleDialogConfiguration = null, MaterialConfirmationDialogConfiguration confirmationDialogConfiguration = null, MaterialInputDialogConfiguration inputDialogConfiguration = null, MaterialAlertDialogConfiguration customContentDialogConfiguration = null)
        {
            MaterialAlertDialog.GlobalConfiguration = dialogConfiguration;
            MaterialLoadingDialog.GlobalConfiguration = loadingDialogConfiguration;
            MaterialSnackbar.GlobalConfiguration = snackbarConfiguration;
            MaterialSimpleDialog.GlobalConfiguration = simpleDialogConfiguration;
            MaterialConfirmationDialog.GlobalConfiguration = confirmationDialogConfiguration;
            MaterialInputDialog.GlobalConfiguration = inputDialogConfiguration;
            MaterialDialogFragment.GlobalConfiguration = customContentDialogConfiguration;
        }

        /// <inheritdoc />
        /// <summary>
        /// Shows an dialog for confirmation with a custom content. Returns true when the confirm button was clicked, false if the dismiss button was clicked, and null if the alert dialog was dismissed.
        /// </summary>
        /// <param name="view">The additional custom content of the dialog.</param>
        /// <param name="message">The message of the dialog.</param>
        /// <param name="title">The title of the dialog.</param>
        /// <param name="confirmingText">The text of the confirmation button.</param>
        /// <param name="dismissiveText">The text of the dismissive button</param>
        /// <param name="configuration">The style of the dialog.</param>
        /// <exception cref="T:System.ArgumentNullException" />
        public Task<bool?> ShowCustomContentAsync(View view, string message, string title = null, string confirmingText = "Ok", string dismissiveText = "Cancel", MaterialAlertDialogConfiguration configuration = null)
        {
            return MaterialDialogFragment.ShowAsync(view, message, title, confirmingText, dismissiveText, configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Shows a snackbar with no action.
        /// </summary>
        /// <param name="message">The message of the snackbar.</param>
        /// <param name="msDuration">The duration, in milliseconds, before the snackbar is automatically dismissed.</param>
        /// <param name="configuration">The style of the snackbar.</param>
        public Task SnackbarAsync(string message, int msDuration = MaterialSnackbar.DurationLong, MaterialSnackbarConfiguration configuration = null)
        {
            return MaterialSnackbar.ShowAsync(message, msDuration, configuration);
        }

        /// <inheritdoc />
        /// <summary>
        /// Shows a snackbar with an action. Returns true if the snackbar's action button was clicked, or false if the snackbar was automatically dismissed.
        /// </summary>
        /// <param name="message">The message of the snackbar.</param>
        /// <param name="actionButtonText">The label text of the snackbar's button.</param>
        /// <param name="msDuration">The duration, in milliseconds, before the snackbar is automatically dismissed.</param>
        /// <param name="configuration">The style of the snackbar.</param>
        public Task<bool> SnackbarAsync(string message, string actionButtonText, int msDuration = MaterialSnackbar.DurationLong, MaterialSnackbarConfiguration configuration = null)
        {
            return MaterialSnackbar.ShowAsync(message, actionButtonText, msDuration, configuration);
        }
    }
}