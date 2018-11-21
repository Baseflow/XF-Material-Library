using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace XF.Material.Forms.UI.Dialogs
{
    /// <summary>
    /// Interface that defines the methods for showing dialogs and snackbars.
    /// </summary>
    public interface IMaterialDialog
    {
        /// <summary>
        /// Shows an alert dialog for acknowledgement. It only has a single, dismissive action used for acknowledgement.
        /// </summary>
        /// <param name="message">The message of the alert dialog.</param>
        /// <param name="configuration">The style of the alert dialog.</param>
        Task AlertAsync(string message, MaterialAlertDialogConfiguration configuration = null);

        /// <summary>
        /// Shows an alert dialog for acknowledgement. It only has a single, dismissive action used for acknowledgement.
        /// </summary>
        /// <param name="message">The message of the alert dialog.</param>
        /// <param name="title">The title of the alert dialog.</param>
        /// <param name="configuration">The style of the alert dialog.</param>
        Task AlertAsync(string message, string title, MaterialAlertDialogConfiguration configuration = null);

        /// <summary>
        /// Shows an alert dialog for acknowledgement. It only has a single, dismissive action used for acknowledgement.
        /// </summary>
        /// <param name="message">The message of the alert dialog.</param>
        /// <param name="title">The title of the alert dialog.</param>
        /// <param name="acknowledgementText">The text of the acknowledgement button.</param>
        /// <param name="configuration">The style of the alert dialog.</param>
        Task AlertAsync(string message, string title, string acknowledgementText, MaterialAlertDialogConfiguration configuration = null);

        /// <summary>
        /// Shows an alert dialog for confirmation. Returns true when the confirm button was clicked, false if the dismiss button was clicked or if the alert dialog was dismissed.
        /// </summary>
        /// <param name="message">The message of the alert dialog.</param>
        /// <param name="confirmingText">The text of the confirmation button.</param>
        /// <param name="dismissiveText">The text of the dismissive button</param>
        /// <param name="configuration">The style of the alert dialog.</param>
        Task<bool> ConfirmAsync(string message, string confirmingText = "Ok", string dismissiveText = "Cancel", MaterialAlertDialogConfiguration configuration = null);

        /// <summary>
        /// Shows an alert dialog for confirmation. Returns true when the confirm button was clicked, false if the dismiss button was clicked or if the alert dialog was dismissed.
        /// </summary>
        /// <param name="message">The message of the alert dialog.</param>
        /// <param name="title">The title of the alert dialog.</param>
        /// <param name="confirmingText">The text of the confirmation button.</param>
        /// <param name="dismissiveText">The text of the dismissive button</param>
        /// <param name="configuration">The style of the alert dialog.</param>
        Task<bool> ConfirmAsync(string message, string title, string confirmingText = "Ok", string dismissiveText = "Cancel", MaterialAlertDialogConfiguration configuration = null);

        /// <summary>
        /// Shows a confirmation dialog that allow users to input text. If confirmed, returns the text value of the textfield. If canceled or dismissed, returns <see cref="string.Empty"/>.
        /// </summary>
        /// <param name="title">The title of the confirmation dialog.</param>
        /// <param name="message">The message of the confirmation dialog.</param>
        /// <param name="inputText">The initial text of the textfield.</param>
        /// <param name="inputPlaceholder">The placeholder of the textfield.</param>
        /// <param name="confirmingText">The text of the confirmation button.</param>
        /// <param name="dismissiveText">The text of the dismissive button</param>
        /// <param name="configuration">The style of the confirmation dialog.</param>
        Task<string> InputAsync(string title = null, string message = null, string inputText = null, string inputPlaceholder = "Enter input", string confirmingText = "Ok", string dismissiveText = "Cancel", MaterialInputDialogConfiguration configuration = null);

        /// <summary>
        /// Shows a dialog indicating a running task.
        /// </summary>
        /// <param name="message">The message of the dialog.</param>
        /// <param name="configuration">The style of the loading dialog.</param>
        Task<IMaterialModalPage> LoadingDialogAsync(string message, MaterialLoadingDialogConfiguration configuration = null);

        /// <summary>
        /// Shows a snackbar indicating a running task.
        /// </summary>
        /// <param name="message">The message of the snackbar.</param>
        /// <param name="configuration">The style of the snackbar.</param>
        Task<IMaterialModalPage> LoadingSnackbarAsync(string message, MaterialSnackbarConfiguration configuration = null);

        /// <summary>
        /// Shows a simple dialog that allows the user to select one of listed actions. Returns the index of the selected action.
        /// </summary>
        /// <param name="actions">The list of actions.</param>
        /// <param name="configuration">The style of the simple dialog.</param>
        /// <exception cref="ArgumentNullException" />
        Task<int> SelectActionAsync(IList<string> actions, MaterialSimpleDialogConfiguration configuration = null);

        /// <summary>
        /// Shows a simple dialog that allows the user to select one of listed actions. Returns the index of the selected action.
        /// </summary>
        /// <param name="title">The title of the dialog.</param>
        /// <param name="actions">The list of actions.</param>
        /// <param name="configuration">The style of the simple dialog.</param>
        /// <exception cref="ArgumentNullException" />
        Task<int> SelectActionAsync(string title, IList<string> actions, MaterialSimpleDialogConfiguration configuration = null);

        /// <summary>
        /// Shows a confirmation dialog that allows the user to select one of the listed choices. Returns the index of the selected choice. If none was selected, returns -1.
        /// </summary>
        /// <param name="title">The title of the confirmation dialog. This parameter must not be null or empty.</param>
        /// <param name="choices">The list of choices the user will choose from.</param>
        /// <param name="configuration">The style of the confirmation dialog.</param>
        /// <exception cref="ArgumentNullException" />
        Task<int> SelectChoiceAsync(string title, IList<string> choices, MaterialConfirmationDialogConfiguration configuration = null);

        /// <summary>
        /// Shows a confirmation dialog that allows the user to select one of the listed choices. Returns the index of the selected choice. If none was selected, returns -1.
        /// </summary>
        /// <param name="title">The title of the confirmation dialog. This parameter must not be null or empty.</param>
        /// <param name="choices">The list of choices the user will choose from.</param>
        /// <param name="selectedIndex">The currently selected index.</param>
        /// <param name="configuration">The style of the confirmation dialog.</param>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="IndexOutOfRangeException" />
        Task<int> SelectChoiceAsync(string title, IList<string> choices, int selectedIndex, MaterialConfirmationDialogConfiguration configuration = null);

        /// <summary>
        /// Shows a confirmation dialog that allows the user to select any of the listed choices. Returns the indices of the selected choices. If none was selected, returns an empty array.
        /// </summary>
        /// <param name="title">The title of the confirmation dialog. This parameter must not be null or empty.</param>
        /// <param name="choices">The list of choices the user will choose from.</param>
        /// <param name="configuration">The style of the confirmation dialog.</param>
        /// <exception cref="ArgumentNullException" />
        Task<int[]> SelectChoicesAsync(string title, IList<string> choices, MaterialConfirmationDialogConfiguration configuration = null);

        /// <summary>
        /// Shows a confirmation dialog that allows the user to select any of the listed choices. Returns the indices of the selected choices. If none was selected, returns an empty array.
        /// </summary>
        /// <param name="title">The title of the confirmation dialog. This parameter must not be null or empty.</param>
        /// <param name="choices">The list of choices the user will choose from.</param>
        /// <param name="selectedIndices">The currently selected indices.</param>
        /// <param name="configuration">The style of the confirmation dialog.</param>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="IndexOutOfRangeException" />
        Task<int[]> SelectChoicesAsync(string title, IList<string> choices, List<int> selectedIndices, MaterialConfirmationDialogConfiguration configuration = null);

        /// <summary>
        /// Sets the global styles for <see cref="MaterialAlertDialog"/>, <see cref="MaterialLoadingDialog"/>, <see cref="MaterialSimpleDialog"/>, <see cref="MaterialConfirmationDialog"/>, and <see cref="MaterialSnackbar"/>. Parameters can be null.
        /// </summary>
        /// <param name="dialogConfiguration">Global style for <see cref="MaterialAlertDialog"/>.</param>
        /// <param name="loadingDialogConfiguration">Global style for <see cref="MaterialLoadingDialog"/>.</param>
        /// <param name="snackbarConfiguration">Global style for <see cref="MaterialSnackbar"/>.</param>
        /// <param name="simpleDialogConfiguration">Global style for <see cref="MaterialSimpleDialog"/>.</param>
        /// <param name="confirmationDialogConfiguration">Global style for <see cref="MaterialConfirmationDialog"/>.</param>
        /// <param name="inputDialogConfiguration">Global style for <see cref="MaterialInputDialog"/>.</param>
        void SetGlobalStyles(MaterialAlertDialogConfiguration dialogConfiguration, MaterialLoadingDialogConfiguration loadingDialogConfiguration, MaterialSnackbarConfiguration snackbarConfiguration, MaterialSimpleDialogConfiguration simpleDialogConfiguration, MaterialConfirmationDialogConfiguration confirmationDialogConfiguration, MaterialInputDialogConfiguration inputDialogConfiguration);

        /// <summary>
        /// Shows a snackbar with no action.
        /// </summary>
        /// <param name="message">The message of the snackbar.</param>
        /// <param name="msDuration">The duration, in milliseconds, before the snackbar is automatically dismissed.</param>
        /// <param name="configuration">The style of the snackbar.</param>
        Task SnackbarAsync(string message, int msDuration = MaterialSnackbar.DurationLong, MaterialSnackbarConfiguration configuration = null);

        /// <summary>
        /// Shows a snackbar with an action. Returns true if the snackbar's action button was clicked, or false if the snackbar was automatically dismissed.
        /// </summary>
        /// <param name="message">The message of the snackbar.</param>
        /// <param name="actionButtonText">The label text of the snackbar's button.</param>
        /// <param name="msDuration">The duration, in milliseconds, before the snackbar is automatically dismissed.</param>
        /// <param name="configuration">The style of the snackbar.</param>
        Task<bool> SnackbarAsync(string message, string actionButtonText, int msDuration = MaterialSnackbar.DurationLong, MaterialSnackbarConfiguration configuration = null);
    }
}