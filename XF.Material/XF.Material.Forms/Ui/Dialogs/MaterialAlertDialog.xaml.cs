using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace XF.Material.Forms.UI.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialAlertDialog : BaseMaterialModalPage, IMaterialAwaitableDialog<bool>
    {
        internal MaterialAlertDialog(string message, string title, string action1Text, string action2Text, MaterialAlertDialogConfiguration configuration = null) : this(configuration)
        {
            Message.Text = message;
            DialogTitle.Text = title;
            PositiveButton.Text = action1Text;
            PositiveButton.Command = new Command(() =>
            {
                this.InputTaskCompletionSource?.SetResult(true);
                this.Dismiss();
            });
            NegativeButton.Text = action2Text;
            NegativeButton.Command = new Command(() =>
            {
                this.InputTaskCompletionSource?.SetResult(false);
                this.Dismiss();
            });
        }

        internal MaterialAlertDialog(MaterialAlertDialogConfiguration configuration)
        {
            this.InitializeComponent();
            this.Configure(configuration);
        }

        public TaskCompletionSource<bool> InputTaskCompletionSource { get; set; }

        internal static MaterialAlertDialogConfiguration GlobalConfiguration { get; set; }

        internal static async Task AlertAsync(string message, string acknowledgementText = "Ok", MaterialAlertDialogConfiguration configuration = null)
        {
            var dialog = new MaterialAlertDialog(message, null, acknowledgementText, null, configuration: configuration);
            await dialog.ShowAsync();
        }

        internal static async Task AlertAsync(string message, string title, string acknowledgementText = "Ok", MaterialAlertDialogConfiguration configuration = null)
        {
            var dialog = new MaterialAlertDialog(message, title, acknowledgementText, null, configuration: configuration);
            await dialog.ShowAsync();
        }

        internal static async Task<bool> ConfirmAsync(string message, string confirmingText, string dismissiveText = "Cancel", MaterialAlertDialogConfiguration configuration = null)
        {
            var dialog = new MaterialAlertDialog(message, null, confirmingText, dismissiveText, configuration)
            {
                InputTaskCompletionSource = new TaskCompletionSource<bool>()
            };

            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        internal static async Task<bool> ConfirmAsync(string message, string title, string confirmingText, string dismissiveText = "Cancel", MaterialAlertDialogConfiguration configuration = null)
        {
            var dialog = new MaterialAlertDialog(message, title, confirmingText, dismissiveText, configuration)
            {
                InputTaskCompletionSource = new TaskCompletionSource<bool>()
            };

            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        protected override bool OnBackButtonPressed()
        {
            this.InputTaskCompletionSource?.SetResult(false);

            return base.OnBackButtonPressed();
        }

        protected override bool OnBackgroundClicked()
        {
            this.InputTaskCompletionSource?.SetResult(false);

            return base.OnBackgroundClicked();
        }

        private void Configure(MaterialAlertDialogConfiguration configuration)
        {
            var preferredConfig = configuration ?? GlobalConfiguration;

            if (preferredConfig != null)
            {
                this.BackgroundColor = preferredConfig.ScrimColor;
                Container.CornerRadius = preferredConfig.CornerRadius;
                Container.BackgroundColor = preferredConfig.BackgroundColor;
                DialogTitle.TextColor = preferredConfig.TitleTextColor;
                DialogTitle.FontFamily = preferredConfig.TitleFontFamily;
                Message.TextColor = preferredConfig.MessageTextColor;
                Message.FontFamily = preferredConfig.MessageFontFamily;
                PositiveButton.TextColor = NegativeButton.TextColor = preferredConfig.TintColor;
                PositiveButton.AllCaps = NegativeButton.AllCaps = preferredConfig.ButtonAllCaps;
                PositiveButton.FontFamily = NegativeButton.FontFamily = preferredConfig.ButtonFontFamily;
            }
        }
    }
}